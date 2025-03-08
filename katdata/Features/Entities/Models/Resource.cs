using System.ComponentModel.DataAnnotations;

namespace katdata.Features.Entities.Models
{
    public abstract record Resource
    {
        [Key]
        public Guid Id { get; init; }

        public abstract ResourceTypes Type { get; }

        public required ResourceName Name { get; init;}

        public string? Description { get; init; }

    }

    public abstract record RawResource : Resource
    {
        public required string SomeUniqueProp { get; init; } 

        public override ResourceTypes Type => ResourceTypes.Raw;
    }

    public abstract record ManufacturedResource : Resource
    {
        public override ResourceTypes Type => ResourceTypes.Manufactured;
    }

    public sealed record HighTechResource : Resource
    {
        public override ResourceTypes Type => ResourceTypes.HighTech;
    }

    public enum ResourceName
    {
        None = 0,
        Wood,
        Tools,
    }



}
