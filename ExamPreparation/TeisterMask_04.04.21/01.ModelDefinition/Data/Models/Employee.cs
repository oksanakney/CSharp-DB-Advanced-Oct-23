using System.ComponentModel.DataAnnotations;
using TeisterMask.Common;

namespace TeisterMask.Data.Models
{
    public class Employee
    {
        public Employee()
        {
            this.EmployeesTasks = new HashSet<EmployeeTask>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(ValidationConstants.UsernameMaxLength)]
        public string Username { get; set; } = null!;
        // email to validate!!! there is an attribute for it
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        [MaxLength(ValidationConstants.PhoneLength)]
        public string Phone { get; set; } = null!;
        // EmployeesTasks – collection of type EmployeeTask
        public virtual ICollection<EmployeeTask> EmployeesTasks { get; set; }
    }
}
