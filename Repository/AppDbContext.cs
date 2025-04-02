using Microsoft.EntityFrameworkCore;
using SmartScaleApi.Domain.Entities;

namespace SmartScaleApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<CustomUnit> CustomUnits { get; set; }

        public DbSet<Sample> Samples { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomUnit>(entity =>
            {
                entity.ToTable("CustomUnits");
                entity.HasKey("Id");
                entity.Property("Unit").HasMaxLength(50);
                entity.Property("StandardUnit").HasMaxLength(50);
                entity.Property("ConversionFormulae").HasMaxLength(100); ;

            });
            modelBuilder.Entity<Sample>(entity =>
            {
                entity.ToTable("Samples");
                entity.HasKey("Id");
                entity.Property("Name").HasMaxLength(50);
                entity.Property("Units").HasMaxLength(200);
            });
        }
    }
}