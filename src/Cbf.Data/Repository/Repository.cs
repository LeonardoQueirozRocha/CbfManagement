using Cbf.Business.Interfaces.Repositories;
using Cbf.Business.Models;
using Cbf.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cbf.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(AppDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
            
        public virtual async Task<TEntity> ObterPorId(Guid id) => await DbSet.FindAsync(id);

        public virtual async Task<List<TEntity>> ObterTodos() => await DbSet.ToListAsync();

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public async Task Remover(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges() => await Db.SaveChangesAsync();

        public void Dispose() => Db?.Dispose();
    }
}
