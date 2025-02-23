using AutoMapper;
using CarRental.Entities.DbSets;
using CarRental.Entities.DTOs.Incoming;
using CarRental.Entities.DTOs.Outgoing;

namespace CarRental.Api.Profiles;

public class MaintenanceProfile : Profile
{
    public MaintenanceProfile()
    {
        CreateMap<MaintenanceRequestDto, Maintenance>();
        CreateMap<Maintenance, MaintenanceResponseDto>();
    }
}
