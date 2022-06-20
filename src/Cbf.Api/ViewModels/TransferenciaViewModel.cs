using System.ComponentModel.DataAnnotations;

namespace Cbf.Api.ViewModels
{
    public class TransferenciaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid TimeOrigemId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid TimeDestinoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid JogadorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }

        public JogadorViewModel Jogador { get; set; }
        public TimeViewModel Time { get; set; }
    }
}
