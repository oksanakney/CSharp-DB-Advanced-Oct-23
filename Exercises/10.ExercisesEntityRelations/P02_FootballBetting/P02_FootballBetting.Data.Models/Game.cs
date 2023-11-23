using P02_FootballBetting.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models
{
    public class Game
    {
        public Game()
        {
            this.PlayersStatistics = new HashSet<PlayerStatistic>();
            this.Bets = new HashSet<Bet>();
        }
        // In real project it is good the PK to be string -> GUID
        // GUID -> Global Unique ID
        [Key]
        public int GameId { get; set; }        
        public int HomeTeamId { get; set; }
        [ForeignKey(nameof(HomeTeamId))]
        public virtual Team HomeTeam { get; set; } = null!;
        public int AwayTeamId { get; set; }
        [ForeignKey(nameof(AwayTeamId))]
        public virtual Team AwayTeam { get; set; } = null!;
        public byte HomeTeamGoals { get; set; }
        public byte AwayTeamGoals { get; set; }
        // Is required by default, DateTime? is nullable
        public DateTime DateTime { get; set; }
        public double HomeTeamBetRate { get; set; }        
        public double AwayTeamBetRate { get; set; }
        public double DrawBetRate { get; set; }
        // 5:1    
        [MaxLength(ValidationConstants.GameResultMaxLength)]
        public string? Result { get; set; }
        public virtual ICollection<PlayerStatistic> PlayersStatistics { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
    }
}
