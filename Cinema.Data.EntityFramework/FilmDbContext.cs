using Cinema.Utilities.Data.Dtos;
using System.Data.Entity;

namespace Cinema.Data.EntityFramework
{
    internal sealed class FilmDbContext : DbContext
    {
        DbSet<FilmDto> Films { get; set; }
    }
}