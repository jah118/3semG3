using DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantWebAppCore.Models;
using RestaurantWebAppCore.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAppCore.Controllers
{
    public class BookingController : Controller
    {
        private readonly ILogger<BookingController> _logger;
        private readonly BookingServices _bs;

        public BookingController(ILogger<BookingController> logger,  BookingServices bs  )
        {
            _logger = logger;
            _bs = bs;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Booking/Create
        public IActionResult Create()
        {
            //TODO her skal laves så den kan tage begge former for login Username/Email
            //if (Session["Username"] != null)
            //{
            ReservationDTO rv = new ReservationDTO();
            rv.Tables = _bs.GetBookingTables();

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
        public async Task<IActionResult> Create(ReservationDTO reservation)
        {
            //TODO senere når api virker ville der bar kunne bruge 
            //bruge timeslot
            var date = Request.Form["ReservationTime"];
            string time = Request.Form["Timeslots"];

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
            string r = Request.Form["Tables"];
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
                var response = await _bs.PostBookingAsync(reservation);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
            }
            //TODO need a return message of what failed
            //TODO skal vise alle bordene ikke dem der valgt før<
            return View(reservation);
            //return View();

        }

        // GET: Booking/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: Booking/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            //if (ModelState.IsValid)
            //{


            //    var f_password = GetMD5(password);

            //    //var data = _db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
            //    var data = bs.GetUser(ConfigurationManager.AppSettings["ServiceApi"]);
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
        //public IActionResult Logout()
        //{
        //    Session.Clear();//remove session
        //    return RedirectToAction("Login");
        //}

        //GET: Register

        public IActionResult Register()
        {
            var user = new UserDTO();
            return View(user);
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserDTO _user)
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
