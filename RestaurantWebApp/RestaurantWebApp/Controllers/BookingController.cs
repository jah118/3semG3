using DataAccess.DataTransferObjects;
using RestaurantWebApp.Service;
using RestSharp;
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

            IEnumerable<SelectListItem> items = bs.GetBookingTables(ConfigurationManager.AppSettings["ServiceApi"]).Select(c => new SelectListItem
            {
                Value = c.Id + "",
                Text = c.TableNumber + ""
            });
            ViewBag.Tables2 = items;

            rv.Tables = bs.GetBookingTables(ConfigurationManager.AppSettings["ServiceApi"]);
            return View(rv);
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FormCollection collection, ReservationDTO reservation)
        {
            try
            {
                var datetime = Request.Form["ReservationTime"];
                datetime = datetime.Replace(',', ' ');
               
                
                //reservation.ReservationTime = 

                //dette tager tables som kommer som en lang string og laver dem om til en liste
                //af strings, som splites ved ','
                //og laves til RestaurantTablesDTO objekt spm puttes i en liste
                var r = Request.Form["Tables"];
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
                        //TODO need a return message of what failed
                        //eks: dette er ikke et valid valg bord

                        return View(reservation);
                    }
                    //tables.Add(new RestaurantTablesDTO(Int32.Parse(item), 0, 0));
                }

               
                reservation.Tables = tables;
                var response = await bs.PostBookingAsync(reservation, ConfigurationManager.AppSettings["ServiceApi"]);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    //TODO need a return message of what failed
                    return View(reservation);
                }
            }
            catch
            {
                //TODO make popup or make use of something to show what the user is missing to enter.
                return View();
            }
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