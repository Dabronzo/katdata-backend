
using katdata.Features.Entities.Models;

namespace katdata.Features.Entities
{
    public sealed class ResourceLocation(IEnumerable<(ResourceTypes Type, long TotalAmount)> resources)
    {
        public required Guid Id { get; init; }

        public required Guid ProvinceId { get; init; } // This need to be foreign key to Province

        public ICollection<ProductionFacility> Facilities { get; set; } = [];

        public required ICollection<ResourceLocationInventory> Inventory { get; init; } = resources
           .Select(r => new ResourceLocationInventory(r.Type, r.TotalAmount))
           .ToList();
    }

    public sealed record ResourceLocationInventory(ResourceTypes Type, long TotalAmount);
}
