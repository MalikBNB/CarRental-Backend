using AutoMapper;
using CarRental.Entities.DbSets;
using CarRental.Entities.DTOs.Incoming;
using CarRental.Entities.DTOs.Outgoing;

namespace CarRental.Api.Profiles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<VehicleRequestDto, Vehicle>();

            CreateMap<Vehicle, VehicleResponseDto>()
                .ForMember( dest => dest.CarCategory, from => from.MapFrom(v => v.CarCategory.CategoryName));
        }
    }
}
