using EntityFramworkProject.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramworkProject.Services
{
    public interface ICommonService<T, TI, TU>
    {
        public List<string> Errors { get; }
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TI InsertDTO);
        Task<T> Update(int id, TU employeeUpdateDTO);
        Task<T> Delete(int id);
        bool Validate(TI dto);
        bool Validate(TU dto);

    }
}
