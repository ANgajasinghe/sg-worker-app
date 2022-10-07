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
        private readonly IConfiguration _configuration;

        public FuncGetCustomer(ILogger<FuncGetCustomer> log, IConfiguration configuration)
        {
            _logger = log;
            this._configuration = configuration;
        }

        [FunctionName("FuncGetCustomer")]
        [OpenApiOperation(operationId: "Run")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            try
            {


                _logger.LogInformation("C# HTTP trigger function processed a request.");

                DataAccess dataAceess = new DataAccess();
                var data = dataAceess.GetCustomer(50, _configuration.GetConnectionString("MYSQL_CONNECTION_STR"), _configuration.GetConnectionString("COSMOS_CONNECTION_STR"));
               
                return new OkObjectResult(new {});
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

