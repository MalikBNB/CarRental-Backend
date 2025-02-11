using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Entities.Interfaces;

namespace CarRental.Entities.DbSets
{
    public class Maintenance : IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public DateTime EntranceDate { get; set; } = DateTime.Now;
        public DateTime ExitDate { get; set; }
        public decimal Cost { get; set; }
        public byte Status { get; set; }
        public string CreatorId { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public string ModifierId { get; set; } = string.Empty;
        public DateTime Modified { get; set; }

        //public virtual User Creator { get; set; } = null!;
        //public virtual User Modifier { get; set; } = null!;

    }
}
