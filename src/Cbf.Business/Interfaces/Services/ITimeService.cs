using Cbf.Business.Models;

namespace Cbf.Business.Interfaces.Services
{
    public interface ITimeService : IDisposable
    {
        Task<IEnumerable<Time>> ObterTodos();
        Task<Time> ObterPorId(Guid id);
        Task Adicionar(Time time);
        Task Atualizar(Time time);
        Task Remover(Guid id);
        Task FazerTransferencia(Transferencia transferencia);
    }
}
