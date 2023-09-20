using Core.Models;
using Core.Services;

namespace Core.Implementations.Services
{
    public class ProductionPlanService : IProductionPlanService
    {
        public async Task<IEnumerable<ProductionPlan>> GetProductionPlansAsync(IEnumerable<PowerPlant> powerPlants, double load)
        {
            // Calculate production plans by merit order
            var finalPlans = CalculateProductionPlansByMeritOrder(powerPlants, load);

            // Use Task.FromResult for asynchronous result
            return await Task.FromResult(finalPlans);
        }

        private static IEnumerable<ProductionPlan> CalculateProductionPlansByMeritOrder(IEnumerable<PowerPlant> powerPlants, double load)
        {
            var productionPlans = CalculateProductionPlansByCostPerUnit(powerPlants, load).ToList();

            var remainingLoad = load - productionPlans.Sum(x => x.Power);

            if (remainingLoad > 0)
            {
                var powerPlantNames = productionPlans.Select(p => p.Name ?? string.Empty).ToHashSet();

                var remainingPowerPlants = powerPlants.Where(x => !powerPlantNames.Contains(x.Name));

                // Append production plans for remaining load
                var plans = CalculateProductionPlansByMinimumCostToRun(remainingPowerPlants, remainingLoad);
                productionPlans.AddRange(plans);
            }

            return productionPlans.OrderBy(x => x.Order).ThenBy(x => x.CostPerUnit);
        }

        private static IEnumerable<ProductionPlan> CalculateProductionPlansByMinimumCostToRun(IEnumerable<PowerPlant> powerPlants, double remainingLoad)
        {
            var productionPlans = new List<ProductionPlan>();

            // Sort power plants by minimum cost to run
            var sortedByMinimumCostToRun = powerPlants.OrderBy(y => CalculateMinPowerCanBeProducedBasedOnLoad(remainingLoad, y) * y.CostPerUnit());

            foreach (var powerPlant in sortedByMinimumCostToRun)
            {
                double powerToProduce = 0;

                if (remainingLoad > 0)
                {
                    powerToProduce = Math.Max(remainingLoad, powerPlant.ProducibleMinimumUnits());

                    remainingLoad -= powerToProduce;
                }

                // Create and add production plan
                productionPlans.Add(CreateProductionPlan(remainingLoad, powerPlant, powerToProduce));
            }

            return productionPlans;
        }

        private static IEnumerable<ProductionPlan> CalculateProductionPlansByCostPerUnit(IEnumerable<PowerPlant> powerPlants, double load)
        {
            var productionPlans = new List<ProductionPlan>();
            var remainingLoad = load;

            foreach (var powerPlant in powerPlants.OrderBy(p => p.CostPerUnit()))
            {
                if (remainingLoad > 0 && remainingLoad < powerPlant.ProducibleMinimumUnits())
                {
                    break;
                }

                double powerToProduce = 0;

                if (remainingLoad > 0)
                {
                    powerToProduce = Math.Min(remainingLoad, powerPlant.ProducibleMaximumUnits());

                    remainingLoad -= powerToProduce;
                }

                // Create and add production plan
                productionPlans.Add(CreateProductionPlan(remainingLoad, powerPlant, powerToProduce));
            }

            return productionPlans;
        }

        private static ProductionPlan CreateProductionPlan(double remainingLoad, PowerPlant powerPlant, double powerToProduce)
        {
            double minPower = CalculateMinPowerCanBeProducedBasedOnLoad(remainingLoad, powerPlant);
            return new ProductionPlan
            {
                Name = powerPlant.Name,
                Power = Math.Round(powerToProduce, 1),
                MinPower = minPower,
                CostPerUnit = powerPlant.CostPerUnit()
            };
        }

        private static double CalculateMinPowerCanBeProducedBasedOnLoad(double remainingLoad, PowerPlant powerPlant)
        {
            return powerPlant.ProducibleMinimumUnits() > 0
                ? powerPlant.ProducibleMinimumUnits()
                : remainingLoad >0 && remainingLoad < powerPlant.ProducibleMaximumUnits()
                    ? remainingLoad
                    : powerPlant.ProducibleMaximumUnits();
        }
    }
}
