using Cbf.Business.Models;

namespace Cbf.Business.Interfaces.Repositories
{
    public interface IPartidaRepository : IRepository<Partida>
    {
        Task<IEnumerable<Partida>> ObterPartidasPorTorneio(Guid torneioId);
        Task<IEnumerable<Partida>> ObterPartidasTorneios();
        Task<Partida> ObterPartidaTorneio(Guid id);
    }
}
