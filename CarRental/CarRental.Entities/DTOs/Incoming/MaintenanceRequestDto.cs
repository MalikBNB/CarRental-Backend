using CarRental.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entities.DTOs.Incoming;
public class MaintenanceRequestDto
{
    public Guid VehicleId { get; set; }
    //public virtual Vehicle Vehicle { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public DateTime EntranceDate { get; set; } = DateTime.Now;
    public DateTime ExitDate { get; set; }
    public decimal Cost { get; set; }
    public byte Status { get; set; } = 1;
}
