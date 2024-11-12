using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Core.DTO;
using EmployeeManagement.Core.Models;

namespace EmployeeManagement.Core.Interfaces
{
    public interface IEmployeeDTOService
    {
        Task<Result<IEnumerable<EmployeeWithDepartmentDto>>> GetAll();
        Task<Result<EmployeeWithDepartmentDto>> GetById(int id);
        Task<Result<EmployeeWithDepartmentDto>> GetByEmail(string email);



    }
}
