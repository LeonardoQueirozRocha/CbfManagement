using Cbf.Business.Interfaces.Repositories;
using Cbf.Business.Models;
using Cbf.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cbf.Data.Repository
{
    public class PartidaRepository : Repository<Partida>, IPartidaRepository
    {
        public PartidaRepository(AppDbContext db) : base(db) { }

        public async Task<Partida> ObterPartidaTorneio(Guid id)
        {
            return await Db.Partidas.AsNoTracking()
                                    .Include(p => p.Torneio)
                                    .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Partida>> ObterPartidasTorneios()
        {
            return await Db.Partidas.AsNoTracking()
                                    .Include(p => p.Torneio)
                                    .OrderBy(p => p.DataPartida)
                                    .ToListAsync();
        }

        public async Task<IEnumerable<Partida>> ObterPartidasPorTorneio(Guid torneioId)
        {
            return await Buscar(j => j.TorneioId == torneioId);
        }
    }
}
