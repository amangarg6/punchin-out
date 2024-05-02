using Attendance_Management.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Management.Models
{
    public class Leave
    {
        public int Id { get; set; }
        public  DateTime Date { get; set; }
        public  bool IsLeave { get; set; }
        [ForeignKey("ApplicationUser")]
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public AttandanceStatusl AttandanceStatusl { get; set; }
    }
    public enum AttandanceStatusl
    {
        L = 0      
    }
}

