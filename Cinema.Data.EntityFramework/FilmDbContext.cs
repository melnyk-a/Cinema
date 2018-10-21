using Cinema.Data.EntityFramework.Dtos;
using System.Data.Entity;

namespace Cinema.Data.EntityFramework
{
    internal sealed class FilmDbContext : DbContext
    {
        public FilmDbContext() :
            base("EntityFrameworkConnection")
        {
        }

        public DbSet<FilmCrewDto> FilmCrews { get; set; }

        public DbSet<FilmDto> Films { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}