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
            var productionPlans = new List<ProductionPlan>();
            var powerPlantGroups = powerPlants.OrderBy(x => x.CostPerUnit()).GroupBy(x => x.CostPerUnit()).ToList();
            var remainingLoad = load;

            foreach (var powerPlantsGroup in powerPlantGroups)
            {
                var minPower = powerPlantsGroup.Min(x => x.ProducibleMinimumUnits());

                remainingLoad = load - productionPlans.Sum(x => x.Power);

                if (remainingLoad < minPower)
                {
                    break;
                }

                productionPlans.AddRange(CalculateProductionPlansByCostPerUnit(powerPlantsGroup, remainingLoad).ToList());
            }


            if (productionPlans.Count() != powerPlants.Count())
            {
                var powerPlantNames = productionPlans.Select(p => p.Name ?? string.Empty).ToHashSet();

                var remainingPowerPlants = powerPlants.Where(x => !powerPlantNames.Contains(x.Name));

                // Append production plans for remaining load

                productionPlans.AddRange(CalculateProductionPlansByMinimumCostToRun(remainingPowerPlants, remainingLoad));


            }

            return PlanAdjustments(productionPlans, load).OrderBy(x => x.Order).ThenBy(x => x.CostPerUnit);
        }

        private static IEnumerable<ProductionPlan> PlanAdjustments(List<ProductionPlan> productionPlans, double load)
        {
            if(productionPlans.Sum(x=>x.Power) == load)
            {
                return productionPlans;
            }
            foreach (var productionPlan in productionPlans.OrderBy(x=>x.Power))
            {
                if(productionPlan.Power <= 0)
                {
                    continue;
                }
                var powerPlant = productionPlans.LastOrDefault(p => p != productionPlan);
                if(powerPlant?.Power > 0 )
                {
                    powerPlant.Power -= productionPlan.Power;
                }
                else
                {
                    productionPlan.Power = load;
                }
                break;

            }

            return productionPlans;
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

            foreach (var powerPlant in powerPlants)
            {
                if (remainingLoad > 0 && remainingLoad < powerPlant.ProducibleMinimumUnits())
                {
                    break;
                }

                double powerToProduce = 0;

                if (remainingLoad > 0)
                {
                    powerToProduce = Math.Min(remainingLoad, powerPlant.ProducibleMaximumUnits());

                    var nextPP = powerPlants.FirstOrDefault(pp => pp != powerPlant && pp.ProducibleMinimumUnits() > 0 && !productionPlans.Select(x=>x.Name).Contains(pp.Name));

                    double remaining = remainingLoad - powerToProduce;

                    if ( remaining > 0 && remaining < nextPP?.ProducibleMinimumUnits())
                    {
                        powerToProduce = Math.Min(remainingLoad, powerPlant.ProducibleMaximumUnits() - nextPP.ProducibleMinimumUnits());
                    }

                    remainingLoad -= powerToProduce;
                }

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
                : remainingLoad > 0 && remainingLoad < powerPlant.ProducibleMaximumUnits()
                    ? remainingLoad
                    : powerPlant.ProducibleMaximumUnits();
        }
    }
}
