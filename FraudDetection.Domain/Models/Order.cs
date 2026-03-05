namespace FraudDetection.Domain.Models;

public sealed record Order(
    int OrderId,
    int DealId,
    string Email,
    string Street,
    string City,
    string State,
    string Zip,
    string CreditCard);
