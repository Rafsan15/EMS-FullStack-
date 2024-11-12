using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EmployeeManagement.Core.Models
{
    public class Employee
    {
        [Required]
        [Key]
        public int Id {get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name {get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email {get; set; }

        [Required]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        [JsonIgnore]
        public Department? Department { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Salary must be greater than 0.")]
        public double Salary { get; set; }
    }


}



