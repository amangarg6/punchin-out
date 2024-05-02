using Attendance_Management.Data;
using Attendance_Management.Models;
using Attendance_Management.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Management.Repository
{
    public class Statusrepo:IStatusrepo
    {
        private readonly ApplicationDbContext _context;
        public Statusrepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Status> CreateStatus(Status Status)
        {
            var data = await _context.statuses.AddAsync(Status);
            await _context.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<bool> DeleteStatus(Status Status)
        {
            _context.statuses.Remove(Status);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Status>> GetStatus()
        {
              return await _context.statuses.ToListAsync();
        }

        public async Task<Status> GetStatus(int StatusId)
        {
            return await _context.statuses.FindAsync(StatusId);
        }

        public async Task<bool> GetStatusExists(int StatusId)
        {
            return await _context.statuses.AnyAsync(e => e.Id == StatusId);
        }

        public async Task<bool> GetStatusExists(string ProjectName)
        {
            return await _context.statuses.AnyAsync(e => e.Project == ProjectName);
        }

        public async Task<IEnumerable<Status>> GetUserById(string id)
        {
            var userAttandance = await _context.statuses.Where(x => x.ApplicationUserId == id).ToListAsync();
           return userAttandance;
        }
        public async Task<bool> save()
        {

            return await _context.SaveChangesAsync() == 1 ? true : false;
        }

        public Task<bool> UpdateStatus(Status Status)
        {
            _context.statuses.Update(Status);
            return save();
        }
    }
}
