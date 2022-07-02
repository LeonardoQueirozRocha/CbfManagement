using System.ComponentModel.DataAnnotations;

namespace Cbf.Api.ViewModels
{
    public class TransferenciaViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid TimeOrigemId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid TimeDestinoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid JogadorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }
    }
}
