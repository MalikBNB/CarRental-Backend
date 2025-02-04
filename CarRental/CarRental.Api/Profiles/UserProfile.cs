using AutoMapper;
using CarRental.Authentication.Models.DTOs.Incoming;
using CarRental.Entities.DbSets;

namespace CarRental.Api.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegistrationRequestDto, User>();
    }
}
