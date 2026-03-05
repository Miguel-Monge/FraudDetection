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
        if (string.IsNullOrWhiteSpace(email)) return string.Empty;

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
        sb.Append(NormalizeStreet(street.ToLowerInvariant()));
        sb.Append('|');
        sb.Append(city.ToLowerInvariant());
        sb.Append('|');
        sb.Append(NormalizeState(state));
        sb.Append('|');
        sb.Append(zip.ToLowerInvariant());
        return sb.ToString();
    }

    private static string NormalizeStreet(string street)
    {
        street = street.Replace(" street", "", StringComparison.Ordinal);
        street = street.Replace(" st.", "", StringComparison.Ordinal);
        street = street.Replace(" st", "", StringComparison.Ordinal);
        street = street.Replace(" road", "", StringComparison.Ordinal);
        street = street.Replace(" rd.", "", StringComparison.Ordinal);
        street = street.Replace(" rd", "", StringComparison.Ordinal);
        return street.Trim();
    }

    private static string NormalizeState(string state)
    {
        return StateMap.TryGetValue(state.Trim(), out var normalized)
            ? normalized
            : state.ToLowerInvariant();
    }
}
