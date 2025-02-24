using AutoMapper;
using CarRental.Entities.DbSets;
using CarRental.Entities.DTOs.Incoming;
using CarRental.Entities.DTOs.Outgoing;

namespace CarRental.Api.Profiles;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<BookingRequestDto, RentalBooking>()
            .ForMember(
                dest => dest.RentalTransaction,
                from => from.MapFrom(b => b.RentalTransaction)
            );

        CreateMap<RentalBooking, BookingResponseDto>()
            .ForMember(
                dest => dest.Customer,
                from => from.MapFrom(b => b.Customer)
            )
            .ForMember(
                dest => dest.Vehicle,
                from => from.MapFrom(b => b.Vehicle)
            );
    }
}
