namespace Cbf.Business.Models
{
    public class Time : Entity
    {
        public string Nome { get; set; }
        public string Localidade { get; set; }
        public string Tecnico { get; set; }
        public DateTime Fundacao { get; set; }
        public string Estadio { get; set; }

        // EF Relation
        public IEnumerable<Jogador> Jogadores { get; set; }
        public IEnumerable<Transferencia> Transferencias { get; set; }
    }
}
