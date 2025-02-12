using CarRental.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entities.DTOs.Outgoing;
public class CarCategoryResponseDto
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public byte Status { get; set; }
    public string CreatorId { get; set; } = string.Empty;
    public string Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string ModifierId { get; set; } = string.Empty;
    public string Modifier { get; set; } = string.Empty;
    public DateTime Modified { get; set; }

    public List<VehicleResponseDto>? Vehicles { get; set; }
}
