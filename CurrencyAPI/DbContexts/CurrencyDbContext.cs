using CurrencyAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CurrencyAPI.DbContexts
{
    public class CurrencyDbContext: DbContext
    {
        public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options) : base(options) { }

        public DbSet<Currency> R_CURRENCY { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("R_CURRENCY");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Title).IsRequired().HasMaxLength(60);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(3);
                entity.Property(e => e.Value).IsRequired().HasColumnType("numeric(18,2)");
                entity.Property(e => e.ADate).IsRequired().HasColumnType("date");
            });
        }
    }
}
