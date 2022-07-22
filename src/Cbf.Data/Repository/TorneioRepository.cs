using Cbf.Business.Interfaces.Repositories;
using Cbf.Business.Models;
using Cbf.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cbf.Data.Repository
{
    public class TorneioRepository : Repository<Torneio>, ITorneioRepository
    {
        public TorneioRepository(AppDbContext context) : base(context) { }

        public async Task<Torneio> ObterTorneioTimes(Guid id)
        {
            return await Db.Torneios.AsNoTracking()
                                    .Include(t => t.Times)
                                    .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Torneio>> ObterTorneiosTimes()
        {
            return await Db.Torneios.AsNoTracking()
                                    .Include(t => t.Times)
                                    .OrderBy(t => t.Nome)
                                    .ToListAsync();
        }
    }
}
