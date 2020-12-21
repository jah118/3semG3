using System.Web.Mvc;
using RestaurantWebApp.Model;

namespace RestaurantWebApp.Controllers
{
    [LoginRequired]
    public class ManageController : Controller
    {
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }
    }
}