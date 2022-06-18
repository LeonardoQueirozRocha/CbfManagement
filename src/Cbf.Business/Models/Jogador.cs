namespace Cbf.Business.Models
{
    public class Jogador : Entity
    {
        public Guid TimeId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Pais { get; set; }
        public DateTime DataCadastro { get; set; }

        //EF Relation
        public Time Time { get; set; }
        public IEnumerable<Transferencia> Transferencias { get; set; }
    }
}
