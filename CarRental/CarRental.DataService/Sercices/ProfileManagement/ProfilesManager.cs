using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Configuration.Messages;
using CarRental.Entities.DbSets;
using CarRental.Entities.DTOs.Incoming;
using CarRental.Entities.DTOs.Outgoing;
using CarRental.Entities.Global;
using Microsoft.AspNetCore.Identity;

namespace CarRental.DataService.Sercices.ProfileManagement
{
    public class ProfilesManager : IProfilesManager
    {
        private readonly UserManager<User> _userManager;

        public ProfilesManager(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> UpdateAsync(string id, UpdateProfileDto dto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return null!;

            user.DateOfBirth = dto.DateOfBirth;
            user.Gendor = dto.Gendor;
            user.PhoneNumber = dto.Phone;
            user.PhoneNumberConfirmed = true;
            user.Address = dto.Address;
            user.Picture = dto.Picture;
            user.DriverLicenseNumber = dto.DriverLicenseNumber;
            user.Modified = DateTime.Now;

            await _userManager.UpdateAsync(user);

            return user;
        }
    }
}
