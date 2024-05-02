
using Attendance_Management.Models;
using Attendance_Management.Repository;
using Attendance_Management.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Attendance_Management.Controllers
{
    [Route("api/status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusrepo _statusrepo;
        public StatusController(IStatusrepo statusrepo)
        {
            _statusrepo = statusrepo;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var statuses = await _statusrepo.GetStatus();
            return Ok(statuses);
        }
        [HttpGet("{statusId:int}")]
        public async Task<IActionResult> GetStatus(int statusId)
        {
            var status = await _statusrepo.GetStatus(statusId);
            if (status == null) return NotFound();
            return Ok(status);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] Status status)
        {
            if (status == null) return NotFound();          
            if (status.Date == null)
            {
                status.Date = DateTime.Today;
            }
            if (!ModelState.IsValid) return BadRequest(status);
            await _statusrepo.CreateStatus(status);
            await _statusrepo.save();
            return Ok(status);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStatus([FromBody] Status status)
        {
            if (status == null) return NotFound();
            if (status.Date == null)
            {
                status.Date = DateTime.Today;
            }

            if (!ModelState.IsValid) return BadRequest(status);
            await _statusrepo.UpdateStatus(status);
            await _statusrepo.save();

            return Ok(status);
        }


        [HttpDelete("{statusid:int}")]
        public async Task<IActionResult> DeleteStatus(int statusid)
        {
            if (!await _statusrepo.GetStatusExists(statusid)) return NotFound();
            var status = await _statusrepo.GetStatus(statusid);
            if (status == null) return NotFound();
            if (!await _statusrepo.DeleteStatus(status))
            {
                ModelState.AddModelError("", $"something went wrong while Delete Status:{status.Project}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(new { Status = "Success", Message = "Status Delete successfully!" });
        }

        [HttpGet("GetById/{userId}")]
        public async Task<IActionResult> GetUserStatus(string userId)
        {
            try
            {
                var userPunches = await _statusrepo.GetUserById(userId);          
                return Ok(userPunches);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}
