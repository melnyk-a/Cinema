using Cinema.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Data.EntityFramework.Dtos
{
    [Table("Films")]
    internal sealed class FilmDto
    {
        public int Id { get; set; }

        public bool? HasBlurayRelease { get; set; }

        [Required]
        public Language Language { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public string Title { get; set; }
    }
}