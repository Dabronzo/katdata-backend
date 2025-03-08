using katdata.Features.Entities.Models;

namespace katdata.Features.Entities.Production.Extraction;

public sealed class WoodCutCamp : ProductionFacility
{
    public required Guid ResourceSourceId { get; init; }

    public required decimal RequiredStartCash { get; init; } = 100000; // need tunning

    public override ICollection<FacilityInventoryUnit> MakeSupplyOrder()
    {
        var required = new FacilityInventoryUnit { Material = ResourceName.Tools, Quantity = 100000 };
        return [required];
    }

    public override void Produce()
    {
        if (!CanProduce())
        {
            Console.WriteLine($"{Name} cannot produce: Not enough required resources!");
            return;
        }

        // need implement logic for the production calculations
        // and store it using the parent function

    }

    public override void ConsumeResources()
    {
        throw new NotImplementedException();
    }

}
