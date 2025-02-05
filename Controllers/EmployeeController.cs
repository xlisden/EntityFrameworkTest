using EntityFramworkProject.DTOs;
using EntityFramworkProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFramworkProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private ProgramContext _context;

        public EmployeeController(ProgramContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeDTO>> Get() => 
            await _context.Employees.Select(e => new EmployeeDTO
                {
                    Cod = e.Cod,
                    Name = e.Name,
                    Position = e.Position,
                    Department = e.Department
                }).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }

            var employeeDTO = new EmployeeDTO
            {
                Cod = employee.Cod,
                Name = employee.Name,
                LastName = employee.LastName,
                Position = employee.Position,
                Department = employee.Department
            };
            return Ok(employeeDTO);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> Add(EmployeeInsertDTO employeeInsertDTO)
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
                Cod = employee.Cod,
                Name = employee.Name,
                LastName = employee.LastName,
                Position = employee.Position,
                Department = employee.Department
            };
            Console.WriteLine("employee " + employee.Id);

            return CreatedAtAction(nameof(GetById), new { id = employee.Id}, employeeDTO);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeDTO>> Update(int id, EmployeeUpdatetDTO employeeUpdatetDTO)
        {
            var employee = await _context.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }

            employee.Cod = employeeUpdatetDTO.Cod;
            employee.Name = employeeUpdatetDTO.Name;
            employee.LastName = employeeUpdatetDTO.LastName;
            employee.Position  = employeeUpdatetDTO.Position;
            employee.Department = employeeUpdatetDTO.Department.Length > 0 ? employeeUpdatetDTO.Department : employee.Department;
            await _context.SaveChangesAsync();

            var employeeDTO = new EmployeeDTO
            {
                Cod = employee.Cod,
                Name = employee.Name,
                LastName = employee.LastName,
                Position = employee.Position,
                Department = employee.Department
            };

            return Ok(employeeDTO);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete (int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
