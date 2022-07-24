using Cbf.Business.Models;

namespace Cbf.Business.Interfaces.Services
{
    public interface IPartidaService : IDisposable
    {
        Task<IEnumerable<Partida>> ObterPartidasTorneios();
        Task<Partida> ObterPartidaTorneio(Guid id);
        Task Adicionar(Partida partida);
        Task Atualizar(Partida partida);
        Task Remover(Guid id);
    }
}
