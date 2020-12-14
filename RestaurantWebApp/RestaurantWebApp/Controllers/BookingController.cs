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
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Booking/Reservation
        [HttpGet]
        public ActionResult Reservation()
        {
            var tables = _tableService.GetAll();
            if (tables == null) return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);

            var reservation = new ReservationDTO();
            return View(reservation);
        }

        // POST: Booking/Reservation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Reservation(ReservationDTO reservation)
        {
            var date = Request.Form["ReservationTimeHid"];
            if (DateTime.TryParse(date, out var dt3))
                reservation.ReservationTime = dt3;
            else
                return View(reservation);

            var r = Request.Form["Tables"];
            if (!string.IsNullOrEmpty(r))

                //dette tager tables som kommer som en lang string og laver dem om til en liste
                try
                {
                    var tables = ConvertStringToTables.StringOfIdToTables(r);
                    if (tables.Count() <= 0) return View(reservation);
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

        [HttpGet]
        public ActionResult OrderFood(ReservationDTO reservation)
        {
            if (reservation == null || reservation.Id <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "no valid reservaion");

            var fdto = _foodService.GetAll();
            if (fdto == null) return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
            var Foods = new List<FoodDTO>();
            //IEnumerable<FoodDTO> Drinks = new List<FoodDTO>();
            var Drinks = new List<FoodDTO>();
            foreach (var item in fdto)
                if (item.FoodCategoryName.Equals("Mad"))
                    Foods.Add(item);
                else if (item.FoodCategoryName.Equals("Drikkevare")) Drinks.Add(item);


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

            var foodList = ConvertStringToFoodLists
                .ListOfFoodsIdStringsToFoodList(foodListOfStrings, foodsListFromApi);
            var drinkList = ConvertStringToFoodLists
                .ListOfFoodsIdStringsToFoodList(drinkListOfStrings, foodsListFromApi);

            var allGood = int.TryParse(data3, out var ReservationId);

            cvm.ListFood = foodList;
            cvm.ListDrink = drinkList;
            cvm.Reservation = new ReservationDTO(ReservationId);
            cvm.OrderSummary = new List<FoodDTO>();

            if (orderSummaryListOfStrings != null && orderSummaryListOfStrings.Count > 0 && allGood)
            {
                var orderLineList =
                    ConvertStringToOrderLines
                        .ListOfFoodsIdToOrderLines(orderSummaryListOfStrings, foodsListFromApi);
                var r = new ReservationDTO(ReservationId);

                //if order is null -> then everything after ??(null-coalescing) runs
                var order = _orderService
                                .GetAll()
                                .Where(x => x.ReservationID == r.Id)
                                .OrderBy(x => x.OrderDate)
                                .FirstOrDefault()
                            ?? new OrderDTO
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

            //TODO add so same view return on fail  with same reservaion id. maybe just give a reservation 


            return View(cvm);

        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
    }
}