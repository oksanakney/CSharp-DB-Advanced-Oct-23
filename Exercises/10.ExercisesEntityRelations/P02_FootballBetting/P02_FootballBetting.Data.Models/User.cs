using P02_FootballBetting.Data.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P02_FootballBetting.Data.Models
{
    public class User
    {
        public User()
        {
            this.Bets = new HashSet<Bet>();
        }
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(ValidationConstants.UserUsernameMaxLength)]
        public string Username { get; set; } = null!;
        [Required]
        [MaxLength(ValidationConstants.UserPasswordMaxLength)]
        //Password  hashed in DB 
        //SHA256 Hashing Algorithm + password salt
        public string Password { get; set; } = null!;
        [Required]
        [MaxLength(ValidationConstants.UserEmailMaxlength)]
        public string Email { get; set; } = null!;
        [Required]
        [MaxLength(ValidationConstants.UserNameMaxLength)]
        public string Name { get; set; } = null!;
        public decimal Balance { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
    }
}
