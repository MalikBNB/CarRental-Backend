using CarRental.DataService.IConfiguration;
using CarRental.DataService.IRepositories;
using CarRental.DataService.Repositories;
using CarRental.Entities.DbSets;
using Microsoft.Extensions.Logging;

namespace CarRental.DataService.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        //private readonly ILogger _logger;

        public IVehiclesRepository Vehicles {  get; private set; }  
        public ICarCategoriesRepository CarCategories { get; private set; }
        public IMaintnancesRepository Maintenances { get; private set; }

        public IBookingsRepository RentalBookings {  get; private set; }

        public UnitOfWork(AppDbContext context)//, ILoggerFactory loggerFactory)
        {
            _context = context;
            //_logger = loggerFactory.CreateLogger("db_logs");

            Vehicles = new VehiclesRepository(context);
            CarCategories = new CarCategoriesRepository(context);
            Maintenances = new MaintnancesRepository(context);
            RentalBookings = new BookingsRepository(context);
        }


        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
