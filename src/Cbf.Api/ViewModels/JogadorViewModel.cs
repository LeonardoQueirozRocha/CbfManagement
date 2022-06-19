using System.ComponentModel.DataAnnotations;

namespace Cbf.Api.ViewModels
{
    public class JogadorViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid TimeId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Pais { get; set; }

        public DateTime DataCadastro { get; set; }

        public TimeViewModel Time { get; set; }
        public IEnumerable<TransferenciaViewModel> Transferencias { get; set; }
    }
}
