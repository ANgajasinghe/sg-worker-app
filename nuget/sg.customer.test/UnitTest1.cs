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
                    "", 
                    "");

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