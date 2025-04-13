using System.Collections.Generic;
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

    //[HttpGet]
    //public async Task<IActionResult> GetAllAsync()
    //{
    //    var pagedResult = new PagedResult<CarCategoryResponseDto>();
    //    pagedResult.Content = new List<CarCategoryResponseDto>();

    //    var carCategories = await _unitOfWork.CarCategories.GetAllAsync();
    //    if(!carCategories.Any())
    //    {
    //        pagedResult.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound);
    //        return NotFound(pagedResult);
    //    }

    //    foreach (var carCategory in carCategories)
    //        pagedResult.Content.Add(_mapper.Map<CarCategoryResponseDto>(carCategory));

    //    pagedResult.ResultCount = pagedResult.Content.Count;

    //    return Ok(pagedResult);
    //}

    [HttpGet]
    public async Task<IActionResult> FindAllAsync(int? page, int? pageSize)
    {
        var pagedResult = new PagedResult<CarCategoryResponseDto>();
        pagedResult.Content = new List<CarCategoryResponseDto>();

        var categories = await _unitOfWork.CarCategories.GetAllAsync(page, pageSize, ["Vehicles"]);
        if (!categories.Any())
        {
            return NotFound(pagedResult);
        }

        foreach (var category in categories)
            pagedResult.Content.Add(_mapper.Map<CarCategoryResponseDto>(category));

        pagedResult.Page = page ?? 0;
        pagedResult.PageSize = pageSize ?? 0;
        pagedResult.ResultCount = pagedResult.Content.Count;

        return Ok(pagedResult);
    }


    [HttpGet("{id}", Name = "find-category"), Route("[controller]/FindAsync")]
    public async Task<IActionResult> FindAsync(Guid id)
    {
        var result = new Result<CarCategoryResponseDto>();

        var carCategory = await _unitOfWork.CarCategories.FindAsync(o => o.Id == id);
        if (carCategory is null)
        {
            result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound);
            return NotFound(result);
        }

        result.Content = _mapper.Map<CarCategoryResponseDto>(carCategory);

        return Ok(result);
    }

    [HttpPost]
    //[Authorize(Roles = $"{AppRoles.Admin}, {AppRoles.User}")]
    public async Task<IActionResult> AddAsync([FromBody] CarCategoryRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = new Result<CarCategoryRequestDto>();

        if (await _unitOfWork.CarCategories.IsCategoryExits(dto.CategoryName.Trim()))
        {
            result.Error = PopulateError(400, ErrorMessages.Generic.BadRequest, ErrorMessages.CarCategory.CategoryAlreadyExists);
            return BadRequest(result);
        }

        //var loggedInUser = await _userManager.GetUserAsync(User);
        //if (loggedInUser is null)
        //{
        //    result.Error = PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.User.UserNotFound);
        //    return NotFound(result);
        //}

        var carCategory = _mapper.Map<CarCategory>(dto);
        carCategory.Creator = "Malik07";//loggedInUser.UserName!;
        carCategory.Created = DateTime.Now;
        carCategory.Modifier = "Malik07";//loggedInUser.UserName!;
        carCategory.Modified = DateTime.Now;

        var isAdded = await _unitOfWork.CarCategories.AddAsync(carCategory);
        if (!isAdded)
        {
            result.Error = PopulateError(400, ErrorMessages.Generic.BadRequest, ErrorMessages.Generic.SomethingWentWrong);
            return BadRequest(result);
        }

        await _unitOfWork.CompleteAsync();

        result.Content = dto;

        return CreatedAtRoute("find-category", new { carCategory.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = $"{AppRoles.Admin}, {AppRoles.User}")]
    public async Task<IActionResult> UpdateAsync(Guid id, CarCategoryRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var loggedInUser = await _userManager.GetUserAsync(User);
        
        if (loggedInUser is null)
            return NotFound(PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.User.UserNotFound));

        var carCategory = await _unitOfWork.CarCategories.FindAsync(id);

        if (carCategory is null)
            return NotFound(PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound));

        carCategory.CategoryName = dto.CategoryName;
        carCategory.Modifier = loggedInUser.UserName!;
        carCategory.Modified = DateTime.Now;

        var isUpdated = _unitOfWork.CarCategories.Update(carCategory);

        if (!isUpdated)
            return BadRequest(PopulateError(400, ErrorMessages.Generic.BadRequest, ErrorMessages.Generic.SomethingWentWrong));

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = $"{AppRoles.Admin}, {AppRoles.User}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        //-- Here EF Core generates two SQL statements (Find, Delete), which seems a little bit unnecessary to find the entity first then delete it.
        //var carCategory = await _unitOfWork.CarCategories.FindAsync(id);
        //if (carCategory is null)
        //    return NotFound(PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound));

        if(!await _unitOfWork.CarCategories.AnyAsync(c => c.Id == id))
            return NotFound(PopulateError(404, ErrorMessages.Generic.ObjectNotFound, ErrorMessages.Generic.ObjectNotFound));

        //-- When we delete an entity, the only thing we need is the primary key. So, we can do this.
        var isDeleted = _unitOfWork.CarCategories.Delete(new CarCategory { Id = id });
        if (!isDeleted)
            return BadRequest(PopulateError(400, ErrorMessages.Generic.BadRequest, ErrorMessages.Generic.SomethingWentWrong));

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}
