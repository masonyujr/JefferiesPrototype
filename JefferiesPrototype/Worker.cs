using JefferiesPrototype.Services;
using Microsoft.Extensions.Hosting;
using JefferiesPrototype.Services;
using System.Threading;
using System.Threading.Tasks;

public class Worker : BackgroundService
{
    private readonly MQReaderService _mqReaderService;
    private readonly AvroSchemaValidator _avroSchemaValidator;
    private readonly KafkaProducerService _kafkaProducerService;

    public Worker(MQReaderService mqReaderService, AvroSchemaValidator avroSchemaValidator, KafkaProducerService kafkaProducerService)
    {
        _mqReaderService = mqReaderService;
        _avroSchemaValidator = avroSchemaValidator;
        _kafkaProducerService = kafkaProducerService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var message = _mqReaderService.ReadMessage();
            if (await _avroSchemaValidator.ValidateMessageAsync(message))
            {
                await _kafkaProducerService.ProduceMessageAsync(message);
            }
            await Task.Delay(1000, stoppingToken); // Adjust delay as necessary
        }
    }
}
