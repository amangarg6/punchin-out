using Attendance_Management.Models;
using Attendance_Management.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Attendance_Management.Controllers
{
    [Route("api/punch")]
    [ApiController]
    public class PunchController : ControllerBase
    {
        private readonly IPunchrepo _punch;
        private readonly ILeaverepo _leave;

        public PunchController(IPunchrepo punch, ILeaverepo leave)
        {
            _punch = punch;
            _leave = leave;
        }

        [HttpGet]
        public async Task<IActionResult> GetPunch()
        {
            try
            {
                var punches = await _punch.GetPunch();
                foreach(var p in punches)
                {
                    if(p.PunchOut > DateTime.Now)
                    {
                        p.PunchOut = null;
                    }
                }
                return Ok(punches);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

        [HttpGet("(Check)/{userId}")]
        public async Task<IActionResult> CheckIfPunchedIn(string userId)
        {
            DateTime date = DateTime.Today;
            var existingPunch = await _punch.GetPunchByUserAndDate(userId, date);
            if (existingPunch != null)
            {
                if(existingPunch.PunchOut > DateTime.Now)
                    return Ok(true);
            }
            return Ok(false);
        }

        [HttpGet("{punchId}")]
        public async Task<IActionResult> GetPunch(int punchId)
        {
            try
            {
                var punch = await _punch.GetPunch(punchId);
                if (punch == null)
                    return NotFound();
                punch.Duration = punch.PunchOut - punch.PunchIn;
                return Ok(punch);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreatePunch([FromBody] Punch newPunch)
        {
            try
            {
                if (newPunch.Date == null)
                {
                    newPunch.Date = DateTime.Today;
                }              
                var existingPunch = await _punch.GetPunchByUserAndDate(newPunch.ApplicationUserId, newPunch.Date.Value);

                if (existingPunch != null)
                {
                 
                    return Conflict($"Punch entry already exists for user {newPunch.ApplicationUserId} on {newPunch.Date.Value.ToShortDateString()}");
                }
                newPunch.PunchIn = DateTime.Now;
                newPunch.PunchOut = newPunch.PunchIn.GetValueOrDefault(DateTime.Now).AddHours(8);
                var createdPunch = await _punch.CreatePunch(newPunch);
                return CreatedAtAction(nameof(GetPunch), new { punchId = createdPunch.Id }, createdPunch);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }


        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdatePunch(string userId, [FromBody] Punch updatedPunch)
        {
            try
            {
                if (updatedPunch.Date == null)
                {
                    updatedPunch.Date = DateTime.Today;
                }
                var existingPunch = await _punch.GetPunchByUserAndDate(userId, updatedPunch.Date.Value);
                
                if (existingPunch == null)
                    return NotFound();
                existingPunch.PunchOut = DateTime.Now;
                var success = await _punch.UpdatePunch(existingPunch);
                if (success)
                    return Ok(success);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update Punch");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }



        [HttpGet("GetById/{userId}")]
        public async Task<IActionResult> GetUserPunch(string userId)
        {
            try
            {
                var userPunches = await _punch.GetUserById(userId);
                foreach (var p in userPunches)
                {
                    if (p.PunchOut > DateTime.Now)
                    {
                        p.PunchOut = null;
                    }
                    else if (p.PunchOut - p.PunchIn > new TimeSpan(7, 59, 59))
                    {
                        p.AttandanceStatus = (AttandanceStatus)1;
                    }
                    else if (p.PunchOut - p.PunchIn > new TimeSpan(3, 59, 59))
                    {
                        p.AttandanceStatus = (AttandanceStatus)3;
                    }
                    else
                    {
                        p.AttandanceStatus = (AttandanceStatus)2;
                    }
                }
                return Ok(userPunches);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}
