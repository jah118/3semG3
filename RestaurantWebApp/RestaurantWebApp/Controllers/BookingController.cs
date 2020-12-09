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

            //TODO her skal laves så den kan tage begge former for login Username/Email
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

            //bruge timeslot
            var date = Request.Form["ReservationTimeHid"];
            var date2 = Request.Form["ReservationTimeHid2"];

            //TODO Double up er fordi sometime only gods knows vil den første fejle men den næste virker med samme date string....
            if (DateTime.TryParse(date2, out var dt2))
                reservation.ReservationTime = dt2;
            else if (DateTime.TryParse(date, out var dt3))
                reservation.ReservationTime = dt3;
            else
                return View(reservation);
            
            var r = Request.Form["Tables"];
            if (!string.IsNullOrEmpty(r))

                //dette tager tables som kommer som en lang string og laver dem om til en liste
                try
                {
                    var tables = ConvertStringToTables.StringOfIdToTables(r);
                    if (tables.Count()<=0)
                    {
                        return View(reservation);
                    }
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
            //public ActionResult OrderFood()
        {
            if (reservation == null || reservation.Id <= 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "no valid reservaion");

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

            // //cvm.listDrink = Drinks;
            // IEnumerable<FoodDTO> d;

            // model.VmFoods = Foods;
            // model.VmDrinks = Drinks;
            // model.Slf = GetSelectListItems(Foods);
            // model.Sld = GetSelectListItems(Drinks);
            // model.VmOrderFoodAndDrinks = new List<SelectList>();

            var lsback = new List<FoodDTO>();

            var res = Tuple.Create(Foods, Drinks, lsback, reservation);

            return View(res);
        }

        // POST: Booking/OrderFoods
        [HttpPost]
        public async Task<ActionResult> OrderFood(List<string> Item3)
        //public async Task<ActionResult> OrderFood(string testhej)
        {
            //var data = res;
            //var data = Request.Form["Item1"];
            //var data1 = Request.Form["Item2"];
            //var data2 = Request.Form["Item3"];
             
            var data3 = Request.Form["ReservationNumber"];
            //var date22 = Request.Form["VmOrderFoodAndDrinks"];
            string ReservationNumber = data3;

            var foodsListFromApi = _foodService.GetAll().ToList();
            var allGood = int.TryParse(data3, out var ReservationId);
            if (Item3.Count > 0 && allGood)
            {
                var orderLineList = ConvertStringToOrderLines.ListOfFoodsIdToOrderLines(Item3, foodsListFromApi);
                var r = new ReservationDTO(ReservationId);
                var order = _orderService.GetAll().Where(x => x.ReservationID == r.Id).OrderBy(x => x.OrderDate)
                    .FirstOrDefault();
                if (order == null)
                    order = new OrderDTO
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
                return View(ReservationNumber);
            }

            //TODO add so same view return on fail  with same reservaion id.

            return View(ReservationNumber);
            //return View();
        }
    }
}