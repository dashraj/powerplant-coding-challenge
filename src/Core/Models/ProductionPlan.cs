namespace Core.Models
{

    public class ProductionPlan
    {
        public string? Name { get; set; }
        public double Power { get; set; }
        public double MinPower { get; set; }
        public double CostPerUnit { get; set; }
        public double TotalCost { get { return Math.Round(this.Power * this.CostPerUnit, 1); } }
        public double MinCostToRun { get { return Math.Round(this.MinPower * this.CostPerUnit, 1); } }

        public int Order { get { return this.Power > 0 ? 0 : 1; } }
    }

}

