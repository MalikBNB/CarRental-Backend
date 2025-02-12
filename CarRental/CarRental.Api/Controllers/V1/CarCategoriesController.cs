using AutoMapper;
using CarRental.Configuration.Messages;
using CarRental.DataService.Data;
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
public class CarCategoriesController : BaseController
{
    public CarCategoriesController(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        : base(unitOfWork, userManager, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var pagedResult = new PagedResult<CarCategoryResponseDto>();

        var carCategories = await _unitOfWork.CarCategories.GetAllAsync();

        foreach (var carCategory in carCategories)
            pagedResult.Content.Add(_mapper.Map<CarCategoryResponseDto>(carCategory));

        return Ok(pagedResult);
    }

    [HttpGet("/{id}")]
    public async Task<IActionResult> FindAsync([FromQuery]string id)
    {
        var result = new Result<CarCategoryResponseDto>();

        var carCategory = await _unitOfWork.CarCategories.FindAsync(o => o.Id == new Guid(id));
        if (carCategory is null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound);
            return NotFound(result);
        }

        result.Content = _mapper.Map<CarCategoryResponseDto>(carCategory);

        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddAsync([FromBody] CarCategoryRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = new Result<CarCategoryResponseDto>();

        if(await _unitOfWork.CarCategories.IsCategoryExits(dto.CategoryName.Trim()))
        {
            result.Error = PopulateError(400, ErrorMessages.Generic.BadRequest, ErrorMessages.CarCategory.CategoryAlreadyExists);
            return BadRequest(result);
        }

        var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
        if(loggedInUser is null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.User.UserNotFound);
            return NotFound(result);
        }

        var carCategory = _mapper.Map<CarCategory>(dto);

        await _unitOfWork.CarCategories.AddAsync(carCategory, loggedInUser);
        await _unitOfWork.CompleteAsync();

        result.Content = _mapper.Map<CarCategoryResponseDto>(carCategory);

        return CreatedAtRoute("FindAsync", new {carCategory.Id}, result);
    }
}
