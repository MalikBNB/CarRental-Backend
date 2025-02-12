using AutoMapper;
using CarRental.Entities.DbSets;
using CarRental.Entities.DTOs.Incoming;
using CarRental.Entities.DTOs.Outgoing;

namespace CarRental.Api.Profiles;

public class CarCategoryProfile : Profile
{
    public CarCategoryProfile()
    {
        CreateMap<CarCategory, CarCategoryResponseDto>();

        CreateMap<CarCategoryRequestDto, CarCategory>();
    }
}
