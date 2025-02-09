using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Entities.DbSets;
using CarRental.Entities.DTOs.Incoming;
using CarRental.Entities.DTOs.Outgoing;
using CarRental.Entities.Global;

namespace CarRental.DataService.Sercices.ProfileManagement
{
    public interface IProfilesManager
    {
        Task<User> UpdateAsync(string id, UpdateProfileDto dto);
    }
}
