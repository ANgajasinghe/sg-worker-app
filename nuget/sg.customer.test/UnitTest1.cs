using System.Linq;
using Xunit;

namespace sg.customer.test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            try
            {
                var dataAccess = new DataAccess();
                var res = dataAccess.GetCustomer(100,
                    "Server=sg-mysqlserver.mysql.database.azure.com; Port=3306; Database=sakila; Uid=ag@sg-mysqlserver; Pwd=#Compaq12345; SslMode=Preferred;",
                    "mongodb://sgcustomer-prod-cosmosdb-mongo:pzPIq86j3S1gH0MqEKiDfqxcTjhkBAKWtuIKeG7Gy77wc4SMu1EcG1vwVRaHD5zKqDdAwU1vhGYcmUvIxkXWJg==@sgcustomer-prod-cosmosdb-mongo.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@sgcustomer-prod-cosmosdb-mongo@");

                Assert.NotNull(res);
                Assert.True(res.Any());
            }
            catch (System.Exception ex)
            {

                throw;
            }



        }
    }
}