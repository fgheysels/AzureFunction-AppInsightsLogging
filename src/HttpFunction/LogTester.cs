using System;
using System.IO;
using System.Threading.Tasks;
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

        [FunctionName("LogTester")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            if (req.Query.ContainsKey("number") == false)
            {
                return new BadRequestResult();
            }

            int number = Convert.ToInt32(req.Query["number"]);

            return new OkResult();
        }
    }
}
