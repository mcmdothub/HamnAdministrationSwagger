using Microsoft.EntityFrameworkCore;

namespace HamnAdministration.Models
{
    public class HamnContext : DbContext
    {
        public HamnContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Parkering> Parkering { get; set; }
        public DbSet<ParkeringsPlats> ParkeringsPlatser { get; set; }
        public DbSet<Bestall> BestallStore { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parkering>()
                .HasKey(p => p.Namn);
            modelBuilder.Entity<Parkering>()
                .HasMany(p => p.Platser)
                .WithOne(p => p.Parkering)
                .HasForeignKey(p => p.ParkeringsNamn)
                .IsRequired();

            modelBuilder.Entity<ParkeringsPlats>()
                .HasKey(p => new { p.ParkeringsNamn, p.Siffra });

            modelBuilder.Entity<Bestall>()
                .HasKey(c => c.Id);
        }
    }
}
