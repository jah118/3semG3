using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataAccess.DataTransferObjects;
using DataAccess.DataTransferObjects.Converters;
using Microsoft.EntityFrameworkCore;

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
            //Sanity check here, ensure unique tables etc
            if (transactionEndpoint) _context.Database.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                var order = Converter.Convert(obj);
                order.Employee = _context.Employee.FirstOrDefault(e => e.Id.Equals(obj.EmployeeID));
                order.Reservation = _context.Reservation.FirstOrDefault(r => r.Id.Equals(obj.ReservationID));
                order.PaymentConditionId = _context.PaymentCondition
                    .Where(x => x.Condition.Equals(obj.PaymentCondition)).FirstOrDefault().Id;
                order.PaymentCondition =
                    _context.PaymentCondition.FirstOrDefault(pc => pc.Id.Equals(order.PaymentConditionId));

                var added = _context.RestaurantOrder.Add(order);
                _context.SaveChanges();
                if (transactionEndpoint) _context.Database.CommitTransaction();
                _context.Entry(order).GetDatabaseValues();
                return GetById(order.OrderNo);
            }
            catch (Exception)
            {
                _context.Database.RollbackTransaction();
                throw;
            }

            return null;
        }

        public bool Delete(OrderDTO obj, bool transactionEndpoint = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDTO> GetAll()
        {
            IEnumerable<OrderDTO> res = null;

            var orders = _context.RestaurantOrder
                .Include(f => f.OrderLine)
                .ThenInclude(f => f.Food)
                .ThenInclude(f => f.Price)
                .Include(f => f.OrderLine)
                .ThenInclude(f => f.Food)
                .ThenInclude(f => f.FoodCategory)
                .Include(e => e.Employee)
                .ThenInclude(e => e.Person)
                .ThenInclude(e => e.Location)
                .ThenInclude(e => e.ZipCodeNavigation)
                .Include(e => e.Employee.Title)
                .Include(r => r.Reservation)
                .Include(pc => pc.PaymentCondition).ToList();


            if (orders != null) res = Converter.Convert(orders);
            return res;
        }

        public OrderDTO GetById(int id)
        {
            OrderDTO res = null;

            var order = _context.RestaurantOrder
                .Where(o => o.OrderNo == id)
                .Include(f => f.OrderLine)
                .ThenInclude(f => f.Food)
                .ThenInclude(f => f.Price)
                .Include(f => f.OrderLine)
                .ThenInclude(f => f.Food.FoodCategory)
                .Include(e => e.Employee)
                .ThenInclude(e => e.Person)
                .ThenInclude(e => e.Location)
                .ThenInclude(e => e.ZipCodeNavigation)
                .Include(e => e.Employee.Title)
                .Include(r => r.Reservation)
                .Include(pc => pc.PaymentCondition).OrderBy(x => x.OrderDate).FirstOrDefault();
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