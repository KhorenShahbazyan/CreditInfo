using Microsoft.AspNetCore.Mvc;

namespace KhorenTest.CreditInfo.Web.Api.Controllers
{
    public class MonitoringController : ControllerBase
    {
        [HttpGet("Ping")]
        public ActionResult Ping()
        {
            return Ok("Khoren Test CreditInfo API is UP!");
        }
    }
}
