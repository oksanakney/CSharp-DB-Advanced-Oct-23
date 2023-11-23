using P02_FootballBetting.Data.Common;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models
{
    public class Team
    {
        public Team()
        {
            this.HomeGames = new HashSet<Game>();
            this.AwayGames = new HashSet<Game>();
            this.Players = new HashSet<Player>();
        }        
        [Key]
        public int TeamId { get; set; }
        [Required] //Not null in SQL; VARCHAR(MAXLENGTH)
        //[MaxLength(50)] -> magic number
        [MaxLength(ValidationConstants.TeamNameMaxLength)]
        public string Name { get; set; } = null!;
        [MaxLength(ValidationConstants.TeamLogoUrlMaxLength)]
        public string? LogoUrl { get; set; }
        [Required]
        [MaxLength(ValidationConstants.TeamInitialMaxLength)]
        public string Initials { get; set; } = null!;
        public decimal Budget { get; set; }       
        public int PrimaryKitColorId { get; set; }
        [ForeignKey(nameof(PrimaryKitColorId))]
        public virtual Color PrimaryKitColor { get; set; } = null!;
        public int SecondaryKitColorId { get; set;  }
        [ForeignKey(nameof(SecondaryKitColorId))]
        public virtual Color SecondaryKitColor { get; set; } = null!;
        public int TownId { get; set; }
        [ForeignKey(nameof(TownId))]
        public virtual Town Town { get; set; } = null!;
        [InverseProperty(nameof(Game.HomeTeam))]
        public virtual ICollection<Game> HomeGames { get; set; }
        [InverseProperty(nameof(Game.AwayTeam))]
        public virtual ICollection<Game> AwayGames { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}