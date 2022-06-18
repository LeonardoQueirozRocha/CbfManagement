namespace Cbf.Business.Models
{
    public class Time : Entity
    {
        public string Nome { get; set; }
        public string Localidade { get; set; }

        public DateTime DataCadastro { get; set; }

        // EF Relation
        public IEnumerable<Jogador> Jogadores { get; set; }
        public IEnumerable<Transferencia> Transferencias { get; set; }
    }
}
