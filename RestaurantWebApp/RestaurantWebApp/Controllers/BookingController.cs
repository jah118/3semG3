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

        private readonly IReservationService _reservationService;
        private readonly ITableService _tableService;
        private readonly IFoodService _foodService;
        private readonly IOrderService _orderService;

        public BookingController(IReservationService reservationService, ITableService tableService, IFoodService foodService, IOrderService orderService)
        {
            _reservationService = reservationService;
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
            //var availableTimes = _reservationService.GetReservationTimeByDate(DateTime.Now.Date);
            //if (availableTimes == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
            //}


            var reservation = new ReservationDTO();
            //reservation.TimeSlots = availableTimes;
            //if (rv.Tables == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
            //}

            //rv.TimeSlots = _reservationService
            //TODO  dette er temp  her skal være

            //var ls = new List<ReservationTimesDTO>();
            //var dateToDay = DateTime.Now;
            //var ts = new TimeSpan(17, 30, 0);
            //dateToDay = dateToDay.Date + ts;
            //for (int i = 0; i < 5; i++)
            //{
            //    ts += TimeSpan.FromHours(1);
            //    dateToDay.AddHours(1);
            //    ls.Add(new ReservationTimesDTO(dateToDay, ts));
            //}

            //rv.TimeSlots = ls;
            //return View(rv);
            return View(reservation);
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
            var date = Request.Form["ReservationTimeHid"];
            if (DateTime.TryParse(date, out var dateTime))
            {
                reservation.ReservationTime = dateTime;
            }


            //if (time.Length > 0)
            //{
            //    DateTime datetime = FormatTime.FormatterForReservationTimeFromString(date, time);
            //    reservation.ReservationTime = datetime;
            //}

            var r = Request.Form["Tables"];
            if (!string.IsNullOrEmpty(r))

            {
                //dette tager tables som kommer som en lang string og laver dem om til en liste
                try
                {
                    var tables = ConvertStringToTables.StringOfIdToTables(r);
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

            var response = await _reservationService.CreateAsync(reservation);

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
                    return RedirectToAction("OrderFood", res);
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

        [HttpGet]
        public ActionResult OrderFood(ReservationDTO reservation)
        //public ActionResult OrderFood()
        {

            //TODO remove this when test arf made for order or this not need any more
            //this just makes it so u can skip reservation page 

            //////////////////////////

            //ReservationDTO reservation = new ReservationDTO(
            //    41,
            //    new CustomerDTO("88888888", "JensJensen@gmaiul.com", "jens", "jensen", "jensvej 2", "9000", "aalborg"),
            //    DateTime.Now,
            //    5,
            //    false,
            //    "TEST",
            //    new List<RestaurantTablesDTO> { new RestaurantTablesDTO(97, 2, 97), new RestaurantTablesDTO(98, 4, 98) }

            //    );
            ///////////////////////////////



            if (reservation == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "no valid reservaion");
            }

            var fdto = _foodService.GetAll();
            if (fdto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
            }
            var Foods = new List<FoodDTO>();
            //IEnumerable<FoodDTO> Drinks = new List<FoodDTO>();
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

            //TODO at later time use Viewmodel for better MOdel binding or find the cause of bad binding with the use tuple
            FoodViewModel model = new FoodViewModel();

            CustomViewModel cvm = new CustomViewModel();

            // //cvm.listDrink = Drinks;
            // IEnumerable<FoodDTO> d;

            // model.VmFoods = Foods;
            // model.VmDrinks = Drinks;
            // model.Slf = GetSelectListItems(Foods);
            // model.Sld = GetSelectListItems(Drinks);
            // model.VmOrderFoodAndDrinks = new List<SelectList>();

            var lsback = new List<FoodDTO>();

            Tuple<List<FoodDTO>, List<FoodDTO>, List<FoodDTO>, ReservationDTO> res = Tuple.Create(Foods, Drinks, lsback, reservation);

            return View(res);
        }

        // POST: Booking/OrderFoods
        [HttpPost]
        public async Task<ActionResult> OrderFood(List<string> Item3, string ReservationNumber)
        //public async Task<ActionResult> OrderFood(string testhej)
        {
            //var data = res;
            //var data = Request.Form["Item1"];
            //var data1 = Request.Form["Item2"];
            //var data2 = Request.Form["Item3"];
            var data3 = Request.Form["ReservationNumber"];
            //var date22 = Request.Form["VmOrderFoodAndDrinks"];

            List<FoodDTO> foodsListFromApi = _foodService.GetAll().ToList();
            var allGood = int.TryParse(data3, out var ReservationId);
            if (Item3.Count > 0 && allGood)
            {
                var orderLineList = ConvertStringToOrderLines.ListOfFoodsIdToOrderLines(Item3, foodsListFromApi);
                var r = new ReservationDTO(ReservationId);
                OrderDTO order = _orderService.GetAll().Where(x => x.ReservationID == r.Id).OrderBy(x => x.OrderDate).FirstOrDefault();
                if (order == null)
                {
                    order = new OrderDTO
                    {
                        ReservationID = r.Id,
                        OrderDate = DateTime.Today,
                        EmployeeID = 1,
                        PaymentCondition = PaymentCondition.Begyndt.ToString(),
                        OrderLines = orderLineList
                    };
                }
                else
                {
                    //TODO add logic to handel if is in use by a order
                }

                var response = await _orderService.CreateAsync(order);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                //TODO add error msg that no res id matched/wasChanged or was not net from the creation of reservation
                //if all good fails

                //TODO add error msg to view
                //NO Food were selected
                //throw new NotImplementedException("NO Food were selected or no food return from post");
                return View(ReservationNumber);
            }

            //TODO add so same view return on fail  with same reservaion id.

            return View(ReservationNumber);
            //return View();
        }
    }
}