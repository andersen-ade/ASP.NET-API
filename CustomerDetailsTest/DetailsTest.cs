using SalesAppWebAPI.Controllers;
using SalesAppWebAPI.Models;
using FakeItEasy;
using System;
using Xunit;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerDetailsTest
{
   
    public class DetailsTest
    {
        [Fact]
        public void Get_Customer_Details()
        {
            // Arrange
            var fakeCustomers = A.ListOfDummy<Customer>.AsEnumerable();
            var dataStore = A.Fake<CustomerDBContext>();
            A.CallTo(() => dataStore.GetCustomers()).Return(Task.FromResult());
            var controller = new CustomersController(dataStore);

            // Act


            // Assert
        }
    }
}
