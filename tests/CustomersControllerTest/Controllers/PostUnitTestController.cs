using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesAppWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using SalesAppWebAPI.Repositories;

namespace CustomersControllerTest.Controllers
{
    public class PostUnitTestController 
    {
        //private readonly CustomerRepository _sut;

        //public PostUnitTestController()
        //{
        //    var customerMock = CreateDbSetMock(GetFakeListOfMovies());
        //    var mockDbContext = new Mock<CustomerDBContext>();
        //    mockDbContext.Setup(x => x.Customers).Returns(customerMock.Object);
        //    _sut = new CustomerRepository(mockDbContext.Object);
        //}

        [Fact]
        public void GetAllTest()
        {
            var options = new DbContextOptionsBuilder<CustomerDBContext>()
                .UseInMemoryDatabase(databaseName: "CustomerDB")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new CustomerDBContext(options))
            {
                context.Customers.Add(new Customer 
                { 
                  Id = 1, 
                  FirstName = "Andersen", 
                  LastName = "Adedeji", 
                  Email = "andersenadedeji@gmail.com", 
                  Gender = "Male", 
                  CreationTime = DateTime.Now
                });
                context.Customers.Add(new Customer 
                {
                  Id = 2,
                  FirstName = "Semilore", 
                  LastName = "Andersen-Adedeji", 
                  Email = "semiloreade@gmail.com", 
                  Gender = "Female", 
                  CreationTime = DateTime.Now 
                });
                context.Customers.Add(new Customer
                { 
                   Id = 3,
                   FirstName = "Denzel",
                   LastName = "Andersen-Adedeji",
                   Email = "DzAde@gmail.com", 
                   Gender = "Male", 
                   CreationTime = DateTime.Now 
                });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new CustomerDBContext(options))
            {
                CustomerRepository customerRepository = new CustomerRepository(context);
                List<Customer> customers = (List<Customer>)customerRepository.GetAll();


            Assert.Equal(3, customers.Count);
            }
        }

    }


}
