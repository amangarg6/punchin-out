using Attendance_Management.Data;
using AutoMapper;
using Login_Register.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Attendance_Management.Models.ViewModel
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Register, ApplicationUser>().ReverseMap();

        }
    }
}
