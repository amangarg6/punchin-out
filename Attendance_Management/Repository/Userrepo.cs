using Attendance_Management.Data;
using Attendance_Management.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Attendance_Management.Repository
{
    public class Userrepo : IUserrepo
    {
        private readonly SignInManager<ApplicationUser> _signInManager;      
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppSettings _appSettings;
        public Userrepo(SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager,
             IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;         
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        public async Task<ApplicationUser?> AuthenticateUser(string userName, string userPassword)
        {
            var userExist = await _userManager.FindByNameAsync(userName);
            var userVerification = await _signInManager.CheckPasswordSignInAsync(userExist, userPassword, false);
            if (!userVerification.Succeeded) return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userExist.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userExist.Token = tokenHandler.WriteToken(token);

            return userExist;
        }


        public async Task<bool> IsUnique(string userName)
        {
            var userExist = await _userManager.FindByNameAsync(userName);
            if (userExist == null) return true;
            return false;
        }

        public async Task<bool> RegisterUser(ApplicationUser user)
        {
            var users = await _userManager.CreateAsync(user, user.PasswordHash);
            if (!users.Succeeded) return false;          
            return true;
        }
    }
}
