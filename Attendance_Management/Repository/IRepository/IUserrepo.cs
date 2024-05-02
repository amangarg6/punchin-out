using Attendance_Management.Data;

namespace Attendance_Management.Repository.IRepository
{
    public interface IUserrepo
    {
        Task<bool> IsUnique(string userName);
        Task<ApplicationUser?> AuthenticateUser(string userName, string userPassword);
        Task<bool> RegisterUser(ApplicationUser user);

       

    }
}
