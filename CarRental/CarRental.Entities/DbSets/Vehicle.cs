using System.Text.Json.Serialization;
using CarRental.Entities.Interfaces;

namespace CarRental.Entities.DbSets
{
    public class Vehicle : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public int Mileage { get; set; }
        public FuelType FuelType { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public Guid CarCategoryId { get; set; }
        [JsonIgnore]
        public virtual CarCategory CarCategory { get; set; } = null!;
        public decimal RentalPricePerDay { get; set; }
        public bool IsAvailableForRent { get; set; }
        public byte Status { get; set; } = 1;
        public string Creator { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public string Modifier { get; set; } = string.Empty;
        public DateTime Modified { get; set; }

        public List<Maintenance>? Maintenances { get; set; }
        public List<RentalBooking>? RentalBookings { get; set; }

        //public virtual User Creator { get; set; } = null!;
        //public virtual User Modifier { get; set; } = null!;
    }

    public enum FuelType
    {
        Gasoline = 1,
        Diesel,
        Electric,
        Hybrid
    }
}
