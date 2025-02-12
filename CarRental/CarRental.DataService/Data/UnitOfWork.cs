using CarRental.DataService.IConfiguration;
using CarRental.DataService.IRepositories;
using CarRental.DataService.Repositories;
using Microsoft.Extensions.Logging;

namespace CarRental.DataService.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        //private readonly ILogger _logger;

        public IVehiclesRepository Vehicles {  get; private set; }  
        public ICarCategoriesRepository CarCategories { get; private set; }

        public UnitOfWork(AppDbContext context)//, ILoggerFactory loggerFactory)
        {
            _context = context;
            //_logger = loggerFactory.CreateLogger("db_logs");

            Vehicles = new VehiclesRepository(context);
            CarCategories = new CarCategoriesRepository(context);
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
