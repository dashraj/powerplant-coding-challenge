using Core.Enums;
using Core.Handlers;
using Core.Models;
using Implementations.Models;

namespace Core.Implementations.Handlers
{
    public class PowerPlantHandlers : IPowerPlantHandler
    {
        public PowerPlant ResolvePowerPlant(string name, PowerPlantType type, double efficiency, double powerMaximum, double powerMinimum, Fuel fuel)
        {
            if (string.IsNullOrEmpty(name.Trim())) throw new ArgumentNullException("name");


            return type switch
            {
                PowerPlantType.Gasfired => new GasFiredPowerPlant(name, type, efficiency, powerMaximum, powerMinimum, fuel),

                PowerPlantType.Turbojet => new TurbojetPowerPlant(name, type, efficiency, powerMaximum, powerMinimum, fuel),

                PowerPlantType.Windturbine => new WindTurbinePowerPlant(name, type, efficiency, powerMaximum, powerMinimum, fuel),

                _ => throw new ArgumentOutOfRangeException(name, $"{type} is an unknown power plant type")
            };
        }
    }
}
