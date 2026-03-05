namespace FraudDetection.Domain.Services;

public interface IDataNormalizer
{
    string NormalizeEmail(string email);
    string NormalizeAddress(string street, string city, string state, string zip);
}
