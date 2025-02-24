using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Entities.Interfaces;

namespace CarRental.Entities.DbSets
{
    public class VehicleReturn : IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid RentalTransactionId { get; set; }
        public virtual RentalTransaction RentalTransaction { get; set; } = null!;
        public DateTime ActualReturnDate { get; set; }
        public byte ActualRentalDays { get; set; }
        public byte Mileage { get; set; }
        public byte ConsumedMileage { get; set; }
        public string FinalCheckNotes { get; set; } = string.Empty;
        public decimal AdditionalCharges { get; set; }
        public decimal ActualTotalDueAmount { get; set; }
        public string Creator { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public string Modifier { get; set; } = string.Empty;
        public DateTime Modified { get; set; }

        //public virtual User Creator { get; set; } = null!;
        //public virtual User Modifier { get; set; } = null!;
    }
}
