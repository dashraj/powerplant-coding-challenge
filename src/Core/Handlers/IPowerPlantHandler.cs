using Core.Enums;
using Core.Models;

namespace Core.Handlers
{
    public interface IPowerPlantHandler
    {
        PowerPlant ResolvePowerPlant(string Name, PowerPlantType Type, double Efficiency, double PowerMaximum, double PowerMinimum, Fuel fuel);
    }
}
