using RestaurantWebApp.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Service.Interfaces;

namespace RestaurantWebApp.Controllers
{
    [Authorize]
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

            //TODO  dette er temp  her skal være 
            //d
            var ls = new List<ReservationTimesDTO>();
            DateTime _dateToDay = DateTime.Now;
            TimeSpan ts = new TimeSpan(17, 30, 0);
            _dateToDay = _dateToDay.Date + ts;
            for (int i = 0; i < 5; i++)
            {
                ts += TimeSpan.FromHours(1);
                _dateToDay.AddHours(1);
                ls.Add(new ReservationTimesDTO(_dateToDay, ts));
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
            //TODO senere når api virker ville der bar kunne bruge 
            //bruge timeslot
            var date = Request.Form["ReservationTime"];
            var time = Request.Form["Timeslots"];

            if (time.Length > 0)
            {
                string[] timesplit = time.Split(' ');

                var temp = date + " " + timesplit[1];

                DateTime datetime;
                if (DateTime.TryParse(temp, out datetime))
                {
                    reservation.ReservationTime = datetime;
                }
            }



            //dette tager tables som kommer som en lang string og laver dem om til en liste
            //af strings, som splites ved ','
            //og laves til RestaurantTablesDTO objekt spm puttes i en liste
            var r = Request.Form["Tables"];
            if (r != null)
            {


                List<string> listStrLineElements = r.Split(',').ToList();
                var tables = new List<RestaurantTablesDTO>();
                int tempId;
                foreach (string item in listStrLineElements)
                {
                    //tables.Add(new RestaurantTablesDTO(Int32.Parse(item), 0, 0));     //old way
                    tempId = 0;
                    if (int.TryParse(item, out tempId))
                    {
                        tables.Add(new RestaurantTablesDTO(tempId, 0, 0));
                    }
                    else
                    {
                        //TODO need a return message of what failed eks: dette er ikke et valid valg bord
                        return View(reservation);
                    }
                }
                reservation.Tables = tables;
            }



            if (ModelState.IsValid)
            {
                var response = await _bookingService.CreateAsync(reservation);
                if (response)
                {
                    return RedirectToAction("Index");
                }
            }
            //TODO need a return message of what failed
            //TODO skal vise alle bordene ikke dem der valgt før<
            return View(reservation);
            //return View();

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
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            //if (ModelState.IsValid)
            //{


            //    var f_password = GetMD5(password);

            //    //var data = _db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
            //    var data = _bs.GetUser(ConfigurationManager.AppSettings["ServiceApi"]);
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

            //if (ModelState.IsValid)
            //{
            //    var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
            //    if (check == null)
            //    {

            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        ViewBag.error = "Email already exists";
            //        return View();
            //    }


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
