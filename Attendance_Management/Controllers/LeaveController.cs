using Attendance_Management.Migrations;
using Attendance_Management.Models;
using Attendance_Management.Repository;
using Attendance_Management.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Leave = Attendance_Management.Models.Leave;

namespace Attendance_Management.Controllers
{
    [Route("api/leave")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaverepo _leaverepo;
        public LeaveController(ILeaverepo leaverepo)
        {
            _leaverepo = leaverepo;
        }

        [HttpGet("{LeaveId:int}")]
        public async Task<IActionResult> GetLeave(int LeaveId)
        {
            var Leave = await _leaverepo.GetLeave(LeaveId);
            if (Leave == null) return NotFound();
            return Ok(Leave);
        }
        [HttpPost]
        public async Task<IActionResult> CreateLeave([FromBody] Leave Leave)
        {
            if (Leave == null) return NotFound();
            if (Leave.Date == null)
            {
                Leave.Date = DateTime.Today;
            }
            if (!ModelState.IsValid) return BadRequest(Leave);
            await _leaverepo.CreateLeave(Leave);
            await _leaverepo.save();

            return Ok(Leave);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLeave([FromBody] Leave Leave)
        {
            if (Leave == null) return NotFound();
            if (Leave.Date == null)
            {
                Leave.Date = DateTime.Today;
            }
            if (!ModelState.IsValid) return BadRequest(Leave);
            await _leaverepo.UpdateLeave(Leave);
            await _leaverepo.save();

            return Ok(Leave);
        }


        [HttpDelete("{Leaveid:int}")]
        public async Task<IActionResult> DeleteLeave(int Leaveid)
        {
            if (!await _leaverepo.GetLeaveExists(Leaveid)) return NotFound();
            var leave = await _leaverepo.GetLeave(Leaveid);
            if (leave == null) return NotFound();
            if (!await _leaverepo.DeleteLeave(leave))
            {            
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(new { Status = "Success", Message = "leave Delete successfully!" });
        }


        [HttpGet("GetById/{userId}")]
        public async Task<IActionResult> GetUserLeave(string userId)
        {
            try
            {
                var userleaves = await _leaverepo.GetLeaveEntriesByUserId(userId);              
                foreach (var leaveEntry in userleaves)
                {
                    if (leaveEntry.Date.Date > DateTime.Now.Date)
                    {                       
                        leaveEntry.AttandanceStatusl = AttandanceStatusl.L;
                    }
                }
                return Ok(userleaves);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

    }
}
