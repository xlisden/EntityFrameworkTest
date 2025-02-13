using EntityFramworkProject.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramworkProject.Services
{
    public interface ICommonService<T, TI, TU>
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TI InsertDTO);
        Task<T> Update(int id, TU employeeUpdateDTO);
        Task<T> Delete(int id);

    }
}
