using DataTransferObjects;
using RestaurantWebApp.Model;
using RestaurantWebApp.Service;
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
            return View(rv);
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ReservationDTO reservation)
        {
            var datetime = Request.Form["ReservationTime"];
            datetime = datetime.Replace(',', ' ');
            // bool succes = datetime.TryParse()

            //reservation.ReservationTime =


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
        [HttpGet]
        public ActionResult OrderFoods()
        {
            IEnumerable<FoodDTO> fdto = bs.GetAllFoods(ConfigurationManager.AppSettings["ServiceApi"]);

            return View(fdto);

        }
        [HttpGet]
        public ActionResult OrderFood()
        {
            var f = new FoodViewModel();
            IEnumerable<FoodDTO> fdto = bs.GetAllFoods(ConfigurationManager.AppSettings["ServiceApi"]);

            var Foods = new List<FoodDTO>();
            var Drinks = new List<FoodDTO>();
            foreach (var item in fdto)
            {
                if (item.FoodCategoryName.Name.Equals("Mad"))

                {
                    Foods.Add(item);
                }
                else if(item.FoodCategoryName.Name.Equals("Drikkevare"))
                {
                    Drinks.Add(item);
                } 
                
                       
            }

            
            f.Foods = Foods;
            f.Drinks = Drinks;
            
            return View(f);
        }
    }
}

