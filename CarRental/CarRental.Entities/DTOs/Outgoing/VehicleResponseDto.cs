using CarRental.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entities.DTOs.Outgoing;
public class VehicleResponseDto
{
    public Guid Id { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public int Mileage { get; set; }
    public FuelType FuelType { get; set; }
    public string PlateNumber { get; set; } = string.Empty;
    //public CarCategoryResponseDto CarCategory { get; set; } = null!;
    public string CarCategory { get; set; } = string.Empty;
    public decimal RentalPricePerDay { get; set; }
    public bool IsAvailableForRent { get; set; }
    public byte Status { get; set; }
    public string Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string Modifier { get; set; } = string.Empty;
    public DateTime Modified { get; set; }

    //public List<MaintenanceResponseDto>? Maintenances { get; set; }
    //public List<RentalBookingResponseDto>? RentalBookings { get; set; }
}
