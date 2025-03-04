using AutoMapper;
using EntityFramworkProject.DTOs;
using EntityFramworkProject.Models;
using EntityFramworkProject.Repository;

namespace EntityFramworkProject.Services
{
    public class ChildrenService: ICommonService<ChildrenDTO, ChildrenInsertDTO, ChildrenUpdateDTO>
    {
        private IRepository<Children> _childrenRepository;
        private IRepository<Employee> _employeeRepository;
        private IMapper _mapper;
        public List<string> Errors { get; }

        public ChildrenService(IRepository<Children> repository, IMapper mapper, IRepository<Employee> repositoryE) 
        {
            _childrenRepository = repository;
            _employeeRepository = repositoryE;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<ChildrenDTO>> Get()
        {
            var children = await _childrenRepository.Get();
            
            return children.Select(c => _mapper.Map<ChildrenDTO>(c));
        }

        public async Task<ChildrenDTO> GetById(int id)
        {
            var children = await _childrenRepository.GetById(id);

            if (children != null)
            {
                var childrenDTO = _mapper.Map<ChildrenDTO>(children);
                int parentId = children.ParentId;

                var parent = await _employeeRepository.GetById(parentId);
                if(parent != null)
                {
                    childrenDTO.Parent = _mapper.Map<EmployeeDTO>(parent);
                }

                return childrenDTO;
            }
            
            return null;
        }

        public Task<ChildrenDTO> Add(ChildrenInsertDTO InsertDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ChildrenDTO> Update(int id, ChildrenUpdateDTO employeeUpdateDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ChildrenDTO> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Validate(ChildrenInsertDTO dto)
        {
            throw new NotImplementedException();
        }

        public bool Validate(ChildrenUpdateDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
