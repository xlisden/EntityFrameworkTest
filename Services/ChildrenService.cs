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

                childrenDTO = await assingParent(childrenDTO, children.ParentId);

                //int parentId = children.ParentId;
                //var parent = await _employeeRepository.GetById(parentId);
                //if(parent != null)
                //{
                //    childrenDTO.Parent = _mapper.Map<EmployeeDTO>(parent);
                //}

                return childrenDTO;
            }
            
            return null;
        }

        public async Task<ChildrenDTO> Add(ChildrenInsertDTO InsertDTO)
        {
            var children = _mapper.Map<Children>(InsertDTO);
            
            await _childrenRepository.Add(children);
            await _childrenRepository.Save();

            var childrenDTO = _mapper.Map<ChildrenDTO>(children);
            childrenDTO = await assingParent(childrenDTO, children.ParentId);

            //int parentId = children.ParentId;
            //var parent = await _employeeRepository.GetById(parentId);
            //if (parent != null)
            //{
            //    childrenDTO.Parent = _mapper.Map<EmployeeDTO>(parent);
            //}

            return childrenDTO;
        }

        public async Task<ChildrenDTO> Update(int id, ChildrenUpdateDTO dto)
        {
            var children = await _childrenRepository.GetById(id);

            if(children != null)
            {
                children = _mapper.Map<ChildrenUpdateDTO, Children>(dto, children);

                _childrenRepository.Update(children);
                await _childrenRepository.Save();

                var childrenDTO = _mapper.Map<ChildrenDTO>(children);
                childrenDTO = await assingParent(childrenDTO, children.ParentId);

                //int parentId = children.ParentId;
                //var parent = await _employeeRepository.GetById(parentId);
                //if (parent != null)
                //{
                //    childrenDTO.Parent = _mapper.Map<EmployeeDTO>(parent);
                //}

                return childrenDTO;
            }
            return null;
        }

        public async Task<ChildrenDTO> Delete(int id)
        {
            var children = await _childrenRepository.GetById(id);

            if (children == null)
            {
                return null;
            }

            var childrenDTO = _mapper.Map<ChildrenDTO>(children);
            childrenDTO = await assingParent(childrenDTO, children.ParentId);

            _childrenRepository.Delete(children);
            await _childrenRepository.Save();

            return childrenDTO;
        }

        public bool Validate(ChildrenInsertDTO dto)
        {
            if (_childrenRepository.Search(e => e.Name == dto.Name).Count() > 0)
            {
                Errors.Add("No se puede aniadir un hijo con el nombre de otro.");
                return false;
            }
            return true;
        }

        public bool Validate(ChildrenUpdateDTO dto)
        {
            if (_childrenRepository.Search(e => e.Name == dto.Name && e.Id != dto.Id).Count() > 0)
            {
                Errors.Add("No se puede aniadir un hijo con el nombre de otro.");
                return false;
            }
            return true;
        }

        public async Task<ChildrenDTO> assingParent(ChildrenDTO childrenDTO, int id)
        {
            var parent = await _employeeRepository.GetById(id);
            if (parent != null)
            {
                childrenDTO.Parent = _mapper.Map<EmployeeDTO>(parent);
            }

            return childrenDTO;
        }
    }
}
