namespace Core.Implementations.Handlers.Tests.Handlers
{
    [TestClass]
    public class PowerPlantHandlersTests
    {
        [TestMethod]
        public void ResolvePowerPlant_ShouldCreateGasFiredPowerPlant_WhenPowerPlantTypeIsGasfired()
        {
            // Arrange
            var handlers = new PowerPlantHandlers();
            var name = "Gas Plant";
            var type = PowerPlantType.Gasfired;
            var efficiency = 0.9;
            var powerMaximum = 100;
            var powerMinimum = 10;
            var fuel = new Fuel();

            // Act
            var powerPlant = handlers.ResolvePowerPlant(name, type, efficiency, powerMaximum, powerMinimum, fuel);

            // Assert
            Assert.IsInstanceOfType(powerPlant, typeof(GasFiredPowerPlant));
            Assert.AreEqual(name, powerPlant.Name);
            Assert.AreEqual(efficiency, powerPlant.Efficiency);
            Assert.AreEqual(powerMaximum, powerPlant.PowerMaximum);
            Assert.AreEqual(powerMinimum, powerPlant.PowerMinimum);
        }

        [TestMethod]
        public void ResolvePowerPlant_ShouldCreateTurbojetPowerPlant_WhenPowerPlantTypeIsTurbojet()
        {
            // Arrange
            var handlers = new PowerPlantHandlers();
            var name = "Jet Plant";
            var type = PowerPlantType.Turbojet;
            var efficiency = 0.8;
            var powerMaximum = 200;
            var powerMinimum = 20;
            var fuel = new Fuel();

            // Act
            var powerPlant = handlers.ResolvePowerPlant(name, type, efficiency, powerMaximum, powerMinimum, fuel);

            // Assert
            Assert.IsInstanceOfType(powerPlant, typeof(TurbojetPowerPlant));
            Assert.AreEqual(name, powerPlant.Name);
            Assert.AreEqual(efficiency, powerPlant.Efficiency);
            Assert.AreEqual(powerMaximum, powerPlant.PowerMaximum);
            Assert.AreEqual(powerMinimum, powerPlant.PowerMinimum);
        }

        [TestMethod]
        public void ResolvePowerPlant_ShouldCreateWindTurbinePowerPlant_WhenPowerPlantTypeIsWindturbine()
        {
            // Arrange
            var handlers = new PowerPlantHandlers();
            var name = "Wind Plant";
            var type = PowerPlantType.Windturbine;
            var efficiency = 0.75;
            var powerMaximum = 50;
            var powerMinimum = 5;
            var fuel = new Fuel();

            // Act
            var powerPlant = handlers.ResolvePowerPlant(name, type, efficiency, powerMaximum, powerMinimum, fuel);

            // Assert
            Assert.IsInstanceOfType(powerPlant, typeof(WindTurbinePowerPlant));
            Assert.AreEqual(name, powerPlant.Name);
            Assert.AreEqual(efficiency, powerPlant.Efficiency);
            Assert.AreEqual(powerMaximum, powerPlant.PowerMaximum);
            Assert.AreEqual(powerMinimum, powerPlant.PowerMinimum);
        }

        [TestMethod]
        public void ResolvePowerPlant_ShouldThrowArgumentNullException_WhenNameIsNullOrEmpty()
        {
            // Arrange
            var handlers = new PowerPlantHandlers();
            var name = "";
            var type = PowerPlantType.Gasfired;
            var efficiency = 0.9;
            var powerMaximum = 100;
            var powerMinimum = 10;
            var fuel = new Fuel();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                handlers.ResolvePowerPlant(name, type, efficiency, powerMaximum, powerMinimum, fuel);
            });
        }

        [TestMethod]
        public void ResolvePowerPlant_ShouldThrowArgumentOutOfRangeException_WhenPowerPlantTypeIsUnknown()
        {
            // Arrange
            var handlers = new PowerPlantHandlers();
            var name = "Unknown Plant";
            var type = (PowerPlantType)100; // A value that doesn't correspond to any known type
            var efficiency = 0.9;
            var powerMaximum = 100;
            var powerMinimum = 10;
            var fuel = new Fuel();

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                handlers.ResolvePowerPlant(name, type, efficiency, powerMaximum, powerMinimum, fuel);
            });
        }
    }

}