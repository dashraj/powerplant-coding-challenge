using AutoMapper;
using Application.Contracts.DTO;
using Application.Contracts.Managers;
using Core.Handlers;
using Core.Models;
using Core.Services;

namespace Application.Managers
{
    public class ProductionPlanManager : IProductionPlanManager
    {
        private readonly IPowerPlantHandler handler;
        private readonly IProductionPlanService service;
        private readonly IMapper mapper;

        public ProductionPlanManager(IPowerPlantHandler handler, IProductionPlanService service, IMapper mapper)
        {
            this.handler = handler;
            this.service = service;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<ProductionResponseDto>> GetProductionPlansAsync(ProductionRequestDto productionRequest)
        {
            if(productionRequest?.Powerplants?.Count <= 0 || productionRequest?.Fuels == null || productionRequest?.Load <= 0)
            {
                throw new ArgumentException("Invalid Request");
            }
            var fuel = mapper.Map<Fuel>(productionRequest?.Fuels);
            var powerPlants = productionRequest?.Powerplants?.Select(x => handler.ResolvePowerPlant(x.Name!, x.Type, x.Efficiency, x.PowerMaximum, x.PowerMinimum, fuel));
            return mapper.Map<IEnumerable<ProductionResponseDto>>(await service.GetProductionPlansAsync(powerPlants!, productionRequest!.Load));
        }
    }
}
