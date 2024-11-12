using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Core.Models;
using Framework;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Core.Services
{
    public class DepartmentService(AppDBContext dbContext) : IDepartmentService
    {
        public async Task<Result<Department>> Save(int id, Department department)
        {
            try
            {

                if (id != 0)
                {
                    department.Id = id;
                    dbContext.Department.Update(department);
                    await dbContext.SaveChangesAsync();
                    return Result<Department>.Success(department);
                }
                else
                {
                    dbContext.Department.Add(department);
                    await dbContext.SaveChangesAsync();
                    return Result<Department>.Success(department);
                }
             
            }
            catch (Exception e)
            {
                return Result<Department>.Failure("An error occurred while creating department. " + e.Message);

            }
        }

        public async Task<Result<bool>> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Result<bool>.Failure("Invalid department ID.");
                }

                var department = await dbContext.Department.FindAsync(id);

                if (department != null)
                {
                    dbContext.Department.Remove(department);
                    await dbContext.SaveChangesAsync();
                    return Result<bool>.Success(true);

                }
                else
                {
                    return Result<bool>.Failure("department not found.");
                }
            }
            catch (Exception e)
            {
                return Result<bool>.Failure("An error occurred while deleting an department" + e.Message);
            }
        }

        public async Task<Result<IEnumerable<Department>>> GetAll(string keyword = "")
        {
            try
            {
                if (keyword == null)
                {
                    return Result<IEnumerable<Department>>.Failure("Search keyword cannot be null.");
                }

                keyword = keyword.Trim().ToLower();

                var department = await dbContext.Department
                    .Where(d =>
                        string.IsNullOrEmpty(keyword) ||
                        d.Name.ToLower().Contains(keyword) ||
                        d.Id.ToString().Contains(keyword)
                    )
                    .ToListAsync();

                if (department == null || !department.Any())
                {
                    return Result<IEnumerable<Department>>.Failure("No matching department found.");
                }

                return Result<IEnumerable<Department>>.Success(department);
            }
            catch (Exception e)
            {
                return Result<IEnumerable<Department>>.Failure("An error occurred while fetching data" + e.Message);
            }
        }

        public async Task<Result<Department>> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return Result<Department>.Failure("Invalid department ID. ID must be greater than zero.");
                }

                var department = await dbContext.Department.FindAsync(id);
                if (department != null)
                {
                    return Result<Department>.Success(department);
                }
                else
                {
                    return Result<Department>.Failure("Department not found.");
                }
            }
            catch (Exception e)
            {
                return Result<Department>.Failure("An error occurred while fetching data" + e.Message);

            }

        }

    }
}
