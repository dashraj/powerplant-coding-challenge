using Core.Enums;

namespace Core.Models
{
    public abstract class PowerPlant
    {
        protected readonly string _name;
        protected readonly PowerPlantType _type;
        protected readonly double _efficiency;
        protected readonly double _powerMaximum;
        protected readonly double _powerMinimum;
        protected readonly Fuel _fuel;
                
        public PowerPlant(string Name, PowerPlantType Type, double Efficiency, double PowerMaximum, double PowerMinimum, Fuel fuel)
        {
            this._name = Name;
            this._type = Type;
            this._efficiency = Efficiency;
            this._powerMaximum = PowerMaximum;
            this._powerMinimum = PowerMinimum;
            this._fuel = fuel;
        }

        public string Name {  get { return _name; } }

        public double Efficiency { get { return _efficiency; } }

        public double PowerMaximum { get { return _powerMaximum; } }

        public double PowerMinimum { get { return _powerMinimum; } }

        public abstract double CostPerUnit();

        public virtual double ProducibleMinimumUnits() => this._powerMinimum;
        public virtual double ProducibleMaximumUnits()=> this._powerMaximum;

    }

}

