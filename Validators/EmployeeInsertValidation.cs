using EntityFramworkProject.DTOs;
using FluentValidation;

namespace EntityFramworkProject.Validators
{
    public class EmployeeInsertValidation : AbstractValidator<EmployeeInsertDTO>
    {
        public EmployeeInsertValidation() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es requerido");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("La longitud del nombre debe ser de 2 a 20 caracteres");
            RuleFor(x => x.Department).NotEmpty();
        }
    }
}
