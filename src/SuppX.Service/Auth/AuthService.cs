using System.Collections.Concurrent;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using SuppX.Domain;
using SuppX.Storage;

namespace SuppX.Service;

public class AuthService(IUserRepository repository) : IAuthService
{
    public async Task<string?> LoginUserAsync(string login, string password, CancellationToken cancellationToken = default)
    {
        User? user = await repository.GetByLoginAsync(login, cancellationToken);
        if (user is null)
        {
            return null;
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
        if (!isPasswordValid)
        {
            return null;
        }

        var claims = new List<Claim>
        {
            new("id", user.Id.ToString()),
            new("roleId", user.RoleId.ToString())
        };

        byte[] secret = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET") ?? "secretKeySecretKeySecretKey!!!");
        var key = new SymmetricSecurityKey(secret);

        var jwtToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.Add(TimeSpan.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRES") ?? "00:10:00")),
            claims: claims,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}
