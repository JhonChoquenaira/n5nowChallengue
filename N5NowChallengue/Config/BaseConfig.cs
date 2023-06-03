using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace N5NowChallengue.Config
{
    public static class BaseConfig
    {
        public static void UseBasicBuilder(
            this IApplicationBuilder app)
        {
            var appConfiguration = app.ApplicationServices.GetRequiredService<IConfiguration>();
            Log.Logger = BaseLogConfig.BaseLogger("N5now-Challengue", appConfiguration);
        }
    }
}