using FraudDetection.Domain.Models;

namespace FraudDetection.Domain.Services;

public interface IFraudDetector
{
    IReadOnlySet<int> DetectFraudulentOrders(IEnumerable<Order> orders);
}
