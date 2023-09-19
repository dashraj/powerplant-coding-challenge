using Core.Models;

namespace Core.Services
{
    public  interface IProductionPlanService
    {
        Task<IEnumerable<ProductionPlan>> GetProductionPlansAsync(IEnumerable<PowerPlant>powerPlants, double load);
    }
}
