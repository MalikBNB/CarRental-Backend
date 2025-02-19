using AutoMapper;
using CarRental.Configuration.Messages;
using CarRental.DataService.IConfiguration;
using CarRental.Entities.DbSets;
using CarRental.Entities.DTOs.Incoming;
using CarRental.Entities.DTOs.Outgoing;
using CarRental.Entities.Global;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.V1;
[Route("api/[controller]")]
[ApiController]
public class VehiclesController : BaseController
{
    public VehiclesController(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper) 
        : base(unitOfWork, userManager, mapper)
    {      
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var pagedResult = new PagedResult<VehicleResponseDto>();
        pagedResult.Content = new List<VehicleResponseDto>();

        var vehicles = await _unitOfWork.Vehicles.GetAllAsync(["CarCategory"]);
        if (!vehicles.Any())
        {
            return NotFound(pagedResult);
        }

        foreach (var vehicle in vehicles)
            pagedResult.Content.Add(_mapper.Map<VehicleResponseDto>(vehicle));

        pagedResult.ResultCount = pagedResult.Content.Count;

        return Ok(pagedResult); 
    }


    [HttpGet("page/{page}-{pageSize}")]
    public async Task<IActionResult> FindAllAsync(int page, int pageSize)
    {
        var pagedResult = new PagedResult<VehicleResponseDto>();
        pagedResult.Content = new List<VehicleResponseDto>();

        var vehicles = await _unitOfWork.Vehicles.FindAllAsync(v => v.Status == 1, (page - 1) * pageSize, pageSize, ["CarCategory"]);
        if (!vehicles.Any())
        {
            return NotFound(pagedResult);
        }

        foreach (var vehicle in vehicles)
            pagedResult.Content.Add(_mapper.Map<VehicleResponseDto>(vehicle));

        pagedResult.Page = page;
        pagedResult.PageSize = pageSize;
        pagedResult.ResultCount = pagedResult.Content.Count;

        return Ok(pagedResult);
    }


    [HttpGet("{id}", Name = "find-vehicle"), Route("[controller]/FindAsync")]
    public async Task<IActionResult> FindAsync(Guid id)
    {
        var result = new Result<VehicleResponseDto>();

        var vehicle = await _unitOfWork.Vehicles.FindAsync(v => v.Id == id && v.Status == 1, ["CarCategory"]);
        if(vehicle is null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound);
            return NotFound(result);
        }

        result.Content = _mapper.Map<VehicleResponseDto>(vehicle);

        return Ok(result);
    }


    [HttpPost]
    [Authorize(Roles = $"{AppRoles.Admin}, {AppRoles.User}")]
    public async Task<IActionResult> AddAsync(VehicleRequestDto dto)
    {
        if(!ModelState.IsValid) 
            return BadRequest(ModelState);

        var result = new Result<VehicleRequestDto>();

        var vehicleExist = await _unitOfWork.Vehicles.FindAsync(v => v.PlateNumber == dto.PlateNumber &&
                                                                v.Status == 1);
        if(vehicleExist is not null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.BadRequest, ErrorMessages.Vehicle.VehicleAlreadyExists);
            return BadRequest(result);
        }

        var user = await _userManager.GetUserAsync(User);
        if(user is null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.User.UserNotFound);
            return NotFound(result);
        }

        var vehicle = _mapper.Map<Vehicle>(dto);
        vehicle.Creator = user.UserName!;
        vehicle.Created = DateTime.Now;
        vehicle.Modifier = user.UserName!;
        vehicle.Modified = DateTime.Now;

        var isAdded = await _unitOfWork.Vehicles.AddAsync(vehicle);
        if (!isAdded)
        {
            result.Error = PopulateError(400, ErrorMessages.Generic.BadRequest, ErrorMessages.Generic.SomethingWentWrong);
            return BadRequest(result);
        }

        await _unitOfWork.CompleteAsync();

        result.Content = dto;

        return CreatedAtRoute("find-vehicle", new {vehicle.Id}, result);
    }


    [HttpPut("{id}")]
    [Authorize(Roles = $"{AppRoles.Admin}, {AppRoles.User}")]
    public async Task<IActionResult> UpdateAsync(Guid id, VehicleRequestDto dto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = new Result<VehicleResponseDto>();

        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.User.UserNotFound);
            return NotFound(result);
        }

        var vehicle = await _unitOfWork.Vehicles.FindAsync(v => v.Id == id && v.Status == 1);
        if(vehicle is null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound);
            return NotFound(result);
        }

        vehicle = _mapper.Map<Vehicle>(dto);
        vehicle.Modifier = user.UserName!;
        vehicle.Modified = DateTime.Now;

        var isUpdated = _unitOfWork.Vehicles.Update(vehicle);
        if (!isUpdated)
            return BadRequest(PopulateError(400, ErrorMessages.Generic.BadRequest, ErrorMessages.Generic.SomethingWentWrong));

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }


    [HttpDelete("{id}")]
    [Authorize(Roles = $"{AppRoles.Admin}, {AppRoles.User}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        if (!await _unitOfWork.Vehicles.AnyAsync(v => v.Id == id && v.Status == 1))
            return NotFound(PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound));

        var isDeleted = _unitOfWork.Vehicles.Delete(new Vehicle { Id = id });
        if (!isDeleted)
            return BadRequest(PopulateError(400, ErrorMessages.Generic.BadRequest, ErrorMessages.Generic.SomethingWentWrong));

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}
