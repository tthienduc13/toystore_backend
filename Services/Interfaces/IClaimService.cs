namespace Services.Interfaces;

public interface IClaimService
{
    public Guid GetCurrentUserId { get; }
}