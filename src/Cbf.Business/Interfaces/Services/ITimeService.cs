using Cbf.Business.Models;

namespace Cbf.Business.Interfaces.Services
{
    public interface ITimeService : IDisposable
    {
        Task Adicionar(Time time);
        Task Atualizar(Time time);
        Task Remover(Guid id);
        Task FazerTransferencia(Transferencia transferencia);
    }
}
