﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Trucks.Data.Models
{
    public class ClientTruck
    {
        public int ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public virtual Client Client { get; set; } = null!;
        public int TruckId { get; set; }
        [ForeignKey(nameof(TruckId))]
        public virtual Truck Truck { get; set; } = null!;
    }
}
