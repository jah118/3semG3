using Autofac.Extras.Moq;
using DataAccess;
using DataAccess.DataTransferObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DataAccess
{
    [TestClass]
   public class EmployeeRepositoryTest
    {

        [TestMethod]
        public void GetEmployeeFromDatababaseById()
        {

            using(var mock = AutoMock.GetLoose())
            {
                var employee = new EmployeeDTO
                {
                    Id = 12,
                    Title =  "Tjener",
                    Phone = "88775522",
                    Email = "ABC@BCA.dk",
                    FirstName = "Anders",
                    LastName = "Svendsen",
                    Address = "Svendsensvej 22",
                    ZipCode = "9000",
                    City = "Aalborg"

                };

                var repository = new Mock<IRepository<EmployeeDTO>>();

                repository.Setup(x => x.GetById(It.IsAny<int>())).Returns(employee);

                var e = repository.Object.GetById(12);


                Assert.IsNotNull(e);
                Assert.AreEqual(employee.Id, e.Id);
            }
        }
    }
}
