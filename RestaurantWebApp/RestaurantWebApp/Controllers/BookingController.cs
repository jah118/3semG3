using DataAccess.DataTransferObjects;
using RestaurantWebApp.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RestaurantWebApp.Controllers
{
    public class BookingController : Controller
    {
        private BokingServices bs = new BokingServices();

        public ActionResult Calender()
        {
            return View();
        }

        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Booking/Create
        public ActionResult Create()
        {
            ReservationDTO rv = new ReservationDTO();
            rv.Tables = bs.GetBookingTables(ConfigurationManager.AppSettings["ServiceApi"]);
            
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
            if (r!=null)
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
                var response = await bs.PostBookingAsync(reservation, ConfigurationManager.AppSettings["ServiceApi"]);
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
        public ActionResult Edit(int id)
        {
            return View();
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

        //GET: Booking/Food
        [HttpGet]
        public ActionResult OrderFoods()
        {
            //var client = new RestClient("https://localhost:44349/api/Food");

            //var request = new RestRequest("Food/{FoodId}", Method.GET);

            //request.AddUrlSegment("{FoodId", 1);

            //var content = client.Execute(request).Content;

            return View();
        }
    }
}