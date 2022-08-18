using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BussinessLogic.Data
{
    public class SecurityDbContext : IdentityDbContext<UserEntities>
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
