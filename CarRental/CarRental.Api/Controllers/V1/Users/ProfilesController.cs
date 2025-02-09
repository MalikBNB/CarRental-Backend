using AutoMapper;
using CarRental.Configuration.Messages;
using CarRental.DataService.IConfiguration;
using CarRental.DataService.Sercices.ProfileManagement;
using CarRental.Entities.DbSets;
using CarRental.Entities.DTOs.Incoming;
using CarRental.Entities.DTOs.Outgoing;
using CarRental.Entities.Global;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.V1.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : BaseController
    {
        private readonly IProfilesManager _profilesManager;

        public ProfilesController(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper, IProfilesManager profilesManager)
            : base(unitOfWork, userManager, mapper)
        {
            _profilesManager = profilesManager;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var result = new Result<ProfileDto>();

            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                result.Error = PopulateError(404, ErrorMessages.User.UserNotFound, ErrorMessages.Generic.ObjectNotFound);
                return BadRequest(result);
            }

            var profile = _mapper.Map<ProfileDto>(user);

            result.Content = profile;

            return Ok(result);
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> UpdateProfile(string id, [FromBody] UpdateProfileDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = new Result<ProfileDto>();

            var user = await _profilesManager.UpdateAsync(id, dto);
            if(user is null)
            {
                result.Error = PopulateError(404, ErrorMessages.User.UserNotFound, ErrorMessages.Generic.ObjectNotFound);
                return BadRequest(result);
            }

            result.Content = _mapper.Map<ProfileDto>(user);

            return Ok(result);
        }
    }
}
