using AutoMapper;
using EntityFramworkProject.DTOs;
using EntityFramworkProject.Models;

namespace EntityFramworkProject.Automappers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeInsertDTO, Employee>();
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeUpdateDTO, Employee>();
        }
    }
}
