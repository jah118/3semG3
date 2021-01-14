using System.Web.Mvc;
using RestaurantWebApp.Model;
using RestaurantWebApp.Service.Interfaces;

namespace RestaurantWebApp.Controllers
{
    /// <summary>
    ///     User Management class here goes user options
    /// </summary>
    [LoginRequired]
    public class ManageController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;

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
            var data = _userService.GetUserById((int) Session["UserId"]);
            var customer = data.Customer;
            return View(customer);
        }

        public ActionResult Reservations()
        {
            var data = _reservationService.GetReservationByCustomerId();
            return View(data);
        }
    }
}