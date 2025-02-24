using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Entities.Interfaces;

namespace CarRental.Entities.DbSets
{
    public class Transaction : IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid RentalBookingId { get; set; }
        public virtual RentalBooking RentalBooking { get; set; } = null!;
        public Guid VehicleReturnId { get; set; }
        public virtual VehicleReturn? VehicleReturn { get; set; }
        public string PaymentDetails { get; set; } = string.Empty;
        public decimal PaidAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal RestAmount {  get; set; }
        public decimal RefundedAmount { get; set; }
        public DateTime Date { get; set; }
        public string Creator { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public string Modifier { get; set; } = string.Empty;
        public DateTime Modified { get; set; }

        //public virtual User Creator { get; set; } = null!;
        //public virtual User Modifier { get; set; } = null!;
    }
}
