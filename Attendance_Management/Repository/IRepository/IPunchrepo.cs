using Attendance_Management.Data;
using Attendance_Management.Models;

namespace Attendance_Management.Repository.IRepository
{
    public interface IPunchrepo
    {
        Task<ICollection<Punch>> GetPunch();
        Task<Punch> GetPunch(int PunchId);
        Task<bool> GetPunchExists(int PunchId);       
        Task<Punch> CreatePunch(Punch Punch);
        Task<bool> UpdatePunch(Punch Punch);
        Task<bool> DeletePunch(Punch Punch);
        Task<bool> save();
        Task<IEnumerable<Punch>> GetUserById(string id);
        Task<Punch> GetPunchByUserAndDate(string userId, DateTime date); 

    }
}

