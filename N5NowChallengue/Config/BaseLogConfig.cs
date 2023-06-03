using System;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Compact;

namespace N5NowChallengue.Config
{
    public static class BaseLogConfig
    {
        public static Logger BaseLogger(string name, IConfiguration configuration)
        {
            if (configuration.GetSection("Serilog").Exists())
            {
                return new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("name", name)
                    .CreateLogger();
            }

            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("name", name)
                .Enrich.WithCorrelationId()
                .WriteTo.File(new CompactJsonFormatter(),
                    "/LogN5NowChallengue/" + name + DateTime.Now.ToString("s").Replace(":", "-") + ".log")
                .WriteTo.Logger(lc => lc
                    .WriteTo.Console())
                .CreateLogger();
        }
    }
}