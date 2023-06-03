using Confluent.Kafka;
using N5NowChallengue.BusinessService.DTO;
using N5NowChallengue.ErrorHandler;
using Newtonsoft.Json;
using Serilog;

namespace N5NowChallengue.Config
{
    public interface IKafkaProducer
    {
        public bool ProduceMessage(string topic, string message);
        public void ProduceObject(string topic, KafkaDto kafkaDto);

    }
    public class KafkaProducer: IKafkaProducer
    {
        private readonly ProducerConfig _producerConfig;

        public KafkaProducer(ProducerConfig producerConfig)
        {
            _producerConfig = producerConfig;
        }
        
        public bool ProduceMessage(string topic, string message)
        {
            using var producer = new ProducerBuilder<Null, string>(_producerConfig).Build();

            var deliveryReport = producer.ProduceAsync(topic, new Message<Null, string>
            {
                Value = message
            }).GetAwaiter().GetResult();

            if (deliveryReport.Status == PersistenceStatus.Persisted)
            {
                Log.Information("The message was successfully persisted");
                return true;
            }
            Log.Error("There was an error persisting the message");
            throw new BaseException("There was an error persisting the message");
        }
        
        public void ProduceObject(string topic, KafkaDto kafkaDto)
        {
            using var producer = new ProducerBuilder<Null, string>(_producerConfig).Build();

            var serializedObject = JsonConvert.SerializeObject(kafkaDto);

            var deliveryReport = producer.ProduceAsync(topic, new Message<Null, string>
            {
                Value = serializedObject
            }).GetAwaiter().GetResult();

            if (deliveryReport.Status == PersistenceStatus.Persisted)
            {
                Log.Information($"The kafkaDto with Id {kafkaDto.Id} was successfully persisted");    
            }
            else
            {
                Log.Error($"There was an error persisting the KafkaDto with Id {kafkaDto.Id}");
                throw new BaseException($"There was an error persisting the KafkaDto with Id {kafkaDto.Id}");
            }
        }
    }
}