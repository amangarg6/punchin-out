using Attendance_Management.Data;
using Attendance_Management.Models;
using Attendance_Management.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Management.Repository
{
    public class Punchrepo : IPunchrepo
    {
        private readonly ApplicationDbContext _context;
        public Punchrepo(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<Punch> CreatePunch(Punch Punch)
        {
            var data = await _context.punches.AddAsync(Punch);
            await _context.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<bool> DeletePunch(Punch Punch)
        {
            _context.punches.Remove(Punch);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Punch>> GetPunch()
        {
            return await _context.punches.ToListAsync();
        }

        public async Task<Punch> GetPunch(int PunchId)
        {
            return await _context.punches.FindAsync(PunchId);
        }


        public async Task<Punch> GetPunchByUserAndDate(string userId, DateTime date)
        {
            DateTime startDate = date.Date;
            DateTime endDate = startDate.AddDays(1).AddTicks(-1);
            return await _context.punches.FirstOrDefaultAsync(e => e.ApplicationUserId == userId && e.PunchIn >= startDate && e.PunchIn < endDate);
        }

        public async Task<bool> GetPunchExists(int PunchId)
        {
            return await _context.punches.AnyAsync(e => e.Id == PunchId);
        }

        public async Task<IEnumerable<Punch>> GetUserById(string id)
        {
            var userAttandance = await _context.punches.Where(x => x.ApplicationUserId == id).ToListAsync();
            return userAttandance;
        }

        public async Task<bool> save()
        {
            return await _context.SaveChangesAsync() == 1 ? true : false;
        }

        public Task<bool> UpdatePunch(Punch Punch)
        {
            _context.punches.Update(Punch);
            return save();
        }
    }
}
