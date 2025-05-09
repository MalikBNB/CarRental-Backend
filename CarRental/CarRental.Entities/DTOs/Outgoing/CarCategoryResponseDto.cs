﻿using CarRental.Entities.DbSets;
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
    public byte[]? Picture { get; set; }
    public string Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string Modifier { get; set; } = string.Empty;
    public DateTime Modified { get; set; }

    public List<VehicleResponseDto>? Vehicles { get; set; } = new List<VehicleResponseDto>();
}
