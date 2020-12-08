using DataAccess.DataTransferObjects;
using DataAccess.DataTransferObjects.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.Repositories
{
    public class TableRepository : IRepository<RestaurantTablesDTO>
    {
        private readonly RestaurantContext _context;

        public TableRepository(RestaurantContext context)
        {
            _context = context;
        }

        public RestaurantTablesDTO Create(RestaurantTablesDTO obj, bool transactionEndpoint = true)
        {
            throw new NotImplementedException();
        }

        internal EntityEntry<RestaurantTables> CreateTable(RestaurantTablesDTO obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(RestaurantTablesDTO obj, bool transactionEndpoint = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RestaurantTablesDTO> GetAll()
        {
            IEnumerable<RestaurantTablesDTO> res = null;
            var restaurantTables = _context.RestaurantTables.Include(rt => rt.ReservationsTables).AsNoTracking().ToList();
            if (restaurantTables != null)
            {
                res = Converter.Convert(restaurantTables);
            }

            return res;
        }

        public RestaurantTablesDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RestaurantTablesDTO> GetCountWithOffsetByOrdering(int count, int offset, string ordering)
        {
            throw new NotImplementedException();
        }

        public RestaurantTablesDTO Update(RestaurantTablesDTO obj, bool transactionEndpoint = true)
        {
            throw new NotImplementedException();
        }

        public List<RestaurantTablesDTO> GetReservationTimeByDateAndTime(DateTime dateTime)
        {
            var timeSlots = new AvailableTimesDTO() { AvailabilityDate = dateTime.Date, TableOpenings = new List<AvailableTimesDTO.TableTimes>() };
            var startTime = new TimeSpan(12, 00, 00);
            var endTime = new TimeSpan(22, 00, 00);

            var startDateTime = dateTime;
            var endDateTime = dateTime.AddHours(1).AddMinutes(30);

            var all = _context.Reservation
                .Include(r => r.ReservationsTables)
                .ThenInclude(r => r.RestaurantTables)
                .Where(r =>
                    r.ReservationTime >= endDateTime &&
                    r.ReservationTime <= startDateTime).AsNoTracking();
            var TEST = _context.RestaurantTables


            var tables = new TableRepository(_context).GetAll();

            var availabilityList = new List<AvailableTimesDTO.TableTimes>();
            var availabilityList = new List<RestaurantTablesDTO.TableTimes>();
            foreach (var table in tables)
            {
                var tableTime = new AvailableTimesDTO.TableTimes() { Table = table };
                var timesAvailable = new List<AvailableTimesDTO.TableTimes.TimePair>();
                var times = all.Where(r => r.ReservationsTables.Any(t => t.RestaurantTablesId == table.Id))
                    .Select(r => r.ReservationTime).OrderBy(t => t.TimeOfDay);
                var lastTime = startDateTime;
                foreach (var time in times)
                {
                    timesAvailable.Add(new AvailableTimesDTO.TableTimes.TimePair() { Start = lastTime, End = time });
                    lastTime = time.AddMinutes(90);
                }

                if (lastTime.AddMinutes(90) <= endDateTime)
                    timesAvailable.Add(
                        new AvailableTimesDTO.TableTimes.TimePair() { Start = lastTime, End = endDateTime });
                tableTime.Openings = timesAvailable;
                availabilityList.Add(tableTime);
            }

            timeSlots.TableOpenings = availabilityList;

            return timeSlots;
        }

    }
}