using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BussinessLogic.Data
{
    public class TrailerDbContext : DbContext
    {
        public TrailerDbContext(DbContextOptions<TrailerDbContext> options) : base(options) { }

        public DbSet<TrailersEntities> Trailers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
