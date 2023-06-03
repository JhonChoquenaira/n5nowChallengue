using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using N5NowChallengue.DataService.Models.Types;

namespace N5NowChallengue.DataService.Models
{
    public class EmployeePermissionType
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("PermissionType")]
        public int PermissionTypeId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual PermissionType PermissionType { get; set; }
    }
}