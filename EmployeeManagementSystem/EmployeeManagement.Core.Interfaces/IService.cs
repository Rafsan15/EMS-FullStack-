using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework;

namespace EmployeeManagement.Core.Interfaces
{
    public interface IService<T>
    {
        Task<Result<T>> Save(int id, T Entity);
        Task<Result<bool>> Delete(int id);
        Task<Result<IEnumerable<T>>> GetAll(string keyword = "");
        Task<Result<T>> GetById(int id);

    }
}