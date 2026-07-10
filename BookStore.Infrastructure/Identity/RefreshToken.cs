namespace BookStore.Infrastructure.Identity;

public class RefreshToken()
{
    public string Token { get; set; } = string.Empty;
    public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;
    public DateTime ExpireOn { get; set; }
    public DateTime? RevokedOn { get; private set; }

    public bool IsExpired => DateTime.UtcNow >= ExpireOn;
    public bool IsActive => !IsExpired;
    public void Revoke() => RevokedOn = DateTime.UtcNow;
}
