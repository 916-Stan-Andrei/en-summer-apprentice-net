using AutoMapper;
using TMS.Models.DTOs;

namespace TMS.Models.Profiles
{
    public class TicketCategoryProfile : Profile
    {
        public TicketCategoryProfile()
        {
            CreateMap<TicketCategory, TicketCategoryResponseDTO>().ReverseMap();
        }
    }
}
