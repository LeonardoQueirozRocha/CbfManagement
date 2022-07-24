using FluentValidation;

namespace Cbf.Business.Models.Validations
{
    public class PartidaValidation : AbstractValidator<Partida>
    {
        public PartidaValidation()
        {
            RuleFor(p => p.TimeCasaId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.TimeVisitanteId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.TorneioId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.DataPartida)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .GreaterThanOrEqualTo(p => DateTime.Now).WithMessage("O campo {PropertyName} deve ser atual ou no futuro");
        }
    }
}