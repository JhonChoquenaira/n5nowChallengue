using System;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Nest;
using Microsoft.Extensions.DependencyInjection;

namespace N5NowChallengue.Config
{
    public static class ElasticSearchConfig
    {
        public static IServiceCollection AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.Get<AppSettings>();
            services.AddSingleton<IElasticClient>(provider =>
            {
                var connectionSettings = new ConnectionSettings(new Uri(appSettings.Settings.ElasticSearchConnection))
                    .DefaultIndex(appSettings.Settings.ElasticSearchDefaultIndex);

                return new ElasticClient(connectionSettings);
            });
            return services;
        }
    }
}