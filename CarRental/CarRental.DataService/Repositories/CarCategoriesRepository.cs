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

    public override async Task<bool> AddAsync(CarCategory entity, User user)
    {
        entity.Creator = user.UserName!;
        entity.Created = DateTime.Now;
        entity.Modifier = user.UserName!;
        entity.Modified = DateTime.Now;

        await dbSet.AddAsync(entity);
        return true;
    }

    public async Task<bool> IsCategoryExits(string name)
    {
        return await dbSet.SingleOrDefaultAsync(c => c.CategoryName == name) is not null;
    }
}
