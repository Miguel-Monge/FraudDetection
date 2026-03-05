using FraudDetection.Domain.Models;
using FraudDetection.Domain.Services;

namespace FraudDetection.Application.UseCases;

public sealed class DetectFraudulentOrdersUseCase
{
    private readonly IFraudDetector _fraudDetector;

    public DetectFraudulentOrdersUseCase(IFraudDetector fraudDetector)
    {
        _fraudDetector = fraudDetector ?? throw new ArgumentNullException(nameof(fraudDetector));
    }

    public IReadOnlySet<int> Execute(IEnumerable<Order> orders)
    {
        return _fraudDetector.DetectFraudulentOrders(orders);
    }
}
