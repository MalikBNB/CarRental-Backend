using CarRental.DataService.Data;
using CarRental.DataService.IRepositories;
using CarRental.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataService.Repositories;
public class BookingsRepository : GenericRepository<RentalBooking>, IBookingsRepository
{
    public BookingsRepository(AppDbContext context) : base(context)
    {       
    }


}
