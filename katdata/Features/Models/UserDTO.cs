namespace katdata.Features.Models;

public sealed record UserDTO
{
    public required Guid Id { get; init; }

    public required string Email { get; init; }

    public required UserRoles Role { get; init; }

}
