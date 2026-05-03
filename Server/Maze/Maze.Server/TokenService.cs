using Maze.Common.Types;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Maze.Server;

public class TokenService
{
    private readonly SigningCredentials _credentials;
    private readonly TokenValidationParameters _validationParams;

    public TokenService(IConfiguration config)
    {
        var secret = config["Jwt:SecretKey"] ?? throw new InvalidOperationException("Jwt:SecretKey not configured");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        _credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        _validationParams = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    }

    public (string token, PlayerId playerId) Issue()
    {
        var playerId = PlayerId.New();
        var handler = new JsonWebTokenHandler();
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([new Claim(JwtRegisteredClaimNames.Sub, playerId.ToString())]),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = _credentials
        };
        return (handler.CreateToken(descriptor), playerId);
    }

    public async Task<PlayerId> ValidateAsync(string token)
    {
        var handler = new JsonWebTokenHandler();
        var result = await handler.ValidateTokenAsync(token, _validationParams);
        if (!result.IsValid)
            throw new UnauthorizedAccessException($"Invalid token: {result.Exception?.Message}");
        var sub = result.ClaimsIdentity.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        return PlayerId.From(sub ?? string.Empty);
    }
}
