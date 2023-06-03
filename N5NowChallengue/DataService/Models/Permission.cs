using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5NowChallengue.DataService.Models
{
    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermissionId { get; set; }
        [Required] [StringLength(85)] public string PermissionCode { get; set; }
        [Required] [StringLength(185)] public string PermissionName { get; set; }
        [Required] [StringLength(25)] public string Method { get; set; }
        public bool Status { get; set; }

    }
}