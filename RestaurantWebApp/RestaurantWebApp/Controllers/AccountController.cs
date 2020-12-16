using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantWebApp.DataTransferObject;

namespace RestaurantWebApp.Controllers
{
    public class AccountController : Controller
    {

        
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
            var s = HttpContext.Response;
            //var token = user.Token;
            //var uname = user.Username;

            //if (ModelState.IsValid)
            //{
            //    //var data = _db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
            //    //var data = _bs.GetUser(ConfigurationManager.AppSettings["ServiceApi"]);
            //    if (data.Count() > 0)
            //    {
            //        //add session
            //        Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
            //        Session["Email"] = data.FirstOrDefault().Email;
            //        Session["idUser"] = data.FirstOrDefault().idUser;
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        ViewBag.error = "Login failed";
            //        return RedirectToAction("Login");
            //    }
            //}
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            //Session.Clear();//remove session
            //ControllerContext.HttpContext.Cache.Remove()
            return RedirectToAction("Login");
        }
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Booking");
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
                return RedirectToAction("Index");
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