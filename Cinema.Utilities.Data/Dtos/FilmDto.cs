using Cinema.Domain.Models;
using System;

namespace Cinema.Utilities.Data.Dtos
{
    public sealed class FilmDto
    {
        public int Id { get; set; }

        public Language Language { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Title { get; set; }
    }
}