using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantWebApp.Model
{
    public class CustomViewModel
    {
        public IEnumerable<SelectListItem> listFood { get; set; }
        public IEnumerable<SelectListItem> listDrink { get; set; }
        public IEnumerable<SelectListItem> sum { get; set; }

    }
}