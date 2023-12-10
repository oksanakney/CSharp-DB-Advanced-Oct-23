using Boardgames.Data.Models.Enums;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boardgames.Data.Models
{
    public class Boardgame
    {
        public Boardgame()
        {
            this.BoardgamesSellers = new HashSet<BoardgameSeller>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = null!;
        [MaxLength(10)]
        public double Rating { get; set; }
        [MaxLength(2023)]
        public int YearPublished { get; set; }
        public CategoryType CategoryType { get; set; }
        [Required]
        public string Mechanics { get; set; } = null!;
        public int CreatorId { get; set; }
        [ForeignKey(nameof(CreatorId))]
        public virtual Creator Creator { get; set; } = null!;
        public virtual ICollection<BoardgameSeller> BoardgamesSellers { get; set; }
    }
}