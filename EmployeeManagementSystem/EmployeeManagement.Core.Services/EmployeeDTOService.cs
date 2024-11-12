using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Core.DTO;
using Framework;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Core.Models;

namespace EmployeeManagement.Core.Services
{
    public class EmployeeDTOService(AppDBContext dbContext) : IEmployeeDTOService
    {
        public async Task<Result<IEnumerable<EmployeeWithDepartmentDto>>> GetAll()
        {
            try
            {
                var employeesWithDepartments = await dbContext.Employees
                    .Join(dbContext.Department,
                        employee => employee.DepartmentId,
                        department => department.Id,
                        (employee, department) => new EmployeeWithDepartmentDto
                        {
                            Id = employee.Id,
                            Name = employee.Name,
                            Email = employee.Email,
                            DepartmentName = department.Name,
                            DepartmentId= department.Id,
                            Salary = employee.Salary
                        })
                    .ToListAsync();

                if (employeesWithDepartments == null || !employeesWithDepartments.Any())
                {
                    return Result<IEnumerable<EmployeeWithDepartmentDto>>.Failure("No matching employees found.");
                }

                return Result<IEnumerable<EmployeeWithDepartmentDto>>.Success(employeesWithDepartments);
            }
            catch (Exception e)
            {
                return Result<IEnumerable<EmployeeWithDepartmentDto>>.Failure("An error occurred while fetching data" + e.Message);

            }


        }

        public async Task<Result<EmployeeWithDepartmentDto>> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Result<EmployeeWithDepartmentDto>.Failure("Invalid employee ID. ID must be greater than zero.");
                }

                var employee = await dbContext.Employees
                    .Join(dbContext.Department,
                        emp => emp.DepartmentId,
                        dept => dept.Id,
                        (emp, dept) => new EmployeeWithDepartmentDto
                        {
                            Id = emp.Id,
                            Name = emp.Name,
                            Email = emp.Email,
                            DepartmentName = dept.Name,
                            DepartmentId = dept.Id,
                            Salary = emp.Salary
                        })
                    .FirstOrDefaultAsync(e => e.Id == id); 
                
                if (employee != null)
                {
                    return Result<EmployeeWithDepartmentDto>.Success(employee);
                }
                else
                {
                    return Result<EmployeeWithDepartmentDto>.Failure("Employee not found.");
                }
            }
            catch (Exception e)
            {
                return Result<EmployeeWithDepartmentDto>.Failure("An error occurred while fetching data" + e.Message);

            }
        }

        public async Task<Result<EmployeeWithDepartmentDto>> GetByEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return Result<EmployeeWithDepartmentDto>.Failure("Invalid employee Email.");
                }

                var employee = await dbContext.Employees
                    .Join(dbContext.Department,
                        emp => emp.DepartmentId,
                        dept => dept.Id,
                        (emp, dept) => new EmployeeWithDepartmentDto
                        {
                            Id = emp.Id,
                            Name = emp.Name,
                            Email = emp.Email,
                            DepartmentName = dept.Name,
                            DepartmentId = dept.Id,
                            Salary = emp.Salary
                        })
                    .FirstOrDefaultAsync(e => e.Email == email); 
                
                if (employee != null)
                {
                    return Result<EmployeeWithDepartmentDto>.Success(employee);
                }

                else
                {
                    return Result<EmployeeWithDepartmentDto>.Failure("Employee not found.");
                }

            }
            catch (Exception e)
            {
                return Result<EmployeeWithDepartmentDto>.Failure("An error occurred while fetching data" + e.Message);

            }

        }

    }
}
