namespace AddisEats.Api.Services;

public interface IPaymentProvider
{
    string Name { get; }
}

public sealed class TeleBirrProvider : IPaymentProvider
{
    public string Name => "TeleBirr";
}

public sealed class CbeBirrProvider : IPaymentProvider
{
    public string Name => "CBE Birr";
}
