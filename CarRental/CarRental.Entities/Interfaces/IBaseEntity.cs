﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Entities.DbSets;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Entities.Interfaces
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }
        public byte Status { get; set; }
        public string CreatorId { get; set; }
        public string ModifierId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        //public User Creator { get; set; }
        //public User Modifier { get; set; }
    }
}
