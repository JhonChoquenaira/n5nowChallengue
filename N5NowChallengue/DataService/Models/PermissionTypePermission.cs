using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5NowChallengue.DataService.Models
{
    public class PermissionTypePermission
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("PermissionType")]
        public int PermissionTypeId { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Permission")]
        public int PermissionId { get; set; }

        public virtual PermissionType PermissionType { get; set; }
        public virtual Permission Permission { get; set; }
    }
}