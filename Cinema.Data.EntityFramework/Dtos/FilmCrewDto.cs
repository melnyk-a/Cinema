using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Data.EntityFramework.Dtos
{
    [Table("FilmCrews")]
    internal sealed class FilmCrewDto
    {
        [Required]
        public ICollection<ActorDto> ActorDtos { get; set; }

        [Required]
        public DirectorDto DirectorDto { get; set; }

        public int Id { get; set; }

        [Required]
        public FilmDto FilmDto { get; set; }
    }
}