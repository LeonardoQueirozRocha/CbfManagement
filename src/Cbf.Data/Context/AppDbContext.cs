using Cbf.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Cbf.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Transferencia> Transferencias { get; set; }
        public DbSet<Torneio> Torneios { get; set; }
        public DbSet<Partida> Partidas { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes()
                                                .SelectMany(e => e.GetProperties()
                                                .Where(p => p.ClrType == typeof(string)));

            foreach (var property in entityTypes) property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            var foreignKeys = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());

            foreach (var relationship in foreignKeys) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(entry =>
                entry.Entity.GetType().GetProperty("DataCadastro") != null);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added) entry.Property("DataCadastro").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified) entry.Property("DataCadastro").IsModified = false;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
