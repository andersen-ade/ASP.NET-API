//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using SalesAppWebAPI.Controllers;

//namespace SalesAppWebAPI.Models
//{
//    public class DummyDataDBInitializer
//    {
//        public DummyDataDBInitializer()
//        {
//        }

//        public void Seed(CustomerDBContext context)
//        {
//            context.Database.EnsureDeleted();
//            context.Database.EnsureCreated();

//            context.Customers.AddRange(
//                new Customer() { Id = 1, FirstName = "CSHARP", LastName = "csharp", Email = "csharp.com" },
//                new Customer() { Id = 2, FirstName = "VISUAL STUDIO", LastName = "visualstudio", Email = "visstudio.com" },
//                new Customer() { Id = 3, FirstName = "ASP.NET CORE", LastName = "aspnetcore", Email = "aspnet.com" },
//                new Customer() { Id = 4, FirstName = "SQL SERVER", LastName = "sqlserver", Email = "sqlserver.com" }
//            );

//            context.SaveChanges();
//        }
//    }
//}
