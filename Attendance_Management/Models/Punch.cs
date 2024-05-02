using Attendance_Management.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Management.Models
{
    public class Punch
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }

        public DateTime? PunchIn { get; set; }

        public DateTime? PunchOut { get; set; }
      
        [NotMapped]
        public TimeSpan? Duration { get; set; }

        [ForeignKey("ApplicationUser")]
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public AttandanceStatus AttandanceStatus { get; set; }
    }
        public enum AttandanceStatus
        {
            L = 0,
            P = 1,
            A = 2,
          HFD = 3
        }
}
