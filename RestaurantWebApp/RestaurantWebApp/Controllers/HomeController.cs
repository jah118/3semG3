using System.Web.Mvc;

namespace RestaurantWebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Booking
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}