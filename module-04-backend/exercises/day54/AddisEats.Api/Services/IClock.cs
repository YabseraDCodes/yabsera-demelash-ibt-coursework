namespace AddisEats.Api.Services;

public interface IClock
{
    DateTime UtcNow { get; }
}

public sealed class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}

public sealed class CapturedClock : IClock
{
    public DateTime UtcNow { get; } = DateTime.UtcNow;
}
