using Cbf.Business.Interfaces.Repositories;
using Cbf.Business.Models;
using Cbf.Data.Context;

namespace Cbf.Data.Repository
{
    public class TransferenciaRepository : Repository<Transferencia>, ITransferenciaRepository
    {
        public TransferenciaRepository(AppDbContext db) : base(db) { }

        public async Task<IEnumerable<Transferencia>> ObterTransferenciasPorTimeDestino(Guid timeId)
        {
            return await Buscar(t => t.TimeDestinoId == timeId);
        }

        public async Task<IEnumerable<Transferencia>> ObterTransferenciasPorTimeOrigem(Guid timeId)
        {
            return await Buscar(t => t.TimeOrigemId == timeId);
        }

        public async Task<IEnumerable<Transferencia>> ObterTransferenciasPorJogador(Guid jogadorId)
        {
            return await Buscar(t => t.JogadorId == jogadorId);
        }
    }
}
