using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Model;
using RestaurantWebApp.Service.Interfaces;
using RestaurantWebApp.Util;

namespace RestaurantWebApp.Controllers
{
    //[Authorize]
    public class BookingController : Controller
    {
        private readonly IFoodService _foodService;
        private readonly IOrderService _orderService;

        private readonly IReservationService _reservationService;
        private readonly ITableService _tableService;
        private ControllerContext _context = new ControllerContext();


        public BookingController(IReservationService reservationService, ITableService tableService,
            IFoodService foodService, IOrderService orderService)
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

            var rv = new ReservationDTO {Tables = _tableService.GetAll()};
            if (rv.Tables == null) return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);

            //rv.TimeSlots = _reservationService
            //TODO  dette er temp  her skal være

            var ls = new List<ReservationTimesDTO>();
            var dateToDay = DateTime.Now;
            var ts = new TimeSpan(17, 30, 0);
            dateToDay = dateToDay.Date + ts;
            for (var i = 0; i < 5; i++)
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
                reservation.ReservationTime = datetime;
            }

            var r = Request.Form["Tables"];
            if (!string.IsNullOrEmpty(r))

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

            //if (!ModelState.IsValid)
            //{
            //    return View(reservation);
            //}

            var response = await _reservationService.CreateAsync(reservation);

            if (response.StatusCode == HttpStatusCode.OK && reservation.OrderingFood == false)
                return RedirectToAction("Index");

            if (response.StatusCode == HttpStatusCode.OK && reservation.OrderingFood)
            {
                var res = JsonConvert.DeserializeObject<ReservationDTO>(response.Content);
                if (res != null)
                    //return OrderFood(res);
                    return RedirectToAction("OrderFood", res);
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


            if (reservation == null) return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "no valid reservaion");

            var fdto = _foodService.GetAll();
            if (fdto == null) return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);

            var Foods = new List<FoodDTO>();
            //IEnumerable<FoodDTO> Drinks = new List<FoodDTO>();
            var Drinks = new List<FoodDTO>();
            foreach (var item in fdto)
                if (item.FoodCategoryName.Equals("Mad"))
                    Foods.Add(item);
                else if (item.FoodCategoryName.Equals("Drikkevare")) Drinks.Add(item);

            //TODO at later time use Viewmodel for better MOdel binding or find the cause of bad binding with the use tuple
            var model = new FoodViewModel();

            var cvm = new CustomViewModel();


            cvm.ListDrink = Foods;
            cvm.ListFood = Drinks;
            cvm.OrderSummary = new List<FoodDTO>();
            cvm.Reservation = reservation;


            var lsback = new List<FoodDTO>();

            var res =
                Tuple.Create(Foods, Drinks, lsback, reservation);

            //return View(res);
            return View(cvm);
        }

        // POST: Booking/OrderFoods
        [HttpPost]
        public async Task<ActionResult> OrderFood(
            Tuple<List<FoodDTO>, List<FoodDTO>, List<FoodDTO>, ReservationDTO> dTuple)

        public async Task<ActionResult> OrderFood(CustomViewModel cvm)
        {
            //  data from view
            var foods = Request.Form["listFood"];
            var drinks = Request.Form["listDrink"];
            var orders = Request.Form["OrderSummary"];
            var data3 = Request.Form["ReservationNumber"];

            var orderSummaryListOfStrings = orders?.Split(',').ToList();
            var foodListOfStrings = foods?.Split(',').ToList();
            var drinkListOfStrings = drinks?.Split(',').ToList();
            var foodsListFromApi = _foodService.GetAll().ToList();

            var foodList = ConvertStringToFoodLists.ListOfFoodsIdStringsToFoodList(foodListOfStrings, foodsListFromApi);
            var drinkList =
                ConvertStringToFoodLists.ListOfFoodsIdStringsToFoodList(drinkListOfStrings, foodsListFromApi);

            var allGood = int.TryParse(data3, out var ReservationId);

            cvm.ListFood = foodList;
            cvm.ListDrink = drinkList;
            cvm.Reservation = new ReservationDTO(ReservationId);
            cvm.OrderSummary = new List<FoodDTO>();


            if (!orderSummaryListOfStrings.IsNullOrEmpty() && allGood)
            {
                var orderLineList =
                    ConvertStringToOrderLines.ListOfFoodsIdToOrderLines(orderSummaryListOfStrings, foodsListFromApi);
                var r = new ReservationDTO(ReservationId);

                //if order is null -> then everything after ??(null-coalescing) runs
                var order = _orderService.GetAll().Where(x => x.ReservationID == r.Id).OrderBy(x => x.OrderDate)
                    .FirstOrDefault() ?? new OrderDTO

                {
                    ReservationID = r.Id,
                    OrderDate = DateTime.Today,
                    EmployeeID = 1,
                    PaymentCondition = PaymentCondition.Begyndt.ToString(),
                    OrderLines = orderLineList
                };

                var response = await _orderService.CreateAsync(order);

                if (response.StatusCode == HttpStatusCode.OK) return RedirectToAction("Index");
            }
            else
            {
                //TODO add error msg that no res id matched/wasChanged or was not net from the creation of reservation
                //if all good fails

                //TODO add error msg to view
                //NO Food were selected
                //throw new NotImplementedException("NO Food were selected or no food return from post");
                return View(cvm);
            }

            //TODO add so same view return on fail  with same reservaion id.


            return View(cvm);
        }
    }
}