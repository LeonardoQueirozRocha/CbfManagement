﻿using Cbf.Business.Models;

namespace Cbf.Business.Interfaces.Repositories
{
    public interface IJogadorRepository : IRepository<Jogador>
    {
        Task<IEnumerable<Jogador>> ObterJogadoresPorTime(Guid timeId);
        Task<IEnumerable<Jogador>> ObterJogadoresTimes();
        Task<Jogador> ObterJogadorTime(Guid id);
        Task<Jogador> ObterJogadorTransferencias(Guid id);
    }
}
