namespace Cbf.Business.Models
{
    public class Partida : Entity
    {
        public Guid TimeCasaId { get; set; }
        public Guid TimeVisitanteId { get; set; }
        public Guid TorneioId { get; set; }
        public DateTime DataPartida { get; set; }
        public string Resultado { get; set; }

        // EF Relation
        public Torneio Torneio { get; set; }
    }
}
