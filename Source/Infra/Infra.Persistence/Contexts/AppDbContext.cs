using System.Linq;
using Domain.Entities;
using Infra.Persistence.ConfigMaps;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Customer> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var cascadeFKs = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()).Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.ApplyConfiguration(new CustomerConfigMap());
            modelBuilder.ApplyConfiguration(new TransactionConfigMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}