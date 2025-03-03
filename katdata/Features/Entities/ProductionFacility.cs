using katdata.Features.Entities.Models;

namespace katdata.Features.Entities
{
    public abstract class ProductionFacility
    {
        public Guid Id { get; init; }

        public required FacilityTypes Type { get; set; }

        public required string Name { get; init; }

        public required Guid CountryId { get; init; } // this needed to be linked to country

        public required Resource Product { get; init; }

        public required decimal ProductionRate { get; init; }

        public required int WorkForce { get; init; }

        public required int Level { get; init; }

        public ICollection<FacilityInventoryUnit> RequiredResources { get; private set; } = [];

        public ICollection<FacilityInventoryUnit> ProducedResorces { get; private set; } = [];

        // Abstract method: Each facility must define how it produces resources
        public abstract void Produce();

        // Checks if facility has enough resources to operate
        protected bool CanProduce()
        {
            return RequiredResources.All(r => r.Quantity > 0);
        }

        protected void ConsumeRequiredResources()
        {
            foreach (var resource in RequiredResources)
            {
                resource.Quantity -= 1;
            }
        }
    }

    public sealed class Mine : ProductionFacility
    {
        public required ResourceLocation ResourceSource { get; init; }

        public override void Produce()
        {
            if (!CanProduce())
            {
                Console.WriteLine($"{Name} cannot produce: Not enough required resources!");
                return;
            }

            if (ResourceSource.TotalAmount > 0)
            {
                var produced = ProducedResorces;
            }

        }
    }

    public sealed record FacilityInventoryUnit
    {
        public required Resource Material { get; init; }

        public required long Quantity { get; set; }
    }



}
