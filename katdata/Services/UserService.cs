using katdata.Features.Models;
using katdata.Tools;
using Microsoft.AspNetCore.Identity;

namespace katdata.Services;

public sealed class UserService(
    Repository<User, Guid> userRepo,
    IPasswordHasher<User> passwordHasher)
{
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    private readonly Repository<User, Guid> _repository = userRepo;

    public async Task<User> RegisterUserAsync(string email, string plainPassword, UserRoles role)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            HashedPassword = _passwordHasher.HashPassword(null, plainPassword),
            CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
            Role = role
        };

        await _repository.SaveAsync(user);
        return user;
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        var user = await _repository.GetByIdAsync(id);
        return user;
    }

    public bool ValidatePassword(User user, string plainPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, plainPassword);
        return result == PasswordVerificationResult.Success;
    }

}
