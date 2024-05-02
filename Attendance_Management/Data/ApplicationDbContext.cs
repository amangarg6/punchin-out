using Attendance_Management.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Attendance_Management.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {

        }
        public DbSet<Punch> punches { get; set; }
        public  DbSet<Status>statuses { get; set; }
        public DbSet<Leave> leaves { get; set; }
    }
}
