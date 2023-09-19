using Core.Constants;
using Core.Enums;
using Core.Models;

namespace Implementations.Models
{
    public class GasFiredPowerPlant : PowerPlant
    {
        public GasFiredPowerPlant(string Name, PowerPlantType Type, double Efficiency, double PowerMaximum, double PowerMinimum, Fuel fuel) : base(Name, Type, Efficiency, PowerMaximum, PowerMinimum, fuel)
        {
        }

        public override double CostPerUnit()
        {
            return (_fuel.Gas / _efficiency) + AppConstants._co2Emission_PerUnint * _fuel.CO2;
        }
    }
}

