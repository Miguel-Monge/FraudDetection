using System.Text;

namespace FraudDetection.Domain.Services;

public sealed class DataNormalizer : IDataNormalizer
{
    private static readonly Dictionary<string, string> StateMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["IL"] = "illinois",
        ["Illinois"] = "illinois",
        ["CA"] = "california",
        ["California"] = "california",
        ["NY"] = "newyork",
        ["New York"] = "newyork"
    };

    public string NormalizeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return string.Empty;

        var parts = email.Split('@');
        if (parts.Length != 2) return email.ToLowerInvariant();

        var local = parts[0];
        var domain = parts[1].ToLowerInvariant();

        var plusIndex = local.IndexOf('+');
        if (plusIndex >= 0)
            local = local[..plusIndex];

        local = local.Replace(".", "", StringComparison.Ordinal);
        local = local.ToLowerInvariant();

        return $"{local}@{domain}";
    }

    public string NormalizeAddress(string street, string city, string state, string zip)
    {
        var sb = new StringBuilder(128);
        sb.Append(NormalizeStreet((street ?? "").ToLowerInvariant()));
        sb.Append('|');
        sb.Append((city ?? "").ToLowerInvariant());
        sb.Append('|');
        sb.Append(NormalizeState(state ?? ""));
        sb.Append('|');
        sb.Append((zip ?? "").ToLowerInvariant());
        return sb.ToString();
    }

    private static string NormalizeStreet(string street)
    {
        street = street.Trim();
        var suffixes = new[] { " street", " st.", " st", " road", " rd.", " rd" };
        foreach (var suffix in suffixes)
        {
            if (street.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
                street = street[..^suffix.Length].Trim();
        }
        return street;
    }

    private static string NormalizeState(string state)
    {
        var trimmed = (state ?? "").Trim();
        return StateMap.TryGetValue(trimmed, out var normalized)
            ? normalized
            : trimmed.ToLowerInvariant();
    }
}
