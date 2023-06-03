using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using N5NowChallengue.DataService.Models.Types;

namespace N5NowChallengue.DataService.Models
{
    public class EmployeePermission
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Permission")]
        public int PermissionId { get; set; }

        public StatusEmployeePermission RequestedStatus { get; set; }
        public int ApprovedBy { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Permission Permission { get; set; }
    }
}