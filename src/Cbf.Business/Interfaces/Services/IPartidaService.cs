using Cbf.Business.Models;

namespace Cbf.Business.Interfaces.Services
{
    public interface IPartidaService : IDisposable
    {
        Task<IEnumerable<Partida>> ObterTodos();
        Task<Partida> ObterPorId(Guid id);
        Task<Partida> ObterPartidaTorneio(Guid id);
        Task Adicionar(Partida partida);
        Task Atualizar(Partida partida);
        Task Remover(Guid id);
    }
}
