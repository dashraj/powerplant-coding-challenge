using Application.Contracts.DTO;
using AutoMapper;
using Core.Models;

namespace Application.Mappers
{
    public  class ProductionResponseDtoMapper : Profile
    {
        public ProductionResponseDtoMapper()
        {
            CreateMap<ProductionPlan, ProductionResponseDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(m => m.Name))                
                .ForMember(x => x.Power, opt => opt.MapFrom(m => m.Power));
        }
    }
}
