using FraudDetection.Domain.Models;
using FraudDetection.Domain.Services;

namespace FraudDetection.Application.UseCases;

public sealed class DetectFraudulentOrdersUseCase
{
    private readonly IFraudDetector _fraudDetector;

    public DetectFraudulentOrdersUseCase(IFraudDetector fraudDetector)
    {
        ArgumentNullException.ThrowIfNull(fraudDetector);
        _fraudDetector = fraudDetector;
    }

    public IReadOnlySet<int> Execute(IEnumerable<Order> orders)
    {
        return _fraudDetector.DetectFraudulentOrders(orders);
    }
}
