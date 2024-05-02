using Attendance_Management.Models;

namespace Attendance_Management.Repository.IRepository
{
    public interface ILeaverepo
    {
        Task<ICollection<Leave>> GetLeave();
        Task<Leave> GetLeave(int LeaveId);
        Task<bool> GetLeaveExists(int LeaveId);
        Task<Leave> CreateLeave(Leave Leave);
        Task<bool> UpdateLeave(Leave Leave);
        Task<bool> DeleteLeave(Leave Leave);
        Task<bool> save();
        Task<IEnumerable<Leave>> GetLeaveEntriesByUserId(string id);
    }
}
