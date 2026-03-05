using FraudDetection.Application.Ports;
using FraudDetection.Domain.Services;

namespace FraudDetection.Application.UseCases;

public sealed class DetectFraudulentOrdersUseCase
{
    private readonly IFraudDetector _fraudDetector;
    private readonly IOrderReader _orderReader;

    public DetectFraudulentOrdersUseCase(IFraudDetector fraudDetector, IOrderReader orderReader)
    {
        ArgumentNullException.ThrowIfNull(fraudDetector);
        ArgumentNullException.ThrowIfNull(orderReader);
        _fraudDetector = fraudDetector;
        _orderReader = orderReader;
    }

    public IReadOnlySet<int> Execute(TextReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        var orders = _orderReader.Read(reader);
        return _fraudDetector.DetectFraudulentOrders(orders);
    }
}
