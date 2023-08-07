using AutoMapper;
using TMS.Models.DTOs;

namespace TMS.Models.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventResponseDTO>()
                .ForMember(dest => dest.locationDTO, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.TicketCategories, opt => opt.MapFrom(src => src.TicketCategories))
                .ReverseMap();
        }
    }
}
