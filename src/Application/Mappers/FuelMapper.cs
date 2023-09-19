using Application.Contracts.DTO;
using AutoMapper;
using Core.Models;

namespace Application.Mappers
{
    public  class FuelMapper : Profile
    {
        public FuelMapper() {
            CreateMap<Fuel, FuelDto>()
                    .ForMember(x => x.Gas, opt => opt.MapFrom(m => m.Gas))
                    .ForMember(x => x.Wind, opt => opt.MapFrom(m => m.Wind))
                    .ForMember(x => x.Co2Cost, opt => opt.MapFrom(m => m.CO2))
                    .ForMember(x => x.Kerosine, opt => opt.MapFrom(m => m.Kerosene)).ReverseMap();
        } 
    }
}
