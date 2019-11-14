using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpFunction
{
    public class LogTester
    {
        private readonly TelemetryClient _logger;

        private static readonly HttpClient Http = new HttpClient();

        /// <summary>
        /// Initializes a new instance of the <see cref="LogTester"/> class.
        /// </summary>
        public LogTester(TelemetryClient logger)
        {
            _logger = logger;
        }

        [FunctionName("LogTester")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            if (req.Query.ContainsKey("number") == false)
            {
                return new BadRequestResult();
            }

            int number = Convert.ToInt32(req.Query["number"]);

            if (number % 2 != 0)
            {
                _logger.TrackTrace("Number is not even", SeverityLevel.Error,
                    new Dictionary<string, string>() { { "number", number.ToString() } });

                _logger.TrackException(new InvalidOperationException("Number not even"));

                return new OkObjectResult("number not even");
            }
            else
            {
                await Http.GetAsync("http://google.com");

                var trace = new TraceTelemetry("number is even", SeverityLevel.Information);

                if (Activity.Current != null)
                {
                    trace.Context.Operation.Id = Activity.Current.RootId;
                    trace.Context.Operation.ParentId = Activity.Current.ParentId;
                }

                _logger.TrackTrace(trace);

                return new OkObjectResult("number even");
            }
        }
    }
}
