using Cbf.Business.Models;

namespace Cbf.Business.Interfaces.Services
{
    public interface IJogadorService : IDisposable
    {
        Task<IEnumerable<Jogador>> ObterTodos();
        Task<Jogador> ObterPorId(Guid id);
        Task Adicionar(Jogador jogador);
        Task Atualizar(Jogador jogador);
        Task Remover(Guid id);
    }
}
