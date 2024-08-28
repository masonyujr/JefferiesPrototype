// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;
using JefferiesPrototype.Services;
using Confluent.SchemaRegistry;
using Confluent.Kafka;
using JefferiesPrototype.Services;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<MQReaderService>();
        services.AddSingleton<AvroSchemaValidator>();
        services.AddSingleton<KafkaProducerService>();
        services.AddHostedService<Worker>();

        // Kafka Producer Configuration
        var producerConfig = new ProducerConfig { BootstrapServers = "your-kafka-broker-endpoint" };
        services.AddSingleton(producerConfig);

        // Schema Registry Configuration
        var schemaRegistryConfig = new SchemaRegistryConfig { Url = "your-schema-registry-url" };
        services.AddSingleton<ISchemaRegistryClient>(new CachedSchemaRegistryClient(schemaRegistryConfig));
    })
    .UseWindowsService() // Converts the app to a Windows Service
    .Build()
    .Run();
