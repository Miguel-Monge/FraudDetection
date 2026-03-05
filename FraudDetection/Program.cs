using FraudDetection.Application.Ports;
using FraudDetection.Application.UseCases;
using FraudDetection.Domain.Services;
using FraudDetection.Infrastructure.Adapters;
using FraudDetection.Infrastructure.Parsing;

IDataNormalizer normalizer = new DataNormalizer();
IFraudDetector fraudDetector = new FraudDetector(normalizer);
IOrderReader orderReader = new OrderReader();
var useCase = new DetectFraudulentOrdersUseCase(fraudDetector, orderReader);

try
{
    var fraudulentIds = useCase.Execute(Console.In);
    var output = string.Join(",", fraudulentIds.Order());
    Console.WriteLine(output);
}
catch (OrderParseException ex)
{
    Console.Error.WriteLine($"Could not parse the input: {ex.Message}");
    Console.Error.WriteLine("Please check the format and try again.");
    Environment.Exit(1);
}
