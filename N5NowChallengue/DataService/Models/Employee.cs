using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using N5NowChallengue.DataService.Models.Types;

namespace N5NowChallengue.DataService.Models
{
    public class Employee
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        [Required] [StringLength(85)] public string Username { get; set; }
        [Required] [StringLength(185)] public string Password { get; set; }
        [Required] public EmployeeType EmployeeType { get; set; }
        public bool Status { get; set; }
        [StringLength(256)] public string Email { get; set; }
    }
}