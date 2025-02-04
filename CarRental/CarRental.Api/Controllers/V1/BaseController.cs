using AutoMapper;
using CarRental.Entities.DbSets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers.V1;
[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    public UserManager<User> _userManager;
    public readonly IMapper _mapper;

    public BaseController(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
}
