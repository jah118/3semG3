using System.Web.Mvc;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Model;
using RestaurantWebApp.Service.Interfaces;

namespace RestaurantWebApp.Controllers
{
    [LoginRequired]
    public class ManageController : Controller
    {
        private readonly IUserService _userService;
        private readonly IReservationService _reservationService;

        public ManageController(IUserService userService, IReservationService reservationService)
        {
            _reservationService = reservationService;
            _userService = userService;
        }
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Info()
        {
            var data = _userService.GetUserById((int)Session["UserId"]);
            var customer = data.Customer;
            return View(customer);
        }

        public ActionResult Reservations()
        {
            var data = _reservationService.GetReservationByCustomerId((int)Session["CustomerId"]);
            return View(data);
        }
    }
}