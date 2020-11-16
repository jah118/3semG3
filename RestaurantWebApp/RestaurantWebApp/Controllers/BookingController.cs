using DataAccess.DataTransferObjects;
using Newtonsoft.Json;
using RestaurantWebApp.Service;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RestaurantWebApp.Controllers
{
    public class BookingController : Controller
    {
        private string _constr = ConfigurationManager.AppSettings["ServiceApi"];
        
        private BokingServices bs = new BokingServices();

        public void GetCustomer()
        {
            var client = new RestClient("https://localhost:44349/api/customer");

            var request = new RestRequest("Customer/{CustomerId}", Method.GET);

            request.AddUrlSegment("{CustomerId", 1);

            var content = client.Execute(request).Content;
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

            IEnumerable<SelectListItem> items = bs.GetBookingTables(_constr).Select(c => new SelectListItem
            {
                Value = c.Id + "",
                Text = c.TableNumber + ""

            });
            ViewBag.Tables2 = items;

            rv.Tables =  bs.GetBookingTables(_constr);
            return View(rv);
        }

        // POST: Booking/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection, ReservationDTO reservation)
        {
             string _constr2 = ConfigurationManager.AppSettings["ToiletApiPost"];
            try
            {

                var response =  await bs.PostBookingAsync(reservation, _constr2);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }else
                {
                    return View();
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
