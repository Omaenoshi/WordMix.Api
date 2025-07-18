namespace WordMix.Domain.Options;

using System;

public class JwtSettings
{
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public TimeSpan TokenLifetime { get; set; }
}