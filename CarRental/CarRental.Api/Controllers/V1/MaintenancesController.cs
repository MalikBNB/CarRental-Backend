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
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Api.Controllers.V1;
[Route("api/[controller]")]
[ApiController]
public class MaintenancesController : BaseController
{
    public MaintenancesController(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper) : base(unitOfWork, userManager, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(int? page, int? pageSize)
    {
        var pagedResult = new PagedResult<MaintenanceResponseDto>();
        pagedResult.Content = new List<MaintenanceResponseDto>();

        var maintnances = await _unitOfWork.Maintenances.GetAllAsync(page, pageSize, ["maintenance"]);
        if (!maintnances.Any())
        {
            pagedResult.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound);
            return NotFound(pagedResult);
        }

        foreach (var maintnance in maintnances)
            pagedResult.Content.Add(_mapper.Map<MaintenanceResponseDto>(maintnance));

        pagedResult.ResultCount = pagedResult.Content.Count;
        pagedResult.Page = (int)page!;
        pagedResult.PageSize = (int)pageSize!;

        return Ok(pagedResult);
    }

    [HttpGet("{id}", Name = "find-maintnance")]
    public async Task<IActionResult> FindAsync(Guid id)
    {
        var result = new Result<MaintenanceResponseDto>();

        var maintnance = await _unitOfWork.Maintenances.FindAsync(m => m.Id == id, ["maintenance"]);
        if (maintnance is null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound);
            return NotFound(result);
        }

        result.Content = _mapper.Map<MaintenanceResponseDto>(maintnance);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = $"{AppRoles.Admin}, {AppRoles.User}")]
    public async Task<IActionResult> AddAsync(MaintenanceRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = new Result<MaintenanceRequestDto>();

        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.User.UserNotFound);
            return NotFound(result);
        }

        var vehicle = await _unitOfWork.Vehicles.FindAsync(dto.VehicleId);
        if(vehicle is null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Vehicle.VehicleNotExist);
            return NotFound(result);
        }

        vehicle.IsAvailableForRent = false;
        vehicle.Modifier = user.UserName!;
        vehicle.Modified = DateTime.Now;

        var isUpdated = _unitOfWork.Vehicles.Update(vehicle);
        if (!isUpdated)
        {
            result.Error = PopulateError(400, ErrorMessages.Generic.BadRequest, ErrorMessages.Generic.SomethingWentWrong);
            return BadRequest(result);
        }

        var maintenance = _mapper.Map<Maintenance>(dto);
        maintenance.Creator = user.UserName!;
        maintenance.Modifier = user.UserName!;
        maintenance.Created = DateTime.Now;
        maintenance.Modified = DateTime.Now;

        var isAdded = await _unitOfWork.Maintenances.AddAsync(maintenance);
        if (!isAdded)
        {
            result.Error = PopulateError(400, ErrorMessages.Generic.BadRequest, ErrorMessages.Generic.SomethingWentWrong);
            return BadRequest(result);
        }

        await _unitOfWork.CompleteAsync();

        result.Content = dto;

        return CreatedAtRoute("find-maintnance", new { maintenance.Id }, result);
    }

    [HttpPut]
    [Authorize(Roles = $"{AppRoles.Admin}, {AppRoles.User}")]
    public async Task<IActionResult> UpdateAsync(Guid id, MaintenanceRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = new Result<MaintenanceRequestDto>();

        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.User.UserNotFound);
            return NotFound(result);
        }

        var maintenance = await _unitOfWork.Maintenances.FindAsync(m => m.Id == id);
        if (maintenance is null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound);
            return NotFound(result);
        }

        maintenance = _mapper.Map<Maintenance>(dto);
        maintenance.Modifier = user.UserName!;
        maintenance.Modified = DateTime.Now;

        var isUpdated = _unitOfWork.Maintenances.Update(maintenance);
        if (!isUpdated)
            return BadRequest(PopulateError(400, ErrorMessages.Generic.BadRequest, ErrorMessages.Generic.SomethingWentWrong));

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpDelete]
    [Authorize(Roles = $"{AppRoles.Admin}, {AppRoles.User}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        if(await _unitOfWork.Maintenances.FindAsync(m => m.Id == id) is not null)
            return NotFound(PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound));

        var isDeleted = _unitOfWork.Maintenances.Delete(new Maintenance { Id = id });
        if (!isDeleted)
            return BadRequest(PopulateError(400, ErrorMessages.Generic.BadRequest, ErrorMessages.Generic.SomethingWentWrong));

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}
