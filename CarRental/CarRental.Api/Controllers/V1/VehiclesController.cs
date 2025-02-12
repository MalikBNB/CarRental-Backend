using AutoMapper;
using CarRental.DataService.IConfiguration;
using CarRental.Entities.DbSets;
using CarRental.Entities.DTOs.Outgoing;
using CarRental.Entities.Global;
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

    //[HttpGet]
    //public async Task<IActionResult> GetAllAsync()
    //{
    //    var result = new PagedResult<VehicleResponseDto>();
    //}
}
