namespace Cbf.Business.Models
{
    public class Transferencia : Entity
    {
        public Guid TimeOrigemId { get; set; }
        public Guid TimeDestinoId { get; set; }
        public Guid JogadorId { get; set; }
        public decimal Valor { get; set; }

        // EF Relation
        public Time Time { get; set; }
        public Jogador Jogador { get; set; }
    }
}
