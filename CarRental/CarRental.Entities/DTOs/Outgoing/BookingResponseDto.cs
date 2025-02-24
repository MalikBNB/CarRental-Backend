using CarRental.Entities.DbSets;

namespace CarRental.Entities.DTOs.Outgoing;
public class BookingResponseDto
{
    public Guid Id { get; set; }
    public string CustomerId { get; set; } = string.Empty;
    public virtual ProfileDto Customer { get; set; } = null!;
    public Guid VehicleId { get; set; }
    public virtual VehicleResponseDto Vehicle { get; set; } = null!;
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
    public string Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string Modifier { get; set; } = string.Empty;
}
