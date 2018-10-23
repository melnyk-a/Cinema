using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Data.EntityFramework.Dtos
{
    [Table("Directors")]
    internal sealed class DirectorDto
    {
        public int Id { get; set; }

        [Required]
        public HumanDto HumanDto { get; set; }
    }
}