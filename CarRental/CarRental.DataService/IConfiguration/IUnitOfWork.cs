using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.DataService.IRepositories;

namespace CarRental.DataService.IConfiguration
{
    public interface IUnitOfWork
    {
        IVehiclesRepository Vehicles { get; }
        ICarCategoriesRepository CarCategories { get; }

        Task CompleteAsync();
    }
}
