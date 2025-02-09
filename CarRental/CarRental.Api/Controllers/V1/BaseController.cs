using AutoMapper;
using CarRental.DataService.Data;
using CarRental.DataService.IConfiguration;
using CarRental.Entities.DbSets;
using CarRental.Entities.Global;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.V1;
[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    public readonly IUnitOfWork _unitOfWork;
    public readonly UserManager<User> _userManager;
    public readonly IMapper _mapper;

    public BaseController(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _mapper = mapper;
    }

    internal Error PopulateError(int code, string type, string message)
    {
        return new Error
        {
            Code = code,
            Type = type,
            Message = message
        };
    }
}
