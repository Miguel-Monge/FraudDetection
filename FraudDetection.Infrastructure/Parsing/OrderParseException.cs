namespace FraudDetection.Infrastructure.Parsing;

public sealed class OrderParseException : Exception
{
    public OrderParseException(string message) : base(message) { }
}
