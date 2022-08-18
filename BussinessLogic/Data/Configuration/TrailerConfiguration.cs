using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BussinessLogic.Data.Configuration
{
    public class TrailerConfiguration : IEntityTypeConfiguration<TrailersEntities>
    {
        public void Configure(EntityTypeBuilder<TrailersEntities> builder)
        {
            builder.Property(t => t.TrailerName).IsRequired().HasMaxLength(250);
            builder.Property(t => t.Description).IsRequired().HasMaxLength(500);
            builder.Property(t => t.Image).IsRequired().HasMaxLength(500);
            builder.Property(t => t.Link).IsRequired().HasMaxLength(500);
            builder.Property(t => t.Creator).IsRequired().HasMaxLength(250);
        }
    }
}
