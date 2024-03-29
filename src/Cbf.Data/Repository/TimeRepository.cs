﻿using Cbf.Business.Interfaces.Repositories;
using Cbf.Business.Models;
using Cbf.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Cbf.Data.Repository
{
    public class TimeRepository : Repository<Time>, ITimeRepository
    {
        public TimeRepository(AppDbContext context) : base(context) { }

        public async Task<Time> ObterTimeJogadores(Guid id)
        {
            return await Db.Times.AsNoTracking()
                                 .Include(t => t.Jogadores)
                                 .FirstOrDefaultAsync(t => t.Id == id);

        }

        public async Task<Time> ObterTimeTransferencias(Guid id)
        {
            return await Db.Times.AsNoTracking()
                           .Include(t => t.Transferencias)
                           .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Time>> ObterTimesJogadores()
        {
            return await Db.Times.AsNoTracking()
                                 .Include(t => t.Jogadores)
                                 .OrderBy(t => t.Nome)
                                 .ToListAsync();
        }
    }
}
