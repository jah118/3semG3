using RestaurantWebApp.DataTransferObject;
using System.Web.Mvc;
using RestaurantWebApp.Service.Interfaces;

namespace RestaurantWebApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAuthService _authRepository;
        private readonly IService<CustomerDTO> _customerService;

        public AccountController(IService<CustomerDTO> customerService, IAuthService authRepository)
        {
            _authRepository = authRepository;
            _customerService = customerService;
        }

        //GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            UserDTO user = new UserDTO { AccountType = UserRoles.Customer };
            return View(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserDTO user)
        {
            var use = user.Username;
            var pas = user.Password;
            var f = user.AccountType;
            var s = HttpContext.Response;

           
            //var token = user.Token;
            //var uname = user.Username;

            if (ModelState.IsValid)
            {
                //var data = _db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                //var data = _bs.GetUser(ConfigurationManager.AppSettings["ServiceApi"]);
                Session["Token"] = _authRepository.Authenticate(user.Username, user.Password);
                var data = _customerService.GetById(1);
                if (data != null)
                {
                    //add session
                    Session["FullName"] = data.FirstName + " " + data.LastName;
                    Session["Email"] = data.Email;
                    Session["idUser"] = data.Id;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            Session.Abandon();
            //ControllerContext.HttpContext.Cache.Remove()
            //ControllerContext.HttpContext.Session.
            return RedirectToAction("Login");
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
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
        public ActionResult Register(UserDTO _user)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            //var response = ControllerContext.RequestContext.HttpContext.
            //if (check == null)
            //{
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    ViewBag.error = "Email already exists";
            //    return View();
            //}

            return View();
        }
    }
}