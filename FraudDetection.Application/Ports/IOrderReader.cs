using FraudDetection.Domain.Models;

namespace FraudDetection.Application.Ports;

public interface IOrderReader
{
    IReadOnlyList<Order> Read(TextReader reader);
}
