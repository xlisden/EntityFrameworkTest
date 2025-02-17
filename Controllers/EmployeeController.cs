using EntityFramworkProject.DTOs;
using EntityFramworkProject.Models;
using EntityFramworkProject.Services;
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
        private IValidator<EmployeeInsertDTO> _employeeInsertValidator;
        private IValidator<EmployeeUpdateDTO> _employeeUpdateValidator;
        private ICommonService<EmployeeDTO, EmployeeInsertDTO, EmployeeUpdateDTO> _employeeService;

        public EmployeeController(IValidator<EmployeeInsertDTO> validator,
                                  IValidator<EmployeeUpdateDTO> employeeUpdateValidator,
                                  [FromKeyedServices("employeeService")] ICommonService<EmployeeDTO, EmployeeInsertDTO, EmployeeUpdateDTO> employeeService)
        {
            _employeeInsertValidator = validator;
            _employeeUpdateValidator = employeeUpdateValidator;
            _employeeService = employeeService;
        }


        [HttpGet]
        public async Task<IEnumerable<EmployeeDTO>> Get() =>
            await _employeeService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetById(int id)
        {
            var employeeDTO = await _employeeService.GetById(id);

            return employeeDTO == null ? NotFound(): Ok(employeeDTO);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> Add(EmployeeInsertDTO employeeInsertDTO)
        {
            var validationResult = await _employeeInsertValidator.ValidateAsync(employeeInsertDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var employeeDTO = await _employeeService.Add(employeeInsertDTO);

            return CreatedAtAction(nameof(GetById), new { id = employeeDTO.Id}, employeeDTO);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeDTO>> Update(int id, EmployeeUpdateDTO employeeUpdateDTO)
        {
            var validationResult = await _employeeUpdateValidator.ValidateAsync(employeeUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var employeeDTO = await _employeeService.Update(id, employeeUpdateDTO);

            return employeeDTO == null ? NotFound() : Ok(employeeDTO);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeDTO>> Delete (int id)
        {
            var employeeDTO = await _employeeService.Delete(id);

            return employeeDTO == null ? NotFound(): Ok(employeeDTO);
        }


    }
}
