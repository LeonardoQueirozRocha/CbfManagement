﻿using Cbf.Business.Models;

namespace Cbf.Business.Interfaces.Services
{
    public interface ITorneioService
    {
        Task<IEnumerable<Torneio>> ObterTodos();
        Task<Torneio> ObterPorId(Guid id);
        Task Adicionar(Torneio torneio);
        Task AdicionarTime(Guid torneioId, Guid timeId);
        Task Atualizar(Torneio torneio);
        Task Remover(Guid id);
    }
}
