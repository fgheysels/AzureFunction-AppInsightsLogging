using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HttpFunction
{
    public static class LogTester
    {
        private static readonly TelemetryClient Logger
            = new TelemetryClient(new TelemetryConfiguration(Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY")));

        [FunctionName("LogTester")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            if (req.Query.ContainsKey("number") == false)
            {
                return new BadRequestResult();
            }

            int number = Convert.ToInt32(req.Query["number"]);

            if (number % 2 != 0)
            {
                Logger.TrackTrace("Number is not even", SeverityLevel.Error,
                    new Dictionary<string, string>() { { "number", number.ToString() } });
            }

            return new OkResult();
        }
    }
}
