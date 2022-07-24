using System.ComponentModel.DataAnnotations;

namespace Cbf.Api.ViewModels
{
    public class PartidaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid TimeCasaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid TimeVisitanteId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid TorneioId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataPartida { get; set; }

        public string Resultado { get; set; }

        // EF Relation
        public TorneioViewModel Torneio { get; set; }
    }
}
