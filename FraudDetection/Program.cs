using FraudDetection.Application.UseCases;
using FraudDetection.Domain.Services;
using FraudDetection.Infrastructure.Parsing;

IDataNormalizer normalizer = new DataNormalizer();
IFraudDetector fraudDetector = new FraudDetector(normalizer);
var useCase = new DetectFraudulentOrdersUseCase(fraudDetector);

var orders = OrderParser.Parse(Console.In);
var fraudulentIds = useCase.Execute(orders);
var output = string.Join(",", fraudulentIds.Order());
Console.WriteLine(output);
