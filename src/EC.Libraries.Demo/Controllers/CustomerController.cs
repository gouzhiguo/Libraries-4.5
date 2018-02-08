using System.Web.Http;

namespace EC.Libraries.Demo.Controllers
{
    public class CustomerController : ApiController
    {
        // GET api/customer/5
        public string GetCusteomer(int id)
        {
            return "value";
        }
    }
}
