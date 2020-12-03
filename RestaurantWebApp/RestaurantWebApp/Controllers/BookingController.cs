using Newtonsoft.Json;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Model;
using RestaurantWebApp.Service.Interfaces;
using RestaurantWebApp.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RestaurantWebApp.Controllers
{
    //[Authorize]
    public class BookingController : Controller
    {
        private ControllerContext _context = new ControllerContext();

        private readonly IBookingService _bookingService;
        private readonly ITableService _tableService;
        private readonly IFoodService _foodService;
        private readonly IOrderService _orderService;

        public BookingController(IBookingService bookingService, ITableService tableService, IFoodService foodService, IOrderService orderService)
        {
            _bookingService = bookingService;
            _tableService = tableService;
            _foodService = foodService;
            _orderService = orderService;
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

            ReservationDTO rv = new ReservationDTO();


            rv.Tables = _tableService.GetAll();
            if (rv.Tables == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
            }

            //rv.TimeSlots = _bookingService
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
                DateTime datetime = FormatTime.FormatterForReservationTimeFromString(date, time);
                reservation.ReservationTime = datetime;
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

            if (response.StatusCode == HttpStatusCode.OK && reservation.OrderingFood == false)
            {
                return RedirectToAction("Index");
            }

            if (response.StatusCode == HttpStatusCode.OK && reservation.OrderingFood)
            {
                var res = JsonConvert.DeserializeObject<ReservationDTO>(response.Content);
                if (res != null)
                {
                    //return OrderFood(res);
                    RedirectToAction("OrderFood", res);
                }
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

        //POST: Booking/OrderFoods
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        // public async ActionResult OrderFoods(FoodDTO food)
        // {
        //var client = new RestClient("https://localhost:44349/api/Food");

        //var request = new RestRequest("Food/{FoodId}", Method.GET);

        //request.AddUrlSegment("{FoodId", 1);

        //var content = client.Execute(request).Content;

        //    return View();
        // }

        // GET: Booking/OrderFoods
        //[HttpGet]
        //public ActionResult OrderFoods()
        //{
        //    IEnumerable<FoodDTO> fdto = _foodService.GetAll();

        //    return View(fdto);

        //}
        [HttpGet]
        public ActionResult OrderFood(ReservationDTO reservation)
        {
            var foodsview = new FoodViewModel();
            IEnumerable<FoodDTO> fdto = _foodService.GetAll();




            var Foods = new List<FoodDTO>();
            var Drinks = new List<FoodDTO>();
            foreach (var item in fdto)
            {
                if (item.FoodCategoryName.Equals("Mad"))
                {
                    Foods.Add(item);
                }
                else if (item.FoodCategoryName.Equals("Drikkevare"))
                {
                    Drinks.Add(item);
                }
            }


            OrderDTO order = _orderService.GetAll().Where(x => x.ReservationID == reservation.Id).OrderBy(x => x.OrderDate).FirstOrDefault();
            if (order == null)
            {
                order = new OrderDTO
                {
                    ReservationID = reservation.Id,
                    PaymentCondition = PaymentCondition.Begyndt.ToString()
                };
            }
            foodsview.Foods = Foods;
            foodsview.Drinks = Drinks;
            foodsview.Order = order;

            //Tuple<OrderDTO, FoodViewModel> res = Tuple.Create(order, foods);
            return View(foodsview);
        }

        // POST: Booking/OrderFoods
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult OrderFood(FoodViewModel foodsview)
        {
            var data = foodsview;
        //    var client = new RestClient("https://localhost:44349/api/Food");

        //    var request = new RestRequest("Food/{FoodId}", Method.GET);

        //    request.AddUrlSegment("{FoodId", 1);

        //    var content = client.Execute(request).Content;

            return View();
        }


    }
}