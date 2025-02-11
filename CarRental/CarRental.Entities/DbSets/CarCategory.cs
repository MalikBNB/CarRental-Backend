﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Entities.Interfaces;

namespace CarRental.Entities.DbSets
{
    public class CarCategory : IBaseEntity
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public byte Status { get; set; }
        public string CreatorId { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public string ModifierId { get; set; } = string.Empty;
        public DateTime Modified { get; set; }

        public List<Vehicle>? Vehicles { get; set; }

        //public User Creator { get; set; } = null!;
        //public User Modifier { get; set; } = null!;
    }
}
