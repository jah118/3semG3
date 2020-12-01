using DataAccess.DataTransferObjects;
using DataAccess.DataTransferObjects.Converters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class FoodRepository : IRepository<FoodDTO>
    {
        private readonly RestaurantContext _context;

        public FoodRepository(RestaurantContext context)
        {
            _context = context;
        }

        public FoodDTO Create(FoodDTO obj, bool transactionEndpoint = true)
        {
            throw new NotImplementedException();
        }

        public bool Delete(FoodDTO obj, bool transactionEndpoint = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FoodDTO> GetAll()
        {
            IEnumerable<FoodDTO> res = null;

            var food = _context.Food
               .Include(f => f.Price)
               .Include(f => f.FoodCategory)
               .ToList();

            if (food != null)
            {
                res = Converter.Convert(food);
            }
            return res;

        }

        public FoodDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FoodDTO> GetCountWithOffsetByOrdering(int count, int offset, string ordering)
        {
            throw new NotImplementedException();
        }

        public FoodDTO Update(FoodDTO obj, bool transactionEndpoint = true)
        {
            throw new NotImplementedException();
        }
    }
}
