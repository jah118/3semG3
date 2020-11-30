using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantWebApp.Util;
using System;

namespace RestaurantWebApp.Test.Util
{
    [TestClass]
    public class FormatTimeTests
    {
        [TestMethod]
        public void FormatterForReservationTimeTestValidInput()
        {
            //Arrange
            const string date = "2020-11-27";
            const string timeStamp = "27-11-2020 19:30:00";

            //Act
            var dateTime = FormatTime.FormatterForReservationTimeFromString(date, timeStamp);

            //Assert
            Assert.IsNotNull(dateTime);
            Assert.AreEqual(new DateTime(2020, 11, 27, 19, 30, 00), dateTime);
        }
        [TestMethod]
        public void FormatterForReservationTimeTestValidInputWithTime()
        {
            //Arrange
            const string date = "2020-11-27 09:30:00";
            const string timeStamp = "27-11-2020 19:30:00";

            //Act
            var dateTime = FormatTime.FormatterForReservationTimeFromString(date, timeStamp);

            //Assert
            Assert.IsNotNull(dateTime);
            Assert.AreEqual(new DateTime(2020, 11, 27, 19, 30, 00), dateTime);
        }

        [TestMethod]
        public void FormatterForReservationTimeTestValidDate()
        {
            //Arrange
            const string date = "";
            const string timeStamp = "27-11-2020 19:30:00";

            //Act
            var dateTime = FormatTime.FormatterForReservationTimeFromString(date, timeStamp);

            //Assert
            Assert.IsNotNull(dateTime);
            Assert.AreEqual(new DateTime(2020, 11, 27, 19, 30, 00), dateTime);
        }

    }
}