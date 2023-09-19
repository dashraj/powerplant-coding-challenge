using Application.Contracts.DTO;

namespace Application.Contracts.Managers
{
    public interface IProductionPlanManager
    {
        Task<IEnumerable<ProductionResponseDto>> GetProductionPlansAsync(ProductionRequestDto productionRequest);
        
    }
}
