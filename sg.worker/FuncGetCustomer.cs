using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using sg.customer;

namespace sg.worker
{
    public class FuncGetCustomer
    {
        private readonly ILogger<FuncGetCustomer> _logger;
        private readonly IConfiguration configuration;

        public FuncGetCustomer(ILogger<FuncGetCustomer> log, IConfiguration configuration)
        {
            _logger = log;
            this.configuration = configuration;
        }

        [FunctionName("FuncGetCustomer")]
        [OpenApiOperation(operationId: "Run")]
        [OpenApiParameter(name: "lastId", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiParameter(name: "pageSize", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            try
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

                DataAceess dataAceess = new DataAceess();
                var data = dataAceess.GetCustomer(int.Parse(lastId), int.Parse(pageSize), configuration.GetConnectionString("default"));


                return new OkObjectResult(data);
            }
            catch (System.Exception ex)
            {

                return new BadRequestObjectResult(new 
                {
                    message = "intern sever error",
                    ex
                });
            }
        }
    }
}

