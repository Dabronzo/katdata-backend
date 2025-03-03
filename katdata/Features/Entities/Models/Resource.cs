using System.ComponentModel.DataAnnotations;

namespace katdata.Features.Entities.Models
{
    public abstract record Resource
    {
        [Key]
        public Guid Id { get; init; }

        public abstract ResourceTypes Type { get; }

        public required string Name { get; init;}

        public required string Description { get; init; }

    }

    public sealed record RawResource : Resource
    {
        public required string SomeUniqueProp { get; init; } 

        public override ResourceTypes Type => ResourceTypes.Raw;
    }

    public sealed record ManufacturedResource : Resource
    {
        public override ResourceTypes Type => ResourceTypes.Manufactured;
    }

    public sealed record HighTechResource : Resource
    {
        public override ResourceTypes Type => ResourceTypes.HighTech;
    }
}
