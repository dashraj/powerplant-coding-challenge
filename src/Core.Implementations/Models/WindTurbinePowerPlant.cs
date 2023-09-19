using Core.Enums;
using Core.Models;

namespace Implementations.Models
{
    public class WindTurbinePowerPlant : PowerPlant
    {
        public WindTurbinePowerPlant(string Name, PowerPlantType Type, double Efficiency, double PowerMaximum, double PowerMinimum, Fuel fuel) : base(Name, Type, Efficiency, PowerMaximum, PowerMinimum, fuel)
        {
        }

        public override double CostPerUnit() => 0;


        public override double PoducibleMaximumUnits()
        {
            return (_fuel.Wind / 100) * _powerMaximum;
        }
    }
}

