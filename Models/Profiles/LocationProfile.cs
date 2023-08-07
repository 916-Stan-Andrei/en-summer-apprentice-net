using AutoMapper;
using TMS.Models.DTOs;

namespace TMS.Models.Profiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, LocationDTO>().ReverseMap();
        }
    }
}
