using CarRental.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarRental.Entities.DTOs.Outgoing;
public class MaintenanceResponseDto
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; }

    [JsonIgnore]
    public virtual Vehicle Vehicle { get; set; } = null!;

    public string Description { get; set; } = string.Empty;
    public DateTime EntranceDate { get; set; } = DateTime.Now;
    public DateTime ExitDate { get; set; }
    public decimal Cost { get; set; }
    public byte Status { get; set; }
    public string Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string Modifier { get; set; } = string.Empty;
    public DateTime Modified { get; set; }
}
