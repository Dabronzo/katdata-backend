namespace katdata.Features.Entities
{
    public sealed class Province
    {
        public required Guid ProvinceId { get; init; }

        public required Guid CountryId { get; init; } // This need to be a foreign key to Country

        public required string ProvinceName { get; init; }

        public required ICollection<ResourceLocation> Resources { get; init; }

        public required ICollection<ProductionFacility> ProductionFacility { get; init; } = [];

        public void AddResourceLocation(ResourceLocation resourceLocation) => Resources.Add(resourceLocation);

    }
}
