using RestaurantWebApp.DataTransferObject;
using RestaurantWebApp.Model;
using RestaurantWebApp.Service.Interfaces;
using RestaurantWebApp.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace RestaurantWebApp.Controllers
{
    //TODO adding when login is comfirmed
    //[LoginRequired]
    public class BookingController : Controller
    {
        private readonly IAuthService _authRepository;
        private readonly IService<CustomerDTO> _customerservice;
        private readonly IFoodService _foodService;
        private readonly IOrderService _orderService;

        private readonly IReservationService _reservationService;
        private readonly ITableService _tableService;

        //This use as long there no login feature implemented 
        public BookingController(IReservationService reservationService, ITableService tableService,
            IFoodService foodService, IOrderService orderService)
        {
            _reservationService = reservationService;
            _tableService = tableService;
            _foodService = foodService;
            _orderService = orderService;
        }
        //This constructor is used when a person is login
        public BookingController(IReservationService reservationService, ITableService tableService,
            IFoodService foodService, IOrderService orderService, IAuthService authRepository,
            IService<CustomerDTO> customerService)
        {
            _reservationService = reservationService;
            _tableService = tableService;
            _foodService = foodService;
            _orderService = orderService;
            _authRepository = authRepository;
            _customerservice = customerService;
        }

        // GET: Booking/Reservation
        [HttpGet]
        public ActionResult Reservation()
        {
            var reservation = new ReservationDTO();
            reservation.ReservationDate =  DateTime.Now;
            reservation.OrderingFood = false;


            if (Session["CustomerId"] != null)
            {
                var current = Session["CustomerId"];
                var customer = _customerservice.GetById((int)current);
                if (customer == null) return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);

                reservation.Customer = customer;
            }
            return View(reservation);
        }

        // POST: Booking/Reservation
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reservation(ReservationDTO reservation)
        {
            //takes a string from Request and tries to convert to datetime
            var date = Request.Form["ReservationTimeHid"];
            if (DateTime.TryParse(date, out var dt3))
                reservation.ReservationTime = dt3;
            else
                return View(reservation);

            var r = Request.Form["Tables"];
            if (!string.IsNullOrEmpty(r))
            {

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

                var response = _reservationService.Create(reservation);

                if (response != null && reservation.OrderingFood == false && response.Id > 0)
                    return RedirectToAction("Index", "Home");

                if (response != null && reservation.OrderingFood && response.Id > 0)
                    return RedirectToAction("OrderFood", response);
            }

            return View(reservation);
        }

        [HttpGet]
        public ActionResult OrderFood(ReservationDTO reservation)
        {
            if (reservation == null || reservation.Id <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "no valid reservaion");

            var allFoodsFromService = _foodService.GetAll();
            if (allFoodsFromService == null) return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
            var foods = new List<FoodDTO>();
            var drinks = new List<FoodDTO>();
            foreach (var item in allFoodsFromService)
                if (item.FoodCategoryName.Equals("Mad"))
                    foods.Add(item);
                else if (item.FoodCategoryName.Equals("Drikkevare")) drinks.Add(item);

            var cvm = new CustomViewModel
            {
                ListDrink = foods,
                ListFood = drinks,
                OrderSummary = new List<FoodDTO>(),
                Reservation = reservation
            };

            return View(cvm);
        }


        /// <summary>
        ///  POST: Booking/OrderFoods takes custom model
        /// post of a order, it runs through the Request to rebuild the custom view 
        /// </summary>
        /// <param name="cvm"></param>
        /// <returns></returns>
        /// 
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult OrderFood(CustomViewModel cvm)
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

                var order = new OrderDTO
                {
                    ReservationID = r.Id,
                    OrderDate = DateTime.Today,
                    EmployeeID = 1,
                    PaymentCondition = PaymentCondition.Bestilt.ToString(),
                    OrderLines = orderLineList
                };

                var response = _orderService.Create(order);

                if (response != null) return RedirectToAction("Index", "Home");
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

            return View(cvm);
        }
    }
}