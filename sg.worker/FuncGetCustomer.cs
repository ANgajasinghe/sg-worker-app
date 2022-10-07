using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace sg.worker
{
    public class FuncGetCustomer
    {
        private readonly ILogger<FuncGetCustomer> _logger;

        public FuncGetCustomer(ILogger<FuncGetCustomer> log)
        {
            _logger = log;
        }

        [FunctionName("FuncGetCustomer")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "lastId", "pageSize" })]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string lastId = req.Query["lastId"];
            string pageSize = req.Query["pageSize"];

            if (lastId is null || pageSize is null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Cannot find lastId or pageSize"
                });
            }

            string responseMessage = "";

            return new OkObjectResult(responseMessage);
        }
    }
}

