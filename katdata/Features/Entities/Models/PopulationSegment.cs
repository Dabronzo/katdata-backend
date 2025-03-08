namespace katdata.Features.Entities.Models
{
    public sealed record PopulationSegment
    {
        public required Guid Id { get; init; }

        public required PopulationSegmentType Type { get; init; }

        public required long TotalPopulation { get; init; }

        public required long EmployedPopulation { get; init; }

        public required decimal AverageIncome { get; init; }

        public long UnemployedPopulation => TotalPopulation - EmployedPopulation;

        public decimal UnemployedRate => TotalPopulation > 0
            ? UnemployedPopulation / TotalPopulation
            : 0m;
    }

    public enum PopulationSegmentType
    {
        None =0,
        Low,
        Middle,
        High,
    }
}
