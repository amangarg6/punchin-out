using Attendance_Management.Data;
using Attendance_Management.Models;
using Attendance_Management.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Management.Repository
{
    public class Leaverepo:ILeaverepo
    {
        private readonly ApplicationDbContext _context;
        public Leaverepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Leave> CreateLeave(Leave Leave)
        {
            var data = await _context.leaves.AddAsync(Leave);
            await _context.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<bool> DeleteLeave(Leave Leave)
        {
            _context.leaves.Remove(Leave);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Leave>> GetLeave()
        {
            return await _context.leaves.ToListAsync();
        }

        public async Task<Leave> GetLeave(int LeaveId)
        {
            return await _context.leaves.FindAsync(LeaveId);
        }

        public async Task<bool> GetLeaveExists(int LeaveId)
        {
            return await _context.leaves.AnyAsync(e => e.Id == LeaveId);
        }

        public async Task<IEnumerable<Leave>> GetLeaveEntriesByUserId(string id)
        {
            var userAttandance = await _context.leaves.Where(x => x.ApplicationUserId == id).ToListAsync();
            return userAttandance;
        }

    public async Task<bool> save()
        {
            return await _context.SaveChangesAsync() == 1 ? true : false;
        }

        public Task<bool> UpdateLeave(Leave Leave)
        {
            _context.leaves.Update(Leave);
            return save();
        }
    }
}
