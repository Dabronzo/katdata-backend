
using katdata.Features.Entities.Population;
using katdata.Features.Entities.Utils;
using Microsoft.EntityFrameworkCore;

namespace katdata.Features.Entities.Urban
{
    [Owned]
    public sealed class Settlement
    {
        public required string Name { get; init; }

        public required Guid ProvinceId { get; init; }

        public required SettlementType Type { get; init; }

        public Province? Province { get; init; }

        public required SettlementPopulation Population { get; init; }

    }
}
