﻿using AutoMapper;
using EntityFramworkProject.DTOs;
using EntityFramworkProject.Models;
using EntityFramworkProject.Repository;
using Microsoft.EntityFrameworkCore;

namespace EntityFramworkProject.Services
{
    public class EmployeeService : ICommonService<EmployeeDTO, EmployeeInsertDTO, EmployeeUpdateDTO>
    {
        private IRepository<Employee> _employeeRepository;
        private IMapper _mapper;
        public List<string> Errors { get; }

        public EmployeeService(IRepository<Employee> repository, IMapper mapper) 
        { 
            _employeeRepository = repository;
            _mapper = mapper;   
            Errors = new List<string>();
        }

        public async Task<IEnumerable<EmployeeDTO>> Get()
        {
            var employees = await _employeeRepository.Get();

            return employees.Select(e => _mapper.Map<EmployeeDTO>(e));
        }

        public async Task<EmployeeDTO> GetById(int id)
        {
            var employee = await _employeeRepository.GetById(id);

            if (employee != null)
            {
                var employeeDTO = _mapper.Map<EmployeeDTO>(employee);
                return employeeDTO;
            }
            return null;
        }

        public async Task<EmployeeDTO> Add(EmployeeInsertDTO employeeInsertDTO)
        {
            var employee = _mapper.Map<Employee>(employeeInsertDTO);

            await _employeeRepository.Add(employee);
            await _employeeRepository.Save();

            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);

            return employeeDTO;
        }

        public async Task<EmployeeDTO> Update(int id, EmployeeUpdateDTO employeeUpdateDTO)
        {
            var employee = await _employeeRepository.GetById(id);

            if (employee != null)
            {
                /*
                employee.Cod = employeeUpdateDTO.Cod;
                employee.Name = employeeUpdateDTO.Name;
                employee.LastName = employeeUpdateDTO.LastName;
                employee.Position = employeeUpdateDTO.Position;
                employee.Department = employeeUpdateDTO.Department.Length > 0 ? employeeUpdateDTO.Department : employee.Department;
                */
                employee = _mapper.Map<EmployeeUpdateDTO, Employee>(employeeUpdateDTO, employee);

                _employeeRepository.Update(employee);
                await _employeeRepository.Save();

                var employeeDTO = _mapper.Map<EmployeeDTO>(employee);

                return employeeDTO;
            }

            return null;
        }
        
        public async Task<EmployeeDTO> Delete(int id)
        {
            var employee = await _employeeRepository.GetById(id);

            if (employee != null)
            {
                var employeeDTO = _mapper.Map<EmployeeDTO>(employee);

                _employeeRepository.Delete(employee);
                await _employeeRepository.Save();

                return employeeDTO;
            }

            return null;

        }

        public bool Validate(EmployeeInsertDTO dto)
        {
            if (_employeeRepository.Search(e => e.Name == dto.Name).Count() > 0)
            {
                Errors.Add("No se puede aniadir un empleado con el mismo nombre de otro existente.");
                return false;
            }
            return true;
        }

        public bool Validate(EmployeeUpdateDTO dto)
        {
            if (_employeeRepository.Search(e => e.Name == dto.Name && dto.Id != e.Id).Count() > 0)
            {
                Errors.Add("No se puede editar un empleado con el mismo nombre de otro existente.");
                return false;
            }
            return true;
        }
    }
}
