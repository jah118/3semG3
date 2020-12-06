using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.Models;

namespace DataAccess.DataTransferObjects.Converters
{
    public partial class Converter
    {
        public static Price Convert(PriceDTO obj)
        {
            return new Price()
            {
                Id = obj.Id,
                PriceValue = obj.PriceValue,
            };

        }

        public static PriceDTO Convert(Price obj)
        {
            return new PriceDTO()
            {
                Id = obj.Id,
                PriceValue = obj.PriceValue,
            };
        }

        public static ICollection<Price> Convert(ICollection<PriceDTO> obj)
        {
            var list = new List<Price>();
            foreach (var p in obj)
            {
                list.Add(Convert(p));
            }
            return list;
        }
        public static ICollection<PriceDTO> Convert(ICollection<Price> obj)
        {
            var list = new List<PriceDTO>();
            foreach (var p in obj)
            {
                list.Add(Convert(p));
            }
            return list;
        }

    }
}
