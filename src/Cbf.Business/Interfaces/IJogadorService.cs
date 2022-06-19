using Cbf.Business.Models;

namespace Cbf.Business.Interfaces
{
    public interface IJogadorService : IDisposable
    {
        Task Adicionar(Jogador jogador);
        Task Atualizar(Jogador jogador);
        Task Remover(Guid id);
    }
}
