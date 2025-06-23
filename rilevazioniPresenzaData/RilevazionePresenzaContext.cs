using Microsoft.EntityFrameworkCore;
using rilevazioniPresenzaData.Models;
using System.Reflection.Metadata;

namespace rilevazioniPresenzaData
{
    public class RilevazionePresenzaContext : DbContext
    {
        //public RilevazionePresenzaContext(DbContextOptions<RilevazionePresenzaContext> options)
        //: base(options)
        //{
        //}

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserShift> UserShifts { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=rilevazioniPresenze;Persist Security Info=True;User ID=Andre;Password=Andrea2007;Encrypt=True;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserShift>()
                .HasKey(c => new { c.IdMatricola, c.Giorno });
            modelBuilder.Entity<User>()
            .HasMany(e => e.UserShifts)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.IdMatricola)
            .IsRequired();
        }
    }
}
