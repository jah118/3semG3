using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.DataTransferObjects;
using DataAccess.DataTransferObjects.Converters;
using DataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataAccess.DataTransferObjects.Converters
{
    [TestClass]
    [TestCategory("Converters")]
    public class ConversionTest
    {
        #region OrderConversion

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Converters")]
        public void OrderConversionOrderListToOrderDTOList()
        {
            //Arrange
            var orders = GetRestaurantOrder();
            //Act
            var orderDTOs = Converter.Convert(orders);
            //Assert
            Assert.AreEqual(orderDTOs.Count(), orders.Count);
            for (var i = 0; i < orders.Count; i++)
            {
                Assert.AreEqual(orders[i].OrderNo, orderDTOs.ElementAt(i).OrderNo);
                Assert.AreEqual(orders[i].PaymentCondition.Condition, orderDTOs.ElementAt(i).PaymentCondition);
                Assert.AreEqual(orders[i].EmployeeId, orderDTOs.ElementAt(i).EmployeeID);
                Assert.AreEqual(orders[i].OrderDate, orderDTOs.ElementAt(i).OrderDate);
                Assert.AreEqual(orders[i].ReservationId, orderDTOs.ElementAt(i).ReservationID);
                orders[i].OrderLine.ToList().ForEach(x =>
                {
                    Assert.IsNotNull(orderDTOs.ElementAt(i).OrderLines
                        .Where(o => o.Food.Id == x.Food.Id && o.Quantity == x.Quantity).FirstOrDefault());
                });
            }
        }

        #endregion

        #region TestDataMethods

        private List<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Person = new Person
                    {
                        Phone = "12345678",
                        Email = "Person1@Test.com",
                        FirstName = "Person1",
                        LastName = "TestPerson1",
                        Location = new Location
                        {
                            Address = "Address for TestPerson1",
                            ZipCodeNavigation = new ZipId
                            {
                                ZipCode = "1000",
                                City = "TestCity"
                            }
                        }
                    }
                },
                new Customer
                {
                    Id = 2,
                    Person = new Person
                    {
                        Phone = "87654321",
                        Email = "Person2@Test.com",
                        FirstName = "Person2",
                        LastName = "TestPerson2",
                        Location = new Location
                        {
                            Address = "Address for TestPerson2",
                            ZipCodeNavigation = new ZipId
                            {
                                ZipCode = "2000",
                                City = "TestCity2"
                            }
                        }
                    }
                }
            };
        }

        private List<CustomerDTO> GetCustomerDTOs()
        {
            return new List<CustomerDTO>
            {
                new CustomerDTO
                {
                    Id = 1,
                    Phone = "12345678",
                    Email = "Person1@Test.com",
                    FirstName = "Person1",
                    LastName = "TestPerson1",
                    Address = "Address for TestPerson1",
                    ZipCode = "1000",
                    City = "TestCity"
                },
                new CustomerDTO
                {
                    Id = 2,
                    Phone = "87654321",
                    Email = "Person2@Test.com",
                    FirstName = "Person2",
                    LastName = "TestPerson2",
                    Address = "Address for TestPerson2",
                    ZipCode = "2000",
                    City = "TestCity2"
                }
            };
        }

        private List<EmployeeDTO> GetEmployeeDTOs()
        {
            return new List<EmployeeDTO>
            {
                new EmployeeDTO
                {
                    Id = 1,
                    Title = "Medarbejder",
                    Phone = "12345678",
                    Email = "Person1@Test.com",
                    FirstName = "Person1",
                    LastName = "TestPerson1",
                    Address = "Address for TestPerson1",
                    ZipCode = "1000",
                    City = "TestCity"
                },
                new EmployeeDTO
                {
                    Id = 2,
                    Title = "TestMedarbejder",
                    Phone = "87654321",
                    Email = "Person2@Test.com",
                    FirstName = "Person2",
                    LastName = "TestPerson2",
                    Address = "Address for TestPerson2",
                    ZipCode = "2000",
                    City = "TestCity2"
                }
            };
        }

        private List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Salary = 200,
                    Title = new EmployeeTitle {Title = "Medarbejder"},
                    Person = new Person
                    {
                        Phone = "12345678",
                        Email = "Person1@Test.com",
                        FirstName = "Person1",
                        LastName = "TestPerson1",
                        Location = new Location
                        {
                            Address = "Address for TestPerson1",
                            ZipCodeNavigation = new ZipId
                            {
                                ZipCode = "1000",
                                City = "TestCity"
                            }
                        }
                    }
                },
                new Employee
                {
                    Id = 2,
                    Salary = 100,
                    Title = new EmployeeTitle {Title = "TestTitle"},
                    Person = new Person
                    {
                        Phone = "87654321",
                        Email = "Person2@Test.com",
                        FirstName = "Person2",
                        LastName = "TestPerson2",
                        Location = new Location
                        {
                            Address = "Address for TestPerson2",
                            ZipCodeNavigation = new ZipId
                            {
                                ZipCode = "2000",
                                City = "TestCity2"
                            }
                        }
                    }
                }
            };
        }

        private List<FoodCategory> GetFoodCategories()
        {
            return new List<FoodCategory>
            {
                new FoodCategory
                {
                    Id = 1,
                    Name = "Mad"
                },
                new FoodCategory
                {
                    Id = 2,
                    Name = "Drikkevare"
                }
            };
        }

        private List<FoodCategoryDTO> GetFoodCategoriesDTO()
        {
            return new List<FoodCategoryDTO>
            {
                new FoodCategoryDTO
                {
                    Id = 1,
                    Name = "Mad"
                },
                new FoodCategoryDTO
                {
                    Id = 2,
                    Name = "Drikkevare"
                }
            };
        }

        private List<RestaurantTablesDTO> GetRestaurantTablesDTO()
        {
            return new List<RestaurantTablesDTO>
            {
                new RestaurantTablesDTO
                {
                    NoOfSeats = 3,
                    TableNumber = 1,
                    Id = 2
                },
                new RestaurantTablesDTO
                {
                    NoOfSeats = 4,
                    TableNumber = 2,
                    Id = 3
                }
            };
        }

        private List<RestaurantTables> GetRestaurantTables()
        {
            return new List<RestaurantTables>
            {
                new RestaurantTables
                {
                    NoOfSeats = 3,
                    TableNumber = 1,
                    Id = 2
                },
                new RestaurantTables
                {
                    NoOfSeats = 4,
                    TableNumber = 2,
                    Id = 3
                }
            };
        }

        private List<PriceDTO> GetPriceDTO()
        {
            return new List<PriceDTO>
            {
                new PriceDTO
                {
                    Id = 1,
                    PriceValue = 218
                },
                new PriceDTO
                {
                    Id = 2,
                    PriceValue = 202
                }
            };
        }

        private List<Price> GetPrice()
        {
            return new List<Price>
            {
                new Price
                {
                    Id = 1,
                    PriceValue = 218,
                    FoodId = 1,
                },
                new Price
                {
                    Id = 2,
                    PriceValue = 202,
                    FoodId = 2,
                    //Food = new Food
                    //{
                    //    Description = "Some Test Description, for FoodDTO2",
                    //    FoodCategory = GetFoodCategory()[1],
                    //    Id = 2,
                    //    Name = "Oil - Food, Lacquer Spray"
                    //}
                }
            };
        }

        private List<FoodDTO> GetFoodDTO()
        {
            return new List<FoodDTO>
            {
                new FoodDTO
                {
                    Description = "Some Test Description, for FoodDTO1",
                    FoodCategoryName = "Mad",
                    Id = 1,
                    Name = "Oats Large Flake",
                    Price = 218
                },
                new FoodDTO
                {
                    Description = "Some Test Description, for FoodDTO2",
                    FoodCategoryName = "Drikkevare",
                    Id = 2,
                    Name = "Oil - Food, Lacquer Spray",
                    Price = 202
                }
            };
        }

        private List<Food> GetFood()
        {
            return new List<Food>
            {
                new Food
                {
                    Id = 1,
                    Name = "Oats Large Flake",
                    Description = "Some Test Description, for FoodDTO1",
                    FoodCategory = GetFoodCategory()[0],
                    Price = new List<Price> {GetPrice()[0]}
                },
                new Food
                {
                    Id = 2,
                    Name = "Oil - Food, Lacquer Spray",
                    Description = "Some Test Description, for FoodDTO2",
                    FoodCategory = GetFoodCategory()[1],
                    Price = new List<Price> {GetPrice()[1]}
                }
            };
        }

        private List<FoodCategory> GetFoodCategory()
        {
            return new List<FoodCategory>
            {
                new FoodCategory
                {
                    Id = 1,
                    Name = "Mad"
                },
                new FoodCategory
                {
                    Id = 2,
                    Name = "Drikkevare"
                }
            };
        }

        private List<RestaurantOrder> GetRestaurantOrder()
        {
            return new List<RestaurantOrder>
            {
                new RestaurantOrder
                {
                    EmployeeId = 1,
                    OrderDate = DateTime.Now,
                    OrderNo = 1,
                    PaymentCondition = new PaymentCondition {Condition = "Betalt", Id = 4},
                    ReservationId = 1,
                    Reservation = new Reservation {Id = 1},
                    OrderLine = new List<OrderLine>
                    {
                        new OrderLine
                        {
                            Food = GetFood()[0],
                            Quantity = 1
                        },
                        new OrderLine
                        {
                            Food = GetFood()[1],
                            Quantity = 2
                        }
                    }
                },
                new RestaurantOrder
                {
                    EmployeeId = 2,
                    OrderDate = DateTime.Now.AddDays(1),
                    OrderNo = 2,
                    PaymentCondition = new PaymentCondition {Condition = "Leveret", Id = 3},
                    ReservationId = 2,
                    Reservation = new Reservation {Id = 2},
                    OrderLine = new List<OrderLine>
                    {
                        new OrderLine
                        {
                            Food = GetFood()[1],
                            Quantity = 2
                        },
                        new OrderLine
                        {
                            Food = GetFood()[0],
                            Quantity = 3
                        }
                    }
                }
            };
        }

        private List<ReservationDTO> GetReservationDTOs()
        {
            return new List<ReservationDTO>
            {
                new ReservationDTO
                {
                    Id = 1,
                    Note = "Some note for reservationDTO 1",
                    Deposit = true,
                    NoOfPeople = 3,
                    ReservationDate = DateTime.Now.AddHours(5),
                    ReservationTime = DateTime.Now.AddDays(2),
                    Customer = new CustomerDTO {Id = 1}
                },
                new ReservationDTO
                {
                    Id = 2,
                    Note = "Some note for reservationDTO 2",
                    Deposit = false,
                    NoOfPeople = 6,
                    ReservationDate = DateTime.Now,
                    ReservationTime = DateTime.Now.AddDays(2),
                    Customer = new CustomerDTO {Id = 22}
                }
            };
        }

        private List<Reservation> GetReservations()
        {
            return new List<Reservation>
            {
                new Reservation
                {
                    Deposit = true,
                    Id = 1,
                    NoOfPeople = 4,
                    ReservationDate = DateTime.Now,
                    ReservationTime = DateTime.Now.AddDays(2),
                    Note = "Some note for reservation1",
                    Customer = GetCustomers()[0],

        },
                new Reservation
                {
                    Deposit = false,
                    Id = 2,
                    NoOfPeople = 3,
                    ReservationDate = DateTime.Now.AddDays(1),
                    ReservationTime = DateTime.Now.AddDays(2),
                    Note = "Some note for reservation2",
                    Customer = GetCustomers()[1],
                }
            };
        }

        #endregion

        #region CustomerConversion

        [TestMethod]
        [TestCategory("Unit")]
        [TestCategory("Converters")]
        public void CustomerConversionCustomerDTOToCustomer()
        {
            //Arrange
            var customerDTO = GetCustomerDTOs()[0];
            //Act
            var customer = Converter.Convert(customerDTO);
            //Assert
            Assert.AreEqual(customer.Id, customerDTO.Id);
            Assert.AreEqual(customer.Person.Phone, customerDTO.Phone);
            Assert.AreEqual(customer.Person.Email, customerDTO.Email);
            Assert.AreEqual(customer.Person.FirstName, customerDTO.FirstName);
            Assert.AreEqual(customer.Person.LastName, customerDTO.LastName);
            Assert.AreEqual(customer.Person.Location.Address, customerDTO.Address);
            Assert.AreEqual(customer.Person.Location.ZipCodeNavigation.ZipCode, customerDTO.ZipCode);
            Assert.AreEqual(customer.Person.Location.ZipCodeNavigation.City, customerDTO.City);
        }

        [TestMethod]
        [TestCategory("Unit")]
        [TestCategory("Converters")]
        public void CustomerConversionCustomerToCustomerDTO()
        {
            //Arrange
            var customer = GetCustomers()[0];
            //Act
            var customerDTO = Converter.Convert(customer);
            //Assert
            Assert.AreEqual(customer.Id, customerDTO.Id);
            Assert.AreEqual(customer.Person.Phone, customerDTO.Phone);
            Assert.AreEqual(customer.Person.Email, customerDTO.Email);
            Assert.AreEqual(customer.Person.FirstName, customerDTO.FirstName);
            Assert.AreEqual(customer.Person.LastName, customerDTO.LastName);
            Assert.AreEqual(customer.Person.Location.Address, customerDTO.Address);
            Assert.AreEqual(customer.Person.Location.ZipCodeNavigation.ZipCode, customerDTO.ZipCode);
            Assert.AreEqual(customer.Person.Location.ZipCodeNavigation.City, customerDTO.City);
        }

        #endregion

        #region EmployeeConversion

        [TestMethod]
        [TestCategory("Unit")]
        [TestCategory("Converters")]
        public void EmployeeConversionEmployeeDTOToEmployee()
        {
            //Arrange
            var employeeDTO = GetEmployeeDTOs()[0];
            //Act
            var employee = Converter.Convert(employeeDTO);
            //Assert
            Assert.AreEqual(employee.Id, employeeDTO.Id);
            Assert.AreEqual(employee.Title.Title, employeeDTO.Title);
            Assert.AreEqual(employee.Person.Phone, employeeDTO.Phone);
            Assert.AreEqual(employee.Person.Email, employeeDTO.Email);
            Assert.AreEqual(employee.Person.FirstName, employeeDTO.FirstName);
            Assert.AreEqual(employee.Person.LastName, employeeDTO.LastName);
            Assert.AreEqual(employee.Person.Location.Address, employeeDTO.Address);
            Assert.AreEqual(employee.Person.Location.ZipCode, employeeDTO.ZipCode);
        }

        [TestMethod]
        [TestCategory("Unit")]
        [TestCategory("Converters")]
        public void EmployeeConversionEmployeeToEmployeeDTO()
        {
            //Arrange
            var employee = GetEmployees()[0];
            //Act
            var employeeDTO = Converter.Convert(employee);
            //Assert
            Assert.AreEqual(employee.Id, employeeDTO.Id);
            Assert.AreEqual(employee.Title.Title, employeeDTO.Title);
            Assert.AreEqual(employee.Person.Phone, employeeDTO.Phone);
            Assert.AreEqual(employee.Person.Email, employeeDTO.Email);
            Assert.AreEqual(employee.Person.FirstName, employeeDTO.FirstName);
            Assert.AreEqual(employee.Person.LastName, employeeDTO.LastName);
            Assert.AreEqual(employee.Person.Location.Address, employeeDTO.Address);
            Assert.AreEqual(employee.Person.Location.ZipCodeNavigation.ZipCode, employeeDTO.ZipCode);
            Assert.AreEqual(employee.Person.Location.ZipCodeNavigation.City, employeeDTO.City);
        }

        #endregion

        #region FoodCategoryConversion

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Converters")]
        public void FoodCategoryConversionFoodCategoryDTOToFoodCategory()
        {
            //Arrange
            var foodCategoriesDTO = GetFoodCategoriesDTO();
            //Act
            var foodCategorys = Converter.Convert(foodCategoriesDTO);
            //Assert
            Assert.AreEqual(foodCategoriesDTO.Count, foodCategorys.Count());
            for (var i = 0; i < foodCategoriesDTO.Count; i++)
            {
                Assert.AreEqual(foodCategoriesDTO[i].Id, foodCategorys.ElementAt(i).Id);
                Assert.AreEqual(foodCategoriesDTO[i].Name, foodCategorys.ElementAt(i).Name);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Converters")]
        public void FoodCategoryConversionFoodCategoryToFoodCategoryDTO()
        {
            //Arrange
            var foodCategories = GetFoodCategories();
            //Act
            var foodCategoryDTOs = Converter.Convert(foodCategories);
            //Assert
            Assert.AreEqual(foodCategories.Count, foodCategoryDTOs.Count());
            for (var i = 0; i < foodCategories.Count; i++)
            {
                Assert.AreEqual(foodCategories[i].Id, foodCategoryDTOs.ElementAt(i).Id);
                Assert.AreEqual(foodCategories[i].Name, foodCategoryDTOs.ElementAt(i).Name);
            }
        }

        #endregion

        #region RestaurantTablesConversion

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Converters")]
        public void RestaurantTablesConversionRestaurantTablesListToRestaurantTablesDTOList()
        {
            //Arrange
            var tables = GetRestaurantTables();
            //Act
            var tablesDTOs = Converter.Convert(tables);
            //Assert
            Assert.AreEqual(tables.Count, tablesDTOs.Count());
            for (var i = 0; i < tables.Count; i++)
            {
                Assert.AreEqual(tablesDTOs.ElementAt(i).Id, tables[i].Id);
                Assert.AreEqual(tablesDTOs.ElementAt(i).NoOfSeats, tables[i].NoOfSeats);
                Assert.AreEqual(tablesDTOs.ElementAt(i).TableNumber, tables[i].TableNumber);
            }
        }

        [TestMethod]
        [TestCategory("Unit")]
        [TestCategory("Converters")]
        public void RestaurantTablesConversionRestaurantTablesDTOToRestaurantTables()
        {
            //Arrange
            var tablesDTO = GetRestaurantTablesDTO()[0];
            //Act
            var tables = Converter.Convert(tablesDTO);
            //Assert

            Assert.AreEqual(tablesDTO.Id, tables.Id);
            Assert.AreEqual(tablesDTO.NoOfSeats, tables.NoOfSeats);
            Assert.AreEqual(tablesDTO.TableNumber, tables.TableNumber);
        }

        #endregion

        #region ReservationConversion

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Converters")]
        public void ReservationConversionReservationListToReservationDTOList()
        {
            //Arrange
            var reservations = GetReservations();
            //Act
            var reservationDTOs = Converter.Convert(reservations);
            //Assert
            Assert.AreEqual(reservations.Count, reservationDTOs.Count());
            for (var i = 0; i < reservations.Count; i++)
            {
                //Assert.AreEqual(reservations[i].Id, reservationDTOs.ElementAt(i).Id);
                Assert.AreEqual(reservations[i].Note, reservationDTOs.ElementAt(i).Note);
                Assert.AreEqual(reservations[i].Deposit, reservationDTOs.ElementAt(i).Deposit);
                Assert.AreEqual(reservations[i].NoOfPeople, reservationDTOs.ElementAt(i).NoOfPeople);
                Assert.AreEqual(reservations[i].ReservationDate, reservationDTOs.ElementAt(i).ReservationDate);
                Assert.AreEqual(reservations[i].ReservationTime, reservationDTOs.ElementAt(i).ReservationTime);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Converters")]
        public void ReservationConversionReservationDTOToReservation()
        {
            //Arrange
            var reservationDTO = GetReservationDTOs()[1];
            //Act
            var reservation = Converter.Convert(reservationDTO);
            //Assert
            Assert.AreEqual(reservation.Note, reservationDTO.Note);
            Assert.AreEqual(reservation.Deposit, reservationDTO.Deposit);
            Assert.AreEqual(reservation.NoOfPeople, reservationDTO.NoOfPeople);
            Assert.AreEqual(reservation.ReservationDate, reservationDTO.ReservationDate);
            Assert.AreEqual(reservation.ReservationTime, reservationDTO.ReservationTime);
            Assert.AreEqual(reservation.CustomerId, reservationDTO.Customer.Id);
        }

        #endregion

        #region FoodConversion

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Converters")]
        public void FoodConversionFoodListToFoodDTOList()
        {
            //Arrange
            var foods = GetFood();
            //Act
            var foodDTOs = Converter.Convert(foods);
            //Assert
            for (var i = 0; i < foods.Count; i++)
            {
                //var expectedPriceValue = TODO this linq is bad cause of fail
                //    decimal.ToDouble(foods[i].Price.Where(p => p.Food.Id == foods[i].Id).FirstOrDefault().PriceValue);
                Assert.AreEqual(foodDTOs.ElementAt(i).Id, foods[i].Id);
                Assert.AreEqual(foodDTOs.ElementAt(i).Description, foods[i].Description);
                Assert.AreEqual(foodDTOs.ElementAt(i).Name, foods[i].Name);
                //Assert.AreEqual(foodDTOs.ElementAt(i).Price, foods[i].Price);
                Assert.AreEqual(foodDTOs.ElementAt(i).FoodCategoryName, foods[i].FoodCategory.Name);
            }
        }

        [TestMethod]
        [TestCategory("Unit")]
        [TestCategory("Converters")]
        public void FoodConversionFoodToFoodDTO()
        {
            //Arrange
            var food = GetFood()[0];
            //Act
            var foodDTO = Converter.Convert(food);
            var exceptedFoodPrice = 218;
            //Assert
            Assert.AreEqual(food.Id, foodDTO.Id);
            Assert.AreEqual(food.Description, foodDTO.Description);
            Assert.AreEqual(food.Name, foodDTO.Name);
            Assert.AreEqual(exceptedFoodPrice, foodDTO.Price);
            Assert.AreEqual(food.FoodCategory.Name, foodDTO.FoodCategoryName);
        }

        [TestMethod]
        [TestCategory("Unit")]
        [TestCategory("Converters")]
        public void FoodConversionFoodDTOToFood()
        {
            //Arrange
            var foodDTO = GetFoodDTO()[0];
            //Act
            var food = Converter.Convert(foodDTO);
            //Assert
            Assert.AreEqual(foodDTO.Id, food.Id);
            Assert.AreEqual(foodDTO.Description, food.Description);
        }

        #endregion

        #region priceConversion

        [TestMethod]
        [TestCategory("Unit")]
        [TestCategory("Converters")]
        public void PriceConversionPriceDTOToPrice()
        {
            //Arrange
            var priceDTO = GetPriceDTO()[0];
            //Act
            var price = Converter.Convert(priceDTO);
            //Assert
            Assert.AreEqual(priceDTO.Id, price.Id);
            Assert.AreEqual(priceDTO.PriceValue, price.PriceValue);
        }

        [TestMethod]
        [TestCategory("Unit")]
        [TestCategory("Converters")]
        public void PriceConversionPriceToPriceDTO()
        {
            //Arrange
            var price = GetPrice()[0];
            //Act
            var priceDTO = Converter.Convert(price);
            //Assert
            Assert.AreEqual(price.Id, priceDTO.Id);
            Assert.AreEqual(price.PriceValue, priceDTO.PriceValue);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Converters")]
        public void PriceConversionPriceDTOListToPriceList()
        {
            //Arrange
            var priceDTOs = GetPriceDTO();
            //Act
            var prices = Converter.Convert(priceDTOs);
            //Assert
            for (var i = 0; i < priceDTOs.Count; i++)
            {
                Assert.AreEqual(priceDTOs[i].Id, prices.ElementAt(i).Id);
                Assert.AreEqual(priceDTOs[i].PriceValue, prices.ElementAt(i).PriceValue);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("Converters")]
        public void PriceConversionPriceListToPriceDTOList()
        {
            //Arrange
            var prices = GetPrice();
            //Act
            var priceDTOs = Converter.Convert(prices);
            //Assert

            for (var i = 0; i < priceDTOs.Count; i++)
            {
                Assert.AreEqual(prices[i].Id, priceDTOs.ElementAt(i).Id);
                Assert.AreEqual(prices[i].PriceValue, priceDTOs.ElementAt(i).PriceValue);
            }
        }

        #endregion

        //[TestMethod, TestCategory("Unit"), TestCategory("Converters")]
        //public void CustomerConversionOutput()
        //{
        //    //Arrange
        //    var customerID = 2;
        //    var personID = 4;
        //    var email = "test@example.com";

        //    var input = new Customer()
        //    {
        //        Id = customerID,
        //        PersonId = personID,
        //        Person = new Person()
        //        {
        //            Id = personID,
        //            Email = email,
        //            FirstName = "",
        //            LastName = "",
        //            Location = new Location()
        //            {
        //                Address = "",
        //                ZipCode = "",

        //            },
        //            LocationId = -1,
        //            Phone = ""
        //        }

        //    };
        //    var expected = new CustomerDTO()
        //    {
        //        Id = customerID,

        //    };
        //    //Act
        //    var output = Converter.Convert(input);
        //    //Assert
        //    //sem
        //    //Assert.IsTrue(output);
        //}

        //[TestMethod, TestCategory("Unit"), TestCategory("Converters")]
        //public void ReservationConversionOutput()
        //{

        //    //Arrange
        //    var input = new Reservation()
        //    {

        //    };
        //    var expected = new ReservationDTO()
        //    {
        //        Id = 3
        //    };
        //    //Act
        //    var output = Converter.Convert(input);
        //    //Assert
        //    Assert.IsTrue(true);
        //}

        //[TestMethod, TestCategory("Unit"), TestCategory("Converters")]
        //public void EmployeeConversionOutput()
        //{

        //    //Arrange
        //    var input = new Reservation()
        //    {

        //    };
        //    var expected = new ReservationDTO()
        //    {
        //        Id = 3
        //    };
        //    //Act
        //    var output = Converter.Convert(input);
        //    //Assert
        //    Assert.IsTrue(true);
        //}

        //[TestMethod, TestCategory("Unit"), TestCategory("Converters")]
        //public void TableConversionOutput()
        //{

        //    //Arrange
        //    var input = new Reservation()
        //    {

        //    };
        //    var expected = new ReservationDTO()
        //    {
        //        Id = 3
        //    };
        //    //Act
        //    var output = Converter.Convert(input);
        //    //Assert
        //    Assert.IsTrue(true);
        //}
    }
}