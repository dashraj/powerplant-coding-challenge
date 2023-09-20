namespace Core.Implementations.Services.Tests.Services
{
    [TestClass]
    public class ProductionPlanServiceTests
    {
        [TestMethod]
        public async Task GetProductionPlansAsync_ShouldReturnEmptyList_WhenPowerPlantsIsEmpty()
        {
            // Arrange
            var service = new ProductionPlanService();
            var powerPlants = new List<PowerPlant>();
            double load = 100;

            // Act
            var result = await service.GetProductionPlansAsync(powerPlants, load);

            // Assert
            CollectionAssert.AreEqual(new List<ProductionPlan>(), result.ToList());
        }

        [TestMethod]
        public async Task GetProductionPlansAsync_ShouldCalculateProductionPlansCorrectly()
        {
            // Arrange
            var service = new ProductionPlanService();
            var Fuel = new Fuel { CO2 = 20, Gas = 20, Kerosene = 50, Wind = 75 };
            var powerPlants = new List<PowerPlant>
                                    {
                                        new TurbojetPowerPlant("Plant1",PowerPlantType.Turbojet,0.3,25,0,Fuel) ,
                                        new GasFiredPowerPlant("Plant2",PowerPlantType.Gasfired,0.5,500,100,Fuel),
                                        new WindTurbinePowerPlant("Plant3",PowerPlantType.Windturbine,1,100,0,Fuel),
                                    };
            double load = 100;

            // Act
            var result = await service.GetProductionPlansAsync(powerPlants, load);

            // Assert
            Assert.AreEqual(3, result.Count());
        }
    }

}