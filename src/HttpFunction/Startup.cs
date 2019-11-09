using System;
using System.Collections.Generic;
using System.Text;
using HttpFunction;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;


[assembly: WebJobsStartup(typeof(Startup))]

namespace HttpFunction
{
    class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddSingleton<ITelemetryInitializer, TelemetryInitializer>();
        }
    }
}
