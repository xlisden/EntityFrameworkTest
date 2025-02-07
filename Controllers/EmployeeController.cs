using EntityFramworkProject.DTOs;
using EntityFramworkProject.Models;
using EntityFramworkProject.Validators;
using FluentValidation;
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
        private IValidator<EmployeeInsertDTO> _employeeInsertValidator;
        private IValidator<EmployeeUpdatetDTO> _employeeUpdateValidator;

        public EmployeeController(ProgramContext context, IValidator<EmployeeInsertDTO> validator, IValidator<EmployeeUpdatetDTO> employeeUpdateValidator)
        {
            _context = context;
            _employeeInsertValidator = validator;
            _employeeUpdateValidator = employeeUpdateValidator;
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
            var validationResult = await _employeeInsertValidator.ValidateAsync(employeeInsertDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

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
            var validationResult = await _employeeUpdateValidator.ValidateAsync(employeeUpdatetDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

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
