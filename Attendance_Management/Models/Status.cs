using Attendance_Management.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Management.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string? Project { get; set; }
        public string? Module { get; set; }
        public  string? Profile { get; set; }
        public DateTime? Date { get; set; }
        public string? Memo { get; set; }
        [ForeignKey("ApplicationUser")]
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
       
    }
}
