using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trucks.Common;
using Trucks.Data.Models.Enums;

namespace Trucks.Data.Models;

public class Truck
{
    public Truck()
    {
        this.ClientsTrucks = new HashSet<ClientTruck>();
    }
    [Key]
    public int Id { get; set; }
    [MaxLength(ValidationConstants.TruckRegistrationNumberLength)]
    public string? RegistrationNumber { get; set; } // not required
    [Required]
    [MaxLength(ValidationConstants.TruckVinNumberMaxLength)]
    public string VinNumber { get; set; } = null!;
    /*in range [950…1420] na nivo baza niama 
     * nuzhda da pravia check constraint, na nivo importvane da*/
    public int TankCapacity { get; set; }
    public int CargoCapacity { get; set;}
    //Ponezhe tva e enum, to e avtomatichno si required by default
    //zawoto enum e chislen tip dani
    public CategoryType CategoryType { get; set; }
    public MakeType MakeType { get; set; }
    // int is required by default
    public int DespatcherId { get; set; }
    [ForeignKey(nameof(DespatcherId))]
    public virtual Despatcher Despatcher { get; set; } = null!;
    public virtual ICollection<ClientTruck> ClientsTrucks { get; set; }


}
