
using katdata.Features.Entities.Models;

namespace katdata.Features.Entities
{
    public sealed class ResourceLocation(IEnumerable<(Resource Resource, long TotalAmount)> resources)
    {
        public required Guid Id { get; init; }

        public required Guid ProvinceId { get; init; } // This need to be foreign key to Province

        public required ICollection<ResourceLocationInventory> Resources { get; init; } = resources
           .Select(r => new ResourceLocationInventory(r.Resource, r.TotalAmount))
           .ToList();
    }

    public sealed record ResourceLocationInventory(Resource Resource, long TotalAmount);
}
