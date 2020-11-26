using DataAccess.DataTransferObjects;
using DataAccess.DataTransferObjects.Converters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class OrderRepository : IRepository<OrderDTO>
    {
        private readonly RestaurantContext _context;
        public OrderRepository(RestaurantContext context)
        {
            _context = context;
        }
        public OrderDTO Create(OrderDTO obj, bool transactionEndpoint = true)
        {
            throw new NotImplementedException();
        }

        public bool Delete(OrderDTO obj, bool transactionEndpoint = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDTO> GetAll()
        {
            IEnumerable<OrderDTO> res = null;

            var orders = _context.Order
                .Include(e => e.Employee)
                    .ThenInclude(e => e.Person)
                        .ThenInclude(e => e.Location)
                            .ThenInclude(e => e.ZipCodeNavigation)
                    .Include(e=>e.Employee.Title)
                .Include(r => r.Reservation)
                .Include(pc => pc.PaymentCondition).ToList();
            if (orders != null) res = Converter.Convert(orders);
            return res;

        }

        public OrderDTO GetById(int id)
        {
            OrderDTO res = null;

            var order = _context.Order
                .Where(o=>o.OrderNo == id)
                .Include(e => e.Employee)
                    .ThenInclude(e => e.Person)
                        .ThenInclude(e => e.Location)
                            .ThenInclude(e => e.ZipCodeNavigation)
                    .Include(e => e.Employee.Title)
                .Include(r => r.Reservation)
                .Include(pc => pc.PaymentCondition).FirstOrDefault();
            if (order != null) res = Converter.Convert(order);
            return res;
        }

        public IEnumerable<OrderDTO> GetCountWithOffsetByOrdering(int count, int offset, string ordering)
        {
            throw new NotImplementedException();
        }

        public OrderDTO Update(OrderDTO obj, bool transactionEndpoint = true)
        {
            throw new NotImplementedException();
        }
    }
}
