using Confluent.Kafka;
using JefferiesPrototype.Models;
using System;
using System.Threading.Tasks;

namespace JefferiesPrototype.Services
{
    public class KafkaProducerService
    {
        private readonly IProducer<string, string> _producer;

        public KafkaProducerService(ProducerConfig config)
        {
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task ProduceMessageAsync(MessageModel message)
        {
            var key = $"{message.JMSCorrelationID}-{message.Timestamp}";
            var value = message.Payload;

            var kafkaMessage = new Message<string, string>
            {
                Key = key,
                Value = value
            };

            await _producer.ProduceAsync("your-kafka-topic", kafkaMessage);
        }
    }
}
