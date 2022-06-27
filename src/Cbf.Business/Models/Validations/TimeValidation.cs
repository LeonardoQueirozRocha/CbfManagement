using FluentValidation;

namespace Cbf.Business.Models.Validations
{
    public class TimeValidation : AbstractValidator<Time>
    {
        public TimeValidation()
        {
            RuleFor(t => t.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(t => t.Localidade)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 255).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(t => t.Fundacao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .LessThan(t => DateTime.Now).WithMessage("O campo {PropertyName} deve estar no passado");
        }
    }
}
