using Cbf.Business.Interfaces;
using Cbf.Business.Models;
using Cbf.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cbf.Data.Repository
{
    public class JogadorRepository : Repository<Jogador>, IJogadorRepository
    {
        public JogadorRepository(AppDbContext db) : base(db) { }

        public async Task<Jogador> ObterJogadorTime(Guid id)
        {
            return await Db.Jogadores.AsNoTracking()
                                     .Include(j => j.Time)
                                     .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<IEnumerable<Jogador>> ObterJogadoresTimes()
        {
            return await Db.Jogadores.AsNoTracking()
                                     .Include(j => j.Time)
                                     .OrderBy(p => p.Nome)
                                     .ToListAsync();
        }

        public async Task<IEnumerable<Jogador>> ObterJogadoresPorTime(Guid timeId)
        {
            return await Buscar(j => j.TimeId == timeId);
        }

        public async Task<Jogador> ObterJogadorTransferencias(Guid id)
        {
            return await Db.Jogadores.AsNoTracking()
                                     .Include(j => j.Transferencias)
                                     .FirstOrDefaultAsync(j => j.Id == id);
        }
    }
}
