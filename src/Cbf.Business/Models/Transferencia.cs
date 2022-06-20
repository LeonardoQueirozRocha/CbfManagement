namespace Cbf.Business.Models
{
    public class Transferencia : Entity
    {
        public Guid TimeOrigemId { get; set; }
        public Guid TimeDestinoId { get; set; }
        public Guid JogadorId { get; set; }
        public DateTime DataCadastro { get; set; }
        public decimal Valor { get; set; }

        // EF Relation
        public Jogador Jogador { get; set; }
        public Time Time { get; set; }
    }
}
