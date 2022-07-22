using Cbf.Business.Models;

namespace Cbf.Business.Interfaces.Repositories
{
    public interface ITorneioRepository : IRepository<Torneio>
    {
        Task<Torneio> ObterTorneioTimes(Guid id);
        Task<IEnumerable<Torneio>> ObterTorneiosTimes();
    }
}
