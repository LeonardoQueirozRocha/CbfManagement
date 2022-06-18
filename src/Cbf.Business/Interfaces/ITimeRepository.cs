using Cbf.Business.Models;

namespace Cbf.Business.Interfaces
{
    public interface ITimeRepository : IRepository<Time>
    {
        Task<Time> ObterTimeJogadores(Guid id);
    }
}
