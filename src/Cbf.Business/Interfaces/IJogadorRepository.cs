using Cbf.Business.Models;

namespace Cbf.Business.Interfaces
{
    public interface IJogadorRepository
    {
        Task<IEnumerable<Jogador>> ObterJogadoresPorTime(Guid timeId);
        Task<IEnumerable<Jogador>> ObterJogadoresTimes();
        Task<Jogador> ObterJogadorTime(Guid id);
    }
}
