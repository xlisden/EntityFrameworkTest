using EntityFramworkProject.DTOs;
using EntityFramworkProject.Models;
using EntityFramworkProject.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFramworkProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildrenController : ControllerBase
    {
        private IValidator<ChildrenInsertDTO> _childrenInsertValidator;
        private IValidator<ChildrenUpdateDTO> _childrenUpdateValidator;
        private ICommonService<ChildrenDTO, ChildrenInsertDTO, ChildrenUpdateDTO> _childrenService;

        public ChildrenController(IValidator<ChildrenUpdateDTO> childrenUpdateValidator, IValidator<ChildrenInsertDTO> childrenInsertValidator,
            [FromKeyedServices("childrenService")] ICommonService<ChildrenDTO, ChildrenInsertDTO, ChildrenUpdateDTO> childrenService)
        {
            _childrenInsertValidator =  childrenInsertValidator;
            _childrenUpdateValidator = childrenUpdateValidator;
            _childrenService = childrenService;
        }

        [HttpGet]
        public async Task<IEnumerable<ChildrenDTO>> Get() 
            => await _childrenService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<ChildrenDTO>> GetById(int id)
        {
            var childrenDTO = await _childrenService.GetById(id);

            return childrenDTO == null ? NotFound() : Ok(childrenDTO);
        }


    }

}
