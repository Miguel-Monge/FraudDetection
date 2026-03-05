using FraudDetection.Application.UseCases;
using FraudDetection.Domain.Services;
using FraudDetection.Infrastructure.Parsing;

IDataNormalizer normalizer = new DataNormalizer();
IFraudDetector fraudDetector = new FraudDetector(normalizer);
var useCase = new DetectFraudulentOrdersUseCase(fraudDetector);

try
{
    var orders = OrderParser.Parse(Console.In);
    var fraudulentIds = useCase.Execute(orders);
    var output = string.Join(",", fraudulentIds.Order());
    Console.WriteLine(output);
}
catch (OrderParseException ex)
{
    Console.Error.WriteLine($"Could not parse the input: {ex.Message}");
    Console.Error.WriteLine("Please check the format and try again.");
    Environment.Exit(1);
}
