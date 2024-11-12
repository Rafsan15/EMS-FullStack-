namespace EmployeeManagement.Core.DTO
{
    public class EmployeeWithDepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public double Salary { get; set; }
    }
}
