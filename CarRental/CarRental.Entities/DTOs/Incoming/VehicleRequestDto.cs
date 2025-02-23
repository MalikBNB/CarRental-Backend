using CarRental.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entities.DTOs.Incoming;
public class VehicleRequestDto
{
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public byte[]? Picture { get; set; }
    public int Year { get; set; }
    public int Mileage { get; set; }
    public FuelType FuelType { get; set; }
    public string PlateNumber { get; set; } = string.Empty;
    public Guid CarCategoryId { get; set; }
    public decimal RentalPricePerDay { get; set; }
    public bool IsAvailableForRent { get; set; }
}
