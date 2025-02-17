using ISTUDIO.BankPaymentStatusCheckerService.Banks.BakaiBank;
using ISTUDIO.BankPaymentStatusCheckerService.Interfaces;
using ISTUDIO.BankPaymentStatusCheckerService.Banks.OptimaBank;

namespace ISTUDIO.BankPaymentStatusCheckerService.Factory;

public class BankPaymentCheckerFactory
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public BankPaymentCheckerFactory(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public IPaymentStatusChecker CreateChecker(string bankName)
    {
        var scope = _serviceScopeFactory.CreateScope();
        var provider = scope.ServiceProvider;

        return bankName switch
        {
            "bakai" => provider.GetRequiredService<BakaiPaymentStatusChecker>(),
            //_ => throw new ArgumentException($"Неизвестный банк: {bankName}")
        };
    }

}
