using AutoMapper;
using CarRental.Configuration.Messages;
using CarRental.DataService.IConfiguration;
using CarRental.Entities.DbSets;
using CarRental.Entities.DTOs.Incoming;
using CarRental.Entities.DTOs.Outgoing;
using CarRental.Entities.Global;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Api.Controllers.V1;
[Route("api/[controller]")]
[ApiController]
public class BookingsController : BaseController
{
    public BookingsController(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper) : base(unitOfWork, userManager, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(int? page, int? pageSize)
    {
        var pagedResult = new PagedResult<BookingResponseDto>();
        pagedResult.Content = new List<BookingResponseDto>();

        var bookings = await _unitOfWork.RentalBookings.GetAllAsync(page, pageSize, ["User", "Vehicle", "RentalTransaction"]);
        if (bookings.Any())
        {
            pagedResult.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound);
            return NotFound(pagedResult);
        }

        foreach (var booking in bookings)
            pagedResult.Content.Add(_mapper.Map<BookingResponseDto>(booking));

        pagedResult.Page = page ?? 0;
        pagedResult.PageSize = pageSize ?? 0;
        pagedResult.ResultCount = pagedResult.Content.Count;

        return Ok(pagedResult);
    }

    [HttpGet("{id}", Name = "find-booking")]
    public async Task<IActionResult> FindAsync(Guid id)
    {
        var result = new Result<BookingResponseDto>();

        var booking = await _unitOfWork.RentalBookings.FindAsync(b => b.Id == id, ["User", "Vehicle", "RentalTransaction"]);
        if (booking is null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound);
            return NotFound(result);
        }

        result.Content = _mapper.Map<BookingResponseDto>(booking);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(BookingRequestDto dto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = new Result<BookingRequestDto>();

        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.User.UserNotFound);
            return NotFound(result);
        }

        var vehicle = await _unitOfWork.Vehicles.FindAsync(dto.VehicleId);
        if (vehicle is null || !vehicle.IsAvailableForRent)
        {
            result.Error = PopulateError(400, ErrorMessages.Generic.BadRequest, ErrorMessages.Vehicle.VehicleNotAvailable);
            return BadRequest(result);
        }

        var customer = await _userManager.FindByIdAsync(dto.CustomerId);
        if(customer is null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Customer.CustomerNotExist);
            return NotFound(result);
        }

        var booking = _mapper.Map<RentalBooking>(dto);
        booking.Creator = user.UserName!;
        booking.Modifier = user.UserName!;
        booking.Created = DateTime.Now;
        booking.Modified = DateTime.Now;

        

        return CreatedAtRoute("find-booking", new {}, result);
    }
}
