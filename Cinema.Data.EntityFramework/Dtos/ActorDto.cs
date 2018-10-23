using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Data.EntityFramework.Dtos
{
    [Table("Actors")]
    internal sealed class ActorDto
    {
        public int Id { get; set; }

        [Required]
        public HumanDto HumanDto { get; set; }
    }
}