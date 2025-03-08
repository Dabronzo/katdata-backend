using katdata.Features.Entities.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace katdata.Features.Entities
{
    public abstract class ProductionFacility
    {
        public Guid Id { get; init; }

        public required FacilityTypes Type { get; set; }

        public required string Name { get; init; }

        public required decimal Cash { get; set; }

        public required Guid ProvinceId { get; init; } // this needed to be linked to province

        public required Resource Product { get; init; }

        public required int WorkForce { get; set; }

        public required int Level { get; set; }

        public decimal Income { get; private set; } = 0;

        public decimal Expenses { get; private set; } = 0;

        public decimal NetProfit => Income - Expenses;

        private List<MonthlyProfitRecord> ProfitHistory { get; } = [];

        // Keep track of turns (months)
        private int CurrentTurn { get; set; } = 0;

        public ICollection<FacilityInventoryUnit> Supplies { get; private set; } = [];

        public ICollection<FacilityInventoryUnit> ProducedResorces { get; private set; } = [];

        protected void StartCash(decimal cash)
        {
            Cash = cash;
        }

        public abstract ICollection<FacilityInventoryUnit> MakeSupplyOrder();

        // Abstract method: Each facility must define how it produces resources
        public abstract void Produce();

        // Checks if facility has enough resources to operate
        protected bool CanProduce()
        {
            return Supplies.All(r => r.Quantity > 0);
        }

        public abstract void ConsumeResources();

        protected void StoreSupply(ICollection<FacilityInventoryUnit> supply)
        {
            Supplies = [.. supply];
        }

        protected void Pay(decimal amount)
        {
            if (Cash < amount)
            {
                Console.WriteLine($"{Name} cannot buy: Not enough cash!");
                return;
            }
            Cash = Cash - amount;
            Expenses += amount;
        }

        protected void Sell(decimal amount)
        {
            Cash += amount;
            Income += amount;
        }

        protected void PayWorkers(decimal wagePerWorker)
        {
            decimal totalWages = WorkForce * wagePerWorker;
            Pay(totalWages);
        }

        protected void StoreProducedProduct(ResourceName resource, long amount)
        {
            var existing = ProducedResorces.FirstOrDefault(r => r.Material == resource);
            if (existing != null)
            {
                existing.Quantity += amount;
            }
            else
            {
                ProducedResorces.Add(new FacilityInventoryUnit
                {
                    Material = resource,
                    Quantity = amount
                });
            }
        }

        public void CalculateProfit()
        {
            CurrentTurn++; // Increment month

            var record = new MonthlyProfitRecord
            {
                Month = CurrentTurn,
                Income = Income,
                Expenses = Expenses,
                NetProfit = NetProfit
            };

            ProfitHistory.Add(record);

            // Reset for next month
            Income = 0;
            Expenses = 0;
        }

        public IReadOnlyList<MonthlyProfitRecord> GetProfitHistory(int lastNMonths = 12)
        {
            return ProfitHistory.TakeLast(lastNMonths).ToList();
        }
    }
}
