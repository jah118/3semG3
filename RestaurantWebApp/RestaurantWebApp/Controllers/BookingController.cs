﻿using DataAccess.DataTransferObjects;
using RestaurantWebApp.Service;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            if (Session["idUser"] != null)
            {
                ReservationDTO rv = new ReservationDTO();
                rv.Tables = bs.GetBookingTables(ConfigurationManager.AppSettings["ServiceApi"]);
                return View(rv);
            }
            else
            {
                return RedirectToAction("Login");
            }

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

        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserDTO _user)
        {

            //if (ModelState.IsValid)
            //{
            //    var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
            //    if (check == null)
            //    {
            //        _user.Password = GetMD5(_user.Password);
            //        _db.Configuration.ValidateOnSaveEnabled = false;
            //        _db.Users.Add(_user);
            //        _db.SaveChanges();
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        ViewBag.error = "Email already exists";
            //        return View();
            //    }


            //}
           
            return View();


        }

        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            //if (ModelState.IsValid)
            //{


            //    var f_password = GetMD5(password);
              
            //    //var data = _db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
            //    var data = bs.GetUser(ConfigurationManager.AppSettings["ServiceApi"]);
            //    if (data.Count() > 0)
            //    {
            //        //add session
            //        Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
            //        Session["Email"] = data.FirstOrDefault().Email;
            //        Session["idUser"] = data.FirstOrDefault().idUser;
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        ViewBag.error = "Login failed";
            //        return RedirectToAction("Login");
            //    }
            //}
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }



        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

    }


    ////GET: Booking/Food
    //[HttpGet]
    //public ActionResult OrderFoods()
    //{
    //    //var client = new RestClient("https://localhost:44349/api/Food");

    //    //var request = new RestRequest("Food/{FoodId}", Method.GET);

    //    //request.AddUrlSegment("{FoodId", 1);

    //    //var content = client.Execute(request).Content;

    //    return View();
    //}
}
