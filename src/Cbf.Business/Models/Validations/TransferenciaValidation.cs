using FluentValidation;

namespace Cbf.Business.Models.Validations
{
    public class TransferenciaValidation : AbstractValidator<Transferencia>
    {
        public TransferenciaValidation()
        {
            RuleFor(t => t.TimeOrigemId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(t => t.TimeDestinoId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(t => t.JogadorId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(t => t.Valor)
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");

        }
    }
}
