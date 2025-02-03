using System.ComponentModel.DataAnnotations;

namespace katdata.Features.Models;

public sealed record User
{
    [Key]
    public required Guid Id { get; set; }

    public required string Email { get; init; }

    public required string HashedPassword { get; init; }

    public required DateOnly CreatedAt { get; init; }

    public required UserRoles Role {  get; init; }
}


public enum UserRoles
{
    Admin = 0,
    Manager,
    Guest,
}
