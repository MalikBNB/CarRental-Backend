using AutoMapper;
using CarRental.Authentication.Models.DTOs.Incoming;
using CarRental.Entities.DbSets;
using CarRental.Entities.DTOs.Incoming;
using CarRental.Entities.DTOs.Outgoing;

namespace CarRental.Api.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegistrationRequestDto, User>();

        CreateMap<User, ProfileDto>();

    }
}
