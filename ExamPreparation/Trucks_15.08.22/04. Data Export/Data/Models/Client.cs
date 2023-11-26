using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using Trucks.Common;

namespace Trucks.Data.Models
{
    public class Client
    {
        public Client()
        {
            this.ClientsTrucks = new HashSet<ClientTruck>();
        }

        [Key]
        public  int Id { get; set; }
        [Required]
        [MaxLength(ValidationConstants.ClientNameMaxLength)]
        public string Name { get; set; } = null!; // (3 - 40) 3 ne e val nivo baza
        [Required]
        [MaxLength(ValidationConstants.ClientNationalityMaxLength)]
        public string Nationality { get; set; } = null!;
        [Required]
        public string Type { get; set; } = null!;
        public virtual ICollection<ClientTruck> ClientsTrucks { get; set; }
    }
}
