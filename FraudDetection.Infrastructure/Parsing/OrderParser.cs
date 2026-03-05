using FraudDetection.Domain.Models;

namespace FraudDetection.Infrastructure.Parsing;

public static class OrderParser
{
    public static List<Order> Parse(TextReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);

        var firstLine = reader.ReadLine();
        if (string.IsNullOrWhiteSpace(firstLine))
            throw new OrderParseException("The first line must contain the number of records.");

        if (!int.TryParse(firstLine.Trim(), out var n))
            throw new OrderParseException($"The first line must be a number. Got: '{firstLine.Trim()}'");

        if (n < 0)
            throw new OrderParseException($"The number of records must be zero or positive. Got: {n}");

        var orders = new List<Order>(n);

        for (var i = 0; i < n; i++)
        {
            var lineNumber = i + 2;
            var line = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
                throw new OrderParseException($"Line {lineNumber}: expected a record, got an empty line.");

            var parts = line.Split(',');
            if (parts.Length < 8)
                throw new OrderParseException($"Line {lineNumber}: expected 8 comma-separated fields (order id, deal id, email, street, city, state, zip, credit card). Got {parts.Length}.");

            if (!int.TryParse(parts[0].Trim(), out var orderId))
                throw new OrderParseException($"Line {lineNumber}: order ID must be a number. Got: '{parts[0].Trim()}'");

            if (!int.TryParse(parts[1].Trim(), out var dealId))
                throw new OrderParseException($"Line {lineNumber}: deal ID must be a number. Got: '{parts[1].Trim()}'");

            orders.Add(new Order(
                orderId,
                dealId,
                parts[2].Trim(),
                parts[3].Trim(),
                parts[4].Trim(),
                parts[5].Trim(),
                parts[6].Trim(),
                parts[7].Trim()));
        }

        return orders;
    }
}
