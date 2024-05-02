using Attendance_Management.Models;

namespace Attendance_Management.Repository.IRepository
{
    public interface IStatusrepo
    {
        Task<ICollection<Status>> GetStatus();
        Task<Status> GetStatus(int StatusId);
        Task<bool> GetStatusExists(int StatusId);
        Task<bool> GetStatusExists(string ProjectName);
        Task<Status> CreateStatus(Status Status);
        Task<bool> UpdateStatus(Status Status);
        Task<bool> DeleteStatus(Status Status);
        Task<bool> save();
        Task<IEnumerable<Status>> GetUserById(string id);
    }
}
