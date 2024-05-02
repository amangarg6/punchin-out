using System.ComponentModel.DataAnnotations;

namespace Login_Register.Models
{
    public class Register
    {    
        public string? UserName { get; set; }

        [EmailAddress]       
        public string? Email { get; set; }

        public string? Password { get; set; }
        public string? Role { get; set; }


    }
}
