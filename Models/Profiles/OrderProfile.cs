using AutoMapper;
using TMS.Models.DTOs;

namespace TMS.Models.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponseDTO>()
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.Ticketcategory.EventId))
                .ReverseMap();
            CreateMap<Order, OrderRequestPatchDTO>().ReverseMap();
        }
    }
}
