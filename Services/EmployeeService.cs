using EntityFramworkProject.DTOs;
using EntityFramworkProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramworkProject.Services
{
    public class EmployeeService : ICommonService<EmployeeDTO, EmployeeInsertDTO, EmployeeUpdateDTO>
    {
        private ProgramContext _context;

        public EmployeeService(ProgramContext context) 
        { 
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDTO>> Get() =>
             await _context.Employees.Select(e => new EmployeeDTO
             {
                 Id = e.Id,
                 Cod = e.Cod,
                 Name = e.Name,
                 Position = e.Position,
                 Department = e.Department
             }
             ).ToListAsync();

        public async Task<EmployeeDTO> GetById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                var employeeDTO = new EmployeeDTO
                {
                    Id = employee.Id,
                    Cod = employee.Cod,
                    Name = employee.Name,
                    LastName = employee.LastName,
                    Position = employee.Position,
                    Department = employee.Department
                };

                return employeeDTO;
            }

            return null;
        }

        public async Task<EmployeeDTO> Add(EmployeeInsertDTO employeeInsertDTO)
        {
            var employee = new Employee
            {
                Cod = employeeInsertDTO.Cod,
                Name = employeeInsertDTO.Name,
                LastName = employeeInsertDTO.LastName,
                Department = employeeInsertDTO.Department,
                Position = employeeInsertDTO.Position
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            var employeeDTO = new EmployeeDTO
            {
                Id = employee.Id,
                Cod = employee.Cod,
                Name = employee.Name,
                LastName = employee.LastName,
                Position = employee.Position,
                Department = employee.Department
            };

            return employeeDTO;
        }
        public async Task<EmployeeDTO> Update(int id, EmployeeUpdateDTO employeeUpdatetDTO)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee != null)
            {
                employee.Cod = employeeUpdatetDTO.Cod;
                employee.Name = employeeUpdatetDTO.Name;
                employee.LastName = employeeUpdatetDTO.LastName;
                employee.Position = employeeUpdatetDTO.Position;
                employee.Department = employeeUpdatetDTO.Department.Length > 0 ? employeeUpdatetDTO.Department : employee.Department;

                await _context.SaveChangesAsync();

                var employeeDTO = new EmployeeDTO
                {
                    Id = employee.Id,
                    Cod = employee.Cod,
                    Name = employee.Name,
                    LastName = employee.LastName,
                    Position = employee.Position,
                    Department = employee.Department
                };

                return employeeDTO;
            }

            return null;
        }

        public async Task<EmployeeDTO> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee != null)
            {
                var employeeDTO = new EmployeeDTO
                {
                    Cod = employee.Cod,
                    Name = employee.Name,
                    LastName = employee.LastName,
                    Position = employee.Position,
                    Department = employee.Department
                };

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                return employeeDTO;
            }

            return null;

        }


    }
}
