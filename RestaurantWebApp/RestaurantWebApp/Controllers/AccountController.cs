using System.Web;
using System.Web.Mvc;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;

namespace RestaurantWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IService<CustomerDTO> _customerService;

        public AccountController(IService<CustomerDTO> customerService, IAuthService authRepository, IUserService userService)
        {
            _authService = authRepository;
            _customerService = customerService;
            _userService = userService;
        }

        //GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            var user = new UserDTO { AccountType = UserRoles.Customer };
            return View(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserDTO user)
        {
            string token = _authService.Authenticate(user.Username, user.Password);

            if (token != null)
            {
                Session["Token"] = token;
                //var data = _customerService.GetById(1);
                var data = _userService.GetUserByToken();

                if (data != null)
                {
                    //add session
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Clear(); //remove session
            Session.Abandon();
            return RedirectToAction("Login");
        }


        //GET: Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            var user = new UserDTO();
            return View(user);
        }

        //POST: Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                //TODO implemented create function in auth service as customer
                // _authRepository.Create(user.Customer, user.Username, user.Password);
                //if conflic  ViewBag.error = "Email already exists";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}