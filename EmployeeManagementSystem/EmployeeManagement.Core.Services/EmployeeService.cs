using Framework;
using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Core.Models;
using EmployeeManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Core.Services
{
    public class EmployeeService(AppDBContext dbContext) : IEmployeeService
    {
        public async Task<Result<Employee>> Save(int id, Employee employee)
        {

            try
            {
                if (employee == null)
                {
                    return Result<Employee>.Failure("Employee object is null.");
                }

                if (string.IsNullOrWhiteSpace(employee.Name) ||
                    string.IsNullOrWhiteSpace(employee.Email) || 
                    !new EmailAddressAttribute().IsValid(employee.Email) ||
                    employee.Salary <= 0)
                {
                    return Result<Employee>.Failure("Employee object contains invalid or missing data.");
                }

                if (id != 0)
                {
                    employee.Id=id;
                    dbContext.Employees.Update(employee);
                    await dbContext.SaveChangesAsync();
                    return Result<Employee>.Success(employee);
                }
                else
                {
                    dbContext.Employees.Add(employee);
                    await dbContext.SaveChangesAsync();
                    return Result<Employee>.Success(employee);
                }
               
            }
            catch (Exception e)
            {
                return Result<Employee>.Failure("An error occurred while creating employee" + e.Message);

            }


        }

        public async Task<Result<bool>> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Result<bool>.Failure("Invalid employee ID.");
                }

                var employee = await dbContext.Employees.FindAsync(id);
               
                if (employee != null)
                {
                    dbContext.Employees.Remove(employee);
                    await dbContext.SaveChangesAsync();
                    return Result<bool>.Success(false);

                }
                else
                {
                    return Result<bool>.Failure("Employee not found.");
                }
            }
            catch (Exception e)
            {
                return Result<bool>.Failure("An error occurred while deleting an employee" + e.Message);
            }


        }

        public async Task<Result<IEnumerable<Employee>>> GetAll(string keyword = "")
        {
            try
            {
                if (keyword == null)
                {
                    return Result<IEnumerable<Employee>>.Failure("Search keyword cannot be null.");
                }

                keyword = keyword.Trim().ToLower();

                var employees = await dbContext.Employees
                    .Where(e =>
                        string.IsNullOrEmpty(keyword) ||
                        e.Name.ToLower().Contains(keyword) ||
                        e.Email.ToLower().Contains(keyword) ||
                        e.Id.ToString().Contains(keyword) ||
                        e.Salary.ToString().Contains(keyword)
                    )
                    .ToListAsync();

                if (employees == null || !employees.Any())
                {
                    return Result<IEnumerable<Employee>>.Failure("No matching employees found.");
                }

                return Result<IEnumerable<Employee>>.Success(employees);
            }
            catch (Exception e)
            {
                return Result<IEnumerable<Employee>>.Failure("An error occurred while fetching data" + e.Message);
            }
        }

        public async Task<Result<Employee>> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Result<Employee>.Failure("Invalid employee ID. ID must be greater than zero.");
                }

                var employee = await dbContext.Employees.FindAsync(id);
                if (employee != null)
                {
                    return Result<Employee>.Success(employee);
                }
                else
                {
                    return Result<Employee>.Failure("Employee not found.");
                }
            }
            catch (Exception e)
            {
                return Result<Employee>.Failure("An error occurred while fetching data" + e.Message);

            }

        }

    }
}
