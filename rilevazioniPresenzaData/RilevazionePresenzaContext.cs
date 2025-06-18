using Microsoft.EntityFrameworkCore;
using rilevazioniPresenzaData.Models;

namespace rilevazioniPresenzaData
{
    public class RilevazionePresenzaContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=rilevazioniPresenze;Persist Security Info=True;User ID=Andre;Password=Andrea2007;Encrypt=True;Trust Server Certificate=True");
        }
    }
}
