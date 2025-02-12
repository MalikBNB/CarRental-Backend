﻿using CarRental.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataService.IRepositories;
public interface ICarCategoriesRepository : IGenericRepository<CarCategory>
{
    Task<bool> IsCategoryExits(string name);
}
