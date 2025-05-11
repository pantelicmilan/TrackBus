using Domain.Primitives;
using System.Security.Cryptography;

namespace Domain.RefreshToken;

public class RefreshToken : AggregateRoot
{
    private RefreshToken() { }
    private static readonly double ExpirationDays = 7;
    private RefreshToken(
        string token, 
        DateTime expiration, 
        ConsumerIdentity consumerIdentity,
        string userAgent
    )
    {
        Token = token;
        Expires = expiration;
        ConsumerIdentity = consumerIdentity;
        UserAgent = userAgent;
    }
    public bool IsActive => !IsExpired && RevokedAt == null;

    public string Token { get; private set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public DateTime Expires { get; private set; }
    public ConsumerIdentity ConsumerIdentity { get; private set; }
    public string UserAgent { get; private set; }
    public DateTime? RevokedAt { get; private set; } = null;

    public static RefreshToken CreateRefreshToken(ConsumerIdentity consumerIdentity, string userAgent, string generatedRefreshToken)
    {
        return new RefreshToken(
            generatedRefreshToken, 
            DateTime.UtcNow.AddDays(ExpirationDays), 
            consumerIdentity,
            userAgent
        );
    }
    
    public void Revoke()
    {
        RevokedAt = DateTime.UtcNow;
    }
}
