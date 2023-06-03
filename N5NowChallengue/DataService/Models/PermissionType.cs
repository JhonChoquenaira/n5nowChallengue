using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N5NowChallengue.DataService.Models
{
    public class PermissionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermissionTypeId { get; set; }
        [Required] [StringLength(185)] public string PermissionTypeName { get; set; }
        [Required] [StringLength(25)] public string PermissionTypeDescription { get; set; }
        public bool Status { get; set; }
    }
}