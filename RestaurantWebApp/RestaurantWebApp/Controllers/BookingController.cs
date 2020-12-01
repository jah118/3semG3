using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using RestaurantWebApp.Util;

namespace RestaurantWebApp.Controllers
{
    //[Authorize]
    public class BookingController : Controller
    {
        private ControllerContext _context = new ControllerContext();
        

        private readonly IBookingService _bookingService;
        private readonly ITableService _tableService;

        public BookingController(IBookingService bookingService, ITableService tableService)
        {
            _bookingService = bookingService;
            _tableService = tableService;
        }

        // GET: Booking
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Booking/Create
        public ActionResult Create()
        {
            //TODO her skal laves så den kan tage begge former for login Username/Email
            //if (Session["Username"] != null)
            //{
            ReservationDTO rv = new ReservationDTO();
            rv.Tables = _tableService.GetAll();
            if (rv.Tables == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
            }

            //TODO  dette er temp  her skal være
            var ls = new List<ReservationTimesDTO>();
            var dateToDay = DateTime.Now;
            var ts = new TimeSpan(17, 30, 0);
            dateToDay = dateToDay.Date + ts;
            for (int i = 0; i < 5; i++)
            {
                ts += TimeSpan.FromHours(1);
                dateToDay.AddHours(1);
                ls.Add(new ReservationTimesDTO(dateToDay, ts));
            }

            rv.TimeSlots = ls;
            return View(rv);
            //}
            //else
            //{
            //    return RedirectToAction("Login");
            //}
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ReservationDTO reservation)
        {
            //TODO senere når api virker ville der bar kunne bruges uden at kunne konverter 
            //bruge timeslot
            var date = Request.Form["ReservationTime"];
            var time = Request.Form["Timeslots"];

            if (time.Length > 0)
            {
                var datetime = FormatTime.FormatterForReservationTimeFromString(date, time);
                if (datetime != null) reservation.ReservationTime = datetime;
            }
         
            var r = Request.Form["Tables"];
            if (!string.IsNullOrEmpty(r))
            {
                //dette tager tables som kommer som en lang string og laver dem om til en liste
                try
                {
                    var tables = FormatStringToTables.StringOfIdToTables(r);
                    reservation.Tables = tables;
                }
                catch (FormatException e)
                {
                    Debug.WriteLine(e);
                    return View(reservation);
                }
            }

            //if (!ModelState.IsValid)
            //{
            //    return View(reservation);
            //}
            var response = await _bookingService.CreateAsync(reservation);

            if (response == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            return View(reservation);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Booking/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            UserDTO user = new UserDTO {AccountType = UserRoles.Customer};
            return View(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserDTO user)
        {
           var s =  HttpContext.Response;
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

        ////GET: Booking/Food
        //[HttpGet]
        //public ActionResult OrderFoods()
        //{
        //    //var client = new RestClient("https://localhost:44349/api/Food");

        //    //var request = new RestRequest("Food/{FoodId}", Method.GET);

        //    //request.AddUrlSegment("{FoodId", 1);

        //    //var content = client.Execute(request).Content;

        //    return View();
        //}
    }
}