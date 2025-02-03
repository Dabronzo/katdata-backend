using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using katdata.Features.Models;
using katdata.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace katdata.Services;

public sealed class UserService(
    Repository<User, Guid> userRepo,
    IPasswordHasher<User> passwordHasher,
    IConfiguration config)
{
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    private readonly Repository<User, Guid> _repository = userRepo;
    private readonly IConfiguration _configuration = config;


    public async Task<string?> Authenticate(string email, string password)
    {
        var user = (await _repository.GetAllAsync()).FirstOrDefault(u => u.Email == email);
        if (user is null)
        {
            return null;
        }
        var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password);
        if (result != PasswordVerificationResult.Success)
            return null; // Password mismatch

        return GenerateJwt(user);

    }

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

    private string GenerateJwt(User user)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}
