using FraudDetection.Domain.Models;

namespace FraudDetection.Domain.Services;

public sealed class FraudDetector : IFraudDetector
{
    private readonly IDataNormalizer _normalizer;

    public FraudDetector(IDataNormalizer normalizer)
    {
        ArgumentNullException.ThrowIfNull(normalizer);
        _normalizer = normalizer;
    }

    public IReadOnlySet<int> DetectFraudulentOrders(IEnumerable<Order> orders)
    {
        ArgumentNullException.ThrowIfNull(orders);
        var orderList = orders as IReadOnlyList<Order> ?? orders.ToArray();
        var fraudulentIds = new HashSet<int>();

        var emailGroups = new Dictionary<(int DealId, string NormalizedEmail), List<Order>>();
        var addressGroups = new Dictionary<(int DealId, string NormalizedAddress), List<Order>>();

        foreach (var order in orderList)
        {
            var normEmail = _normalizer.NormalizeEmail(order.Email);
            var normAddress = _normalizer.NormalizeAddress(order.Street, order.City, order.State, order.Zip);

            var emailKey = (order.DealId, normEmail);
            if (!emailGroups.TryGetValue(emailKey, out var emailList))
            {
                emailList = [];
                emailGroups[emailKey] = emailList;
            }
            emailList.Add(order);

            var addressKey = (order.DealId, normAddress);
            if (!addressGroups.TryGetValue(addressKey, out var addressList))
            {
                addressList = [];
                addressGroups[addressKey] = addressList;
            }
            addressList.Add(order);
        }

        foreach (var group in emailGroups.Values)
        {
            if (HasMultipleDistinctCards(group))
            {
                foreach (var o in group)
                    fraudulentIds.Add(o.OrderId);
            }
        }

        foreach (var group in addressGroups.Values)
        {
            if (HasMultipleDistinctCards(group))
            {
                foreach (var o in group)
                    fraudulentIds.Add(o.OrderId);
            }
        }

        return fraudulentIds;
    }

    private static bool HasMultipleDistinctCards(List<Order> orders)
    {
        if (orders.Count < 2) return false;
        var cards = new HashSet<string>(StringComparer.Ordinal);
        foreach (var order in orders)
        {
            cards.Add(order.CreditCard);
            if (cards.Count > 1) return true;
        }
        return false;
    }
}
