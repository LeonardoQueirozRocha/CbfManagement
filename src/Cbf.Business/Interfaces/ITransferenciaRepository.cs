using Cbf.Business.Models;

namespace Cbf.Business.Interfaces
{
    public interface ITransferenciaRepository : IRepository<Transferencia>
    {
        Task<IEnumerable<Transferencia>> ObterTransferenciasPorJogador(Guid jogadorId);
        Task<IEnumerable<Transferencia>> ObterTransferenciasPorTimeOrigem(Guid timeId);
        Task<IEnumerable<Transferencia>> ObterTransferenciasPorTimeDestino(Guid timeId);
    }
}
