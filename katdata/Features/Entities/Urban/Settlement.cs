
using katdata.Features.Entities.Population;

namespace katdata.Features.Entities.Urban
{
    public sealed class Settlement
    {
        public required Guid Id { get; init; }

        public required Guid ProvinceId { get; init; }

        public required int TotalPopulation {  get; set; }

        public required SettlementType Type { get; init; }

        public required Children Children { get; init; }

        public required AdultStudents AdultStudens { get; init; }

        public required AdultsWorking Workforce { get; init; }

        public required Retired Retired { get; init; }


    }



    public enum SettlementType
    {
        None = 0,
        City,
        Village,
        Rural,
    }
}
