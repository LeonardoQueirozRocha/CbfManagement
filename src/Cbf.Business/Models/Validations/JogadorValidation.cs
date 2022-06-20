using FluentValidation;

namespace Cbf.Business.Models.Validations
{
    public class JogadorValidation : AbstractValidator<Jogador>
    {
        public JogadorValidation()
        {
            RuleFor(t => t.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(t => t.DataNascimento)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .LessThan(t => DateTime.Now).WithMessage("O campo {PropertyName} deve estar no passado");
                
            RuleFor(t => t.Pais)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
