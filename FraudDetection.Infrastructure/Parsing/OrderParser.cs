using FraudDetection.Domain.Models;

namespace FraudDetection.Infrastructure.Parsing;

public static class OrderParser
{
    public static List<Order> Parse(TextReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);

        var firstLine = reader.ReadLine();
        if (string.IsNullOrWhiteSpace(firstLine))
            return [];

        if (!int.TryParse(firstLine.Trim(), out var n) || n < 0)
            return [];

        var orders = new List<Order>(n);

        for (var i = 0; i < n; i++)
        {
            var line = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = line.Split(',');
            if (parts.Length < 8) continue;

            if (!int.TryParse(parts[0].Trim(), out var orderId) || !int.TryParse(parts[1].Trim(), out var dealId))
                continue;

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
