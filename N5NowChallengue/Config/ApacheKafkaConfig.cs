using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace N5NowChallengue.Config
{
    public static class ApacheKafkaConfig
    {
        public static IServiceCollection AddApacheKafka(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.Get<AppSettings>();
            services.AddSingleton<ProducerConfig>(provider => new ProducerConfig
            {
                BootstrapServers = appSettings.Settings.KafkaConnection, 
                ClientId = "n5now-producer",
            });

            services.AddSingleton<ConsumerConfig>(provider => new ConsumerConfig
            {
                BootstrapServers = appSettings.Settings.KafkaConnection, 
                GroupId = "n5now-consumer-group",
            });
            services.AddSingleton<IKafkaProducer, KafkaProducer>();
            return services;
        }

    }
}