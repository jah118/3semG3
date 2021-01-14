using System.Web.Mvc;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;

namespace RestaurantWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AccountController(IAuthService authRepository, IUserService userService)
        {
            _authService = authRepository;
            _userService = userService;
        }

        //GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            var user = new UserDTO {AccountType = UserRoles.Customer};
            return View(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserDTO user)
        {
            var token = _authService.Authenticate(user.Username, user.Password);

            if (token != null)
            {
                Session["Token"] = token;
                var data = _userService.GetUserByToken();

                if (data != null)
                {
                    //add sessions
                    Session["FullName"] = data.Customer.FirstName + " " + data.Customer.LastName;
                    Session["Username"] = data.Customer;
                    Session["UserId"] = data.Id;
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.error = "Login failed";
            return RedirectToAction("Login");
        }

        // POST: /Account/Logout
        public ActionResult Logout()
        {
            Session.Clear(); //remove session
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


        //GET: Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            var user = new UserDTO();
            return View(user);
        }

        //TODO service needs validation for creation a customer, so not in use for now
        //POST: Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserDTO user)
        {
            if (ModelState.IsValid)
                //TODO implemented create function in auth service as customer
                // _authRepository.Create(user.Customer, user.Username, user.Password);
                //if conflic  ViewBag.error = "Email already exists";
                return RedirectToAction("Index", "Home");

            return View();
        }
    }
}