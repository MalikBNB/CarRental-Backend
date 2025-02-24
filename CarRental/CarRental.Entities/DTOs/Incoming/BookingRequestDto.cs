using CarRental.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entities.DTOs.Incoming;
public class BookingRequestDto
{
    public string CustomerId { get; set; } = string.Empty;
    public Guid VehicleId { get; set; }
    public Guid RentalTransactionId { get; set; }
    public virtual Transaction? RentalTransaction { get; set; } = null!;
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public string PickupLocation { get; set; } = string.Empty;
    public string DropoffLocation { get; set; } = string.Empty;
    public byte RentalDays { get; set; }
    public decimal RentalPricePerDay { get; set; }
    public decimal TotalDueAmount { get; set; }
    public string InitialCheckNotes { get; set; } = string.Empty;
}
