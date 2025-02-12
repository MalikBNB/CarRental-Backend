using CarRental.DataService.Data;
using CarRental.DataService.IRepositories;
using CarRental.Entities.DbSets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataService.Repositories;
public class CarCategoriesRepository : GenericRepository<CarCategory>, ICarCategoriesRepository
{
    public CarCategoriesRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> IsCategoryExits(string name)
    {
        return await dbSet.AnyAsync(c => c.CategoryName == name);
    }
}
