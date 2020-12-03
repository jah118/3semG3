using System;
using System.Collections.Generic;
using DataAccess.DataTransferObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantDesktopClient.Reservation;

namespace RestaurantDesktopClient.Tests
{
    [TestClass]
    public class ReservationService
    {
        //[TestMethod, TestCategory("Integration")]
        //public void GetAllReservationPositiv()
        //{
        //    IReservationRepository _reservationRepository = new ReservationRepository();
        //    List<ReservationDTO> _result = _reservationRepository.GetAllReservations();

        //    Assert.IsNotNull(_result);
        //}
        //[TestMethod, TestCategory("Integration")]
        //public void GetReservationByIdPositiv()
        //{
        //    int id = 1;
        //    IReservationRepository _reservationRepository = new ReservationRepository();
        //    ReservationDTO _result = _reservationRepository.GetReservation(id);
        //    Assert.IsTrue(_result.Id == id);
        //}
        //[TestMethod, TestCategory("Integration")]
        //public void GetReservationByIdNegativ()
        //{
        //    int id = 1;
        //    IReservationRepository _reservationRepository = new ReservationRepository();
        //    ReservationDTO _result = _reservationRepository.GetReservation(id);
        //    Assert.IsFalse(_result.Id != id);
        //}
        //[TestMethod, TestCategory("Integration")]
        //public void PostReservation()
        //{
        //    ReservationDTO _reservation = new ReservationDTO
        //    {
        //        Customer = new CustomerDTO
        //        {
        //            Id = 99,
        //            Address = "TestAddress 99",
        //            City = "TestCity",
        //            Email = "TestEmail@Test.com",
        //            FirstName = "TestFirstName",
        //            LastName = "TestLastName",
        //            Phone = "12345678",
        //            Reservation = new List<ReservationDTO>(),
        //            ZipCode = "1234"
        //        },
        //        Deposit = true,
        //        NoOfPeople = 4,
        //        Note = "Some Test Note",
        //        ReservationDate = DateTime.Now,
        //        ReservationTime = DateTime.Now.AddDays(1),
        //        Tables = new List<TablesDTO> { new TablesDTO { NoOfSeats = 4, TableNumber = 2 }, new TablesDTO { NoOfSeats = 4, TableNumber = 3 } },
        //    };
        //    IReservationRepository _reservationRepository = new ReservationRepository();
        //    ReservationDTO res = _reservationRepository.CreateReservation(_reservation);

        //    Assert.IsNotNull(res);
        //}
        //[TestMethod]
        //public void PostReservationPositiv()
        //{

        //}
    }
}
