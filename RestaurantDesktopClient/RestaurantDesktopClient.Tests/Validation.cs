using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.DataTransferObjects;
using RestaurantDesktopClient.Helpers;

namespace RestaurantDesktopClient.Tests
{
    /// <summary>
    /// Summary description for Validation
    /// </summary>
    ///
    [TestClass]
    public class Validation
    {
        private ReservationDTO GetReservation()
        {
            return new ReservationDTO
            {
                Id = 1,
                Tables = new List<TablesDTO>()
        {
            new TablesDTO {Id = 1, NoOfSeats = 3, TableNumber = 1},
            new TablesDTO {Id = 2, NoOfSeats = 3, TableNumber = 2},
        },
                ReservationTime = DateTime.Now.AddMinutes(15),
                Customer = new CustomerDTO(),
                NoOfPeople = 2,
                Deposit = true,
                ReservationDate = DateTime.Now,
            };
        }

        [TestMethod]
        public void IsNumberSuccessOneNumber()
        {
            var result = Helpers.Validation.IsNumber("1", false);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void IsNumberSuccessMultiNumbers()
        {
            var result = Helpers.Validation.IsNumber("654544532", false);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void IsNumberFailStartLetter()
        {
            var result = Helpers.Validation.IsNumber("s2", false);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void IsNumberFailEndLetter()
        {
            var result = Helpers.Validation.IsNumber("2s", false);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void IsNumberFailLetterInMiddel()
        {
            var result = Helpers.Validation.IsNumber("3s2", false);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void IsNumberFailLetterOnly()
        {
            var result = Helpers.Validation.IsNumber("test", false);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void IsNumberFailWithDotr()
        {
            var result = Helpers.Validation.IsNumber("3.2", false);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void IsNumberFailWithCome()
        {
            var result = Helpers.Validation.IsNumber("3,2", false);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void IsNumberFailWithSpecialChar()
        {
            var result = Helpers.Validation.IsNumber("3!2", false);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void IsUpcomingDateTimeSuccessDateTimeNow()
        {
            var testTime = DateTime.Now;
            var d = TimeSpan.FromMinutes(15);
            var res = new DateTime((testTime.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, testTime.Kind);
            var result = Helpers.Validation.IsUpcomingDateTime(res, false);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void IsUpcomingDateTimeFailDateTimeNowMinusSixteenMin()
        {
            var testTime = DateTime.Now.AddMinutes(-16);
            var d = TimeSpan.FromMinutes(15);
            var res = new DateTime((testTime.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, testTime.Kind);
            var result = Helpers.Validation.IsUpcomingDateTime(res, false);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void IsUpcomingDateTimeSuccessDateTimeNowPlusSixteenMin()
        {
            var testTime = DateTime.Now.AddMinutes(16);
            var d = TimeSpan.FromMinutes(15);
            var res = new DateTime((testTime.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, testTime.Kind);
            var result = Helpers.Validation.IsUpcomingDateTime(res, false);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void IsUpcomingDateTimeFailDateTimeNowMinusOneYear()
        {
            var testTime = DateTime.Now.AddYears(-1);
            var d = TimeSpan.FromMinutes(15);
            var res = new DateTime((testTime.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, testTime.Kind);
            var result = Helpers.Validation.IsUpcomingDateTime(res, false);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void IsUpcomingDateTimeSuccessDateTimeNowplusOneYear()
        {
            var testTime = DateTime.Now.AddYears(1);
            var d = TimeSpan.FromMinutes(15);
            var res = new DateTime((testTime.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, testTime.Kind);
            var result = Helpers.Validation.IsUpcomingDateTime(res, false);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void ReservationValidForCreateSeccessFullObject()
        {
            var reservation = GetReservation();
            reservation.Id = 0;
            var result = Helpers.Validation.ReservationValidForCreate(reservation, false);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void ReservationValidForCreateFailMissingTables()
        {
            var reservation = GetReservation();
            reservation.Tables = new List<TablesDTO>();
            var result = Helpers.Validation.ReservationValidForCreate(reservation, false);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void ReservationValidForCreateFailMissingCustomer()
        {
            var reservation = GetReservation();
            reservation.Customer = null;
            var result = Helpers.Validation.ReservationValidForCreate(reservation, false);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void ReservationValidForCreateFailMissingNoOfPeople()
        {
            var reservation = GetReservation();
            reservation.NoOfPeople = 0;
            var result = Helpers.Validation.ReservationValidForCreate(reservation, false);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void ReservationValidForUpdateSucess()
        {
            var reservation = GetReservation();
            var result = Helpers.Validation.ReservationValidForUpdate(reservation, false);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void ReservationValidForUpdateFailMissingId()
        {
            var reservation = GetReservation();
            reservation.Id = 0;
            var result = Helpers.Validation.ReservationValidForUpdate(reservation, false);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void ReservationValidForUpdateFailMissingTables()
        {
            var reservation = GetReservation();
            reservation.Tables.Clear();
            var result = Helpers.Validation.ReservationValidForUpdate(reservation, false);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void ReservationValidForUpdateFailDateTimeBeforeNow()
        {
            var reservation = GetReservation();
            reservation.ReservationTime = reservation.ReservationTime.AddDays(-16);
            var result = Helpers.Validation.ReservationValidForUpdate(reservation, false);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void ReservationValidForUpdateFailMissingCustomer()
        {
            var reservation = GetReservation();
            reservation.Customer = null;
            var result = Helpers.Validation.ReservationValidForUpdate(reservation, false);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void ReservationValidForUpdateFailMissingNoOfPeople()
        {
            var reservation = GetReservation();
            reservation.NoOfPeople = 0;
            var result = Helpers.Validation.ReservationValidForUpdate(reservation, false);
            Assert.IsFalse(result);
        }
    }
}
