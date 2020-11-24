using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    public class FoodDTO
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FoodCategory { get; set; }
        public object Price { get; internal set; }
    }
}
