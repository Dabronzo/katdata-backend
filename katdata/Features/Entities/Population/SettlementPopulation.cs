using katdata.Features.Entities.Urban;
using Microsoft.EntityFrameworkCore;

namespace katdata.Features.Entities.Population;


[Owned]
public sealed class SettlementPopulation
{
    public required Guid Id { get; init; }

    public required NewBorn NewBornPopulation { get; set; }

    public required Children ChildrenPopulation { get; set; }

    public required YoungAdults YoungAdults { get; set; }

    public required WorkingAdults WorkingAdults { get; set; }

    public required Retired Retired { get; set; }

}
