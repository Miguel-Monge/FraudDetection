using FraudDetection.Application.Ports;
using FraudDetection.Domain.Models;
using FraudDetection.Infrastructure.Parsing;

namespace FraudDetection.Infrastructure.Adapters;

public sealed class OrderReader : IOrderReader
{
    public IReadOnlyList<Order> Read(TextReader reader)
    {
        return OrderParser.Parse(reader);
    }
}
