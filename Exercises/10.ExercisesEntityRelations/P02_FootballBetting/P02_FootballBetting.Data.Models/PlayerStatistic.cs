using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models
{
    public class PlayerStatistic
    {
        //Mapping class
        //Here we have composite PK -> We will use FluentAPI for config it
        public int GameId { get; set; }
        [ForeignKey(nameof(GameId))]
        public virtual Game Game { get; set; } = null!;
        public int PlayerId { get; set;}
        [ForeignKey(nameof(PlayerId))]
        public virtual Player Player { get; set; } = null!;
        //Judge may not be happy with byte
        public byte ScoredGoals { get; set; } // byte max 255
        public byte Assists { get; set;}
        public byte MinutesPlayed { get; set; }
    }
}
