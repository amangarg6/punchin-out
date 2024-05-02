using Attendance_Management.Data;
using Attendance_Management.Models;
using Attendance_Management.Models.ViewModel;
using Attendance_Management.Repository.IRepository;
using Login_Register.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace Attendance_Management.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserrepo _user;
        public UserController(IUserrepo user)
        {
            _user = user;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginVM user)
        {
            if (await _user.IsUnique(user.UserName)) return BadRequest("Please Register");
            var userAuthorize = await _user.AuthenticateUser(user.UserName, user.Password);
            if (userAuthorize == null) return NotFound("Invalid Attempt");
            return Ok(userAuthorize);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Register userRegister)
        {
            if (userRegister == null || !ModelState.IsValid) return BadRequest();          
            var applicationUser = new ApplicationUser
            {
                UserName = userRegister.UserName,
                PasswordHash = userRegister.Password,
                Email= userRegister.Email,   
                Role=userRegister.Role
            };
            if (!await _user.IsUnique(applicationUser.UserName)) return NotFound("Go to login");
            var registerUser = await _user.RegisterUser(applicationUser);
            if (!registerUser) return BadRequest("Register First");
            return Ok("Register Successfully");
        }

    }
}
