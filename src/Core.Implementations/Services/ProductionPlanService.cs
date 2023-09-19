using Core.Models;
using Core.Services;

namespace Core.Implementations.Services
{
    public class ProductionPlanService : IProductionPlanService
    {
        public Task<IEnumerable<ProductionPlan>> GetProductionPlansAsync(IEnumerable<PowerPlant> powerPlants, double load)
        {
            var response = new List<ProductionPlan>();
            var remainingLoad = load;

            var sortedPowerPlants = powerPlants
                .OrderBy(p => p.CostPerUnit())
                .ToList();

            foreach (var powerPlant in sortedPowerPlants)
            {

                double powerToProduce = 0;

                if (remainingLoad > 0)
                {
                    powerToProduce = Math.Min(remainingLoad, powerPlant.PoducibleMaximumUnits());

                    remainingLoad -= powerToProduce;

                }
                response.Add(new ProductionPlan { Name = powerPlant.Name, Power = Math.Round(powerToProduce,1), TotalCost = powerPlant.CostPerUnit() * powerToProduce });

            }

            return Task.FromResult(response.AsEnumerable());
        }
    }
}
