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
                var dataAccess = new DataAceess();
                var res = dataAccess.GetCustomer(0, 10, "server=localhost;database=sakila;uid=root");

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