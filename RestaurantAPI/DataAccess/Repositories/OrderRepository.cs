using DataAccess.DataTransferObjects;
using DataAccess.DataTransferObjects.Converters;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
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
                if (RestaurantOrder.Validate(obj))
                {
                    RestaurantOrder order = null;
                    var notNewOrder = _context.RestaurantOrder.FirstOrDefault(o => o.ReservationId == obj.ReservationID);
                    if (notNewOrder != null)
                    {
                       var orderDto = Update(obj, false);
                       order = Converter.Convert(orderDto);

                    }
                    else
                    {
                        order = Converter.Convert(obj);
                        order.OrderDate = DateTime.Now;
                        order.Employee = _context.Employee.FirstOrDefault(e => e.Id.Equals(obj.EmployeeID));
                        order.Reservation = _context.Reservation.FirstOrDefault(r => r.Id.Equals(obj.ReservationID));
                        var tempPaymentCondition = _context.PaymentCondition.FirstOrDefault(x => x.Condition.Equals(obj.PaymentCondition)).Id;
                        if (tempPaymentCondition > 0)
                        {
                            order.PaymentConditionId = tempPaymentCondition;
                            order.PaymentCondition =
                                _context.PaymentCondition.FirstOrDefault(pc => pc.Id.Equals(order.PaymentConditionId));
                        }

                        var added = _context.RestaurantOrder.Add(order);
                        _context.SaveChanges();
                    }


                    if (transactionEndpoint) _context.Database.CommitTransaction();
                    _context.Entry(order).GetDatabaseValues();
                    return GetById(order.OrderNo);
                }
            }
            catch (Exception)
            {
                _context.Database.RollbackTransaction();
                throw;
            }

            return null;
        }

        public bool Delete(int id, bool transactionEndpoint = true)
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

        //Denne her er kun til at update PaymentCondition og  employee
        //TODO add so it handles change in food (update food).
        public OrderDTO Update(OrderDTO obj, bool transactionEndpoint = true)
        {
            if (!RestaurantOrder.Validate(obj))
            {
                throw new ValidationException("Bad input");
            }
            if (transactionEndpoint) _context.Database.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                var tempOrder = Converter.Convert(obj);

                var order = _context.RestaurantOrder
                    .Where(o => o.OrderNo == obj.OrderNo)
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

                order.EmployeeId = tempOrder.EmployeeId;

                order.PaymentConditionId = tempOrder.PaymentConditionId;
                _context.Update(order);

                if (transactionEndpoint)
                {
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();
                    return GetById(order.OrderNo);
                }
            }
            catch (Exception)
            {
                _context.Database.RollbackTransaction();
                throw;
            }

            return null;
        }

        //this for when you need to cancel a reservation that has become an order  and you needs to get table free
        //and delete all the food and make order canceled
        //this is being use when a waiter from desktop cancel a reservation with a order
        internal bool cancelOrder(int orderNo, bool transactionEndpoint = true)
        {
            try
            {
                var order = _context.RestaurantOrder.FirstOrDefault(o => o.OrderNo == orderNo);
                // delete orderlines
                _context.OrderLine.RemoveRange
                    (_context.OrderLine.Where(r => r.OrderNumber == orderNo));

                //TODO Hard code to  (Annulleret) this maybe need to change in the future
                order.PaymentConditionId = 5;

                _context.Update(order);
                _context.Entry(order).State = EntityState.Modified;
                if (transactionEndpoint)
                {
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                _context.Database.RollbackTransaction();
                throw;
            }

            return false;
        }
    }
}