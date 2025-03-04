using EntityFramworkProject.DTOs;
using FluentValidation;

namespace EntityFramworkProject.Validators
{
    public class ChildrenInsertValidator : AbstractValidator<ChildrenInsertDTO>
    {
        public ChildrenInsertValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre del hijo(a) no puede estar vacio");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("El nombre debe tener entre 2 a 20 caracteres");
            RuleFor(x => x.BirthDay).NotEmpty().WithMessage("La fecha de nacimiento no puede estar vacia");
            RuleFor(x => x.ParentId).NotEmpty().WithMessage("El padre del hijo(a) no puede estar vacio");
        }
    }
}
