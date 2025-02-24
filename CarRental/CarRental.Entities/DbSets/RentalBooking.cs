using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Entities.Interfaces;

namespace CarRental.Entities.DbSets
{
    public class RentalBooking : IBaseEntity
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public virtual User Customer { get; set; } = null!;
        public Guid VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; } = null!;
        public Guid RentalTransactionId { get; set; }
        public virtual RentalTransaction? RentalTransaction { get; set; } = null!;
        public DateTime RentalStartDate { get; set; }
        public DateTime RentalEndDate { get; set; }
        public string PickupLocation { get; set; } = string.Empty;
        public string DropoffLocation { get; set; } = string.Empty;
        public byte RentalDays { get; set; }
        public decimal RentalPricePerDay { get; set; }
        public decimal TotalDueAmount { get; set; }
        public string InitialCheckNotes { get; set; } = string.Empty;
        public string Creator { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public string Modifier { get; set; } = string.Empty;
        public DateTime Modified { get; set; }

        //public virtual User Creator { get; set; } = null!;
        //public virtual User Modifier { get; set; } = null!;
    }
}
