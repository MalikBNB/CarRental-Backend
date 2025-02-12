using CarRental.DataService.Data;
using CarRental.DataService.IRepositories;
using CarRental.Entities.DbSets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataService.Repositories;
public class VehiclesRepository : GenericRepository<Vehicle>, IVehiclesRepository
{
    public VehiclesRepository(AppDbContext context) : base(context)
    {
        
    }

    
}
