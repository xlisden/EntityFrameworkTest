using EntityFramworkProject.DTOs;
using EntityFramworkProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFramworkProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildrenController : ControllerBase
    {
        private ProgramContext _context;

        public ChildrenController(ProgramContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<ChildrenDTO>> Get() =>
            await _context.Childrens
            .Select(c => new ChildrenDTO 
                {
                    Name = c.Name,
                    LastName = c.LastName,
                    BirthDay = c.BirthDay,
                    ParentId = c.ParentId,
                    Parent = new EmployeeDTO
                    {
                        Cod = c.Parent.Cod,
                        Name = c.Parent.Name,
                        LastName = c.Parent.LastName,
                        Department = c.Parent.Department,
                        Position = c.Parent.Position
                    }
            }).ToListAsync();


    }

}
