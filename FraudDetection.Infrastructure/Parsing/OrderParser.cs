using FraudDetection.Domain.Models;

namespace FraudDetection.Infrastructure.Parsing;

public static class OrderParser
{
    public static List<Order> Parse(TextReader reader)
    {
        var firstLine = reader.ReadLine();
        if (string.IsNullOrWhiteSpace(firstLine))
            return [];

        var n = int.Parse(firstLine.Trim());
        var orders = new List<Order>(n);

        for (var i = 0; i < n; i++)
        {
            var line = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = line.Split(',');
            if (parts.Length < 8) continue;

            orders.Add(new Order(
                int.Parse(parts[0].Trim()),
                int.Parse(parts[1].Trim()),
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
