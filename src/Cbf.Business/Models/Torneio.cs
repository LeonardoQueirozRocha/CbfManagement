namespace Cbf.Business.Models
{
    public class Torneio : Entity
    {
        public string Nome { get; set; }

        // EF Relation
        public ICollection<Time> Times { get; set; } = new List<Time>();
        public IEnumerable<Partida> Partidas { get; set; }
    }
}
