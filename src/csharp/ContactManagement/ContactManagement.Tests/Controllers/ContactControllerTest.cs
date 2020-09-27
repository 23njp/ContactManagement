using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using ContactManagement.Controllers;
using ContactManagement.DAL.Model;
using ContactManagement.Entity.Entity;
using ContactManagement.Entity.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace ContactManagement.Tests.Controllers
{
    [TestClass]
    public class ContactControllerTest
    {
        private UnitOfWork<AppDbContext> unitOfWork = new UnitOfWork<AppDbContext>();
        private GenericRepository<Contact> repository;
        private IContactRepository contact_repository;
        public ContactControllerTest()
        {
            repository = new GenericRepository<Contact>(unitOfWork);
            this.contact_repository = new ContactRepository(unitOfWork);
        }

        [TestMethod]
        public void GetAll()
        {
            //// Arrange
            ContactController controller = new ContactController();
            DataTablesHelper dataTables = new DataTablesHelper() { Skip = 0, Pagesize = 10, SortColumn = "ID", SortDirection = "asc", Filters = new List<Entity.Model.Filter>() };

            //// Act
            //controller.GetAll(dataTables);
            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/products")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "contact" } });

            var result = controller.GetAll(dataTables);

            var contentResult = result as OkNegotiatedContentResult<ResultData<List<Contact>>>;
            var actual = contentResult.Content.Data.ToList();
            var expected = repository.GetAll(dataTables).ToList();

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<ResultData<List<Contact>>>));            
        }

        [TestMethod]
        public void AddContactAPI()
        {
            ContactController controller = new ContactController();
            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/products")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "contact" } });

            Contact contact = new Contact() { ID = 0, FirstName = "Pradip2", LastName = "Patel", Email = "pradip@gmail.com", IsActive = true, PhoneNumber = "9876543210" };
            var response = controller.Save(contact);
            var contentResult = response as OkNegotiatedContentResult<ResultData<string>>;
            var data = contentResult.Content.Data;
            int id = 0;
            int.TryParse(data, out id);
            contact.ID = id;
            var message = contentResult.Content.Message;

            Contact expContact = repository.GetById(id);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<ResultData<string>>));
            Assert.AreEqual(message, "contact saved");
            Assert.AreEqual(contact.FirstName, expContact.FirstName);
            Assert.AreEqual(contact.LastName, expContact.LastName);
            Assert.AreEqual(contact.Email, expContact.Email);
            Assert.AreEqual(contact.PhoneNumber, expContact.PhoneNumber);
            Assert.AreEqual(contact.IsActive, expContact.IsActive);   
        }

        [TestMethod]
        public void AddContactEmptyFirstName()
        {
            Contact contact = new Contact() { ID = 0, FirstName = "", LastName = "Patel", Email = "pradip@gmail.com", IsActive = true, PhoneNumber = "9876543210" };
            repository.Insert(contact);
            Assert.ThrowsException<Exception>(() => unitOfWork.Save());
        }
        [TestMethod]
        public void AddContactInvalidEmail()
        {
            Contact contact = new Contact() { ID = 0, FirstName = "Nikunj", LastName = "Patel", Email = "", IsActive = true, PhoneNumber = "9876543210" };
            repository.Insert(contact);
            Assert.ThrowsException<Exception>(() => unitOfWork.Save());
        }

        [TestMethod]
        public void UpdateContact()
        {
            ContactController controller = new ContactController();
            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/products")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "contact" } });

            Contact contact = new Contact() { ID = 0, FirstName = "Pradip2", LastName = "Patel", Email = "pradip@gmail.com", IsActive = true, PhoneNumber = "9876543210" };
            var response = controller.Save(contact);
            var contentResult = response as OkNegotiatedContentResult<ResultData<string>>;
            var data = contentResult.Content.Data;
            int id = 0;
            int.TryParse(data, out id);
            contact.ID = id;
            var message = contentResult.Content.Message;

            Contact expContact = repository.GetById(id);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<ResultData<string>>));
            Assert.AreEqual(message, "contact saved");
            Assert.AreEqual(contact.FirstName, expContact.FirstName);
            Assert.AreEqual(contact.LastName, expContact.LastName);
            Assert.AreEqual(contact.Email, expContact.Email);
            Assert.AreEqual(contact.PhoneNumber, expContact.PhoneNumber);
            Assert.AreEqual(contact.IsActive, expContact.IsActive);
        }

        [TestMethod]
        public void DeleteContact()
        {
            ContactController controller = new ContactController();
            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/products")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "contact" } });

            int id = 18;
            var response = controller.Delete(id);
            var contentResult = response as OkNegotiatedContentResult<ResultData<string>>;
            var message = contentResult.Content.Message;
            Contact expContact = repository.GetById(id);

            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<ResultData<string>>));
            Assert.AreEqual(message, "contact deleted");
            Assert.IsNull(expContact);
        }

        [TestMethod]
        public void DeleteContactNotFound()
        {
            ContactController controller = new ContactController();
            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/products")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "contact" } });

            int id = 1800;
            var response = controller.Delete(id);
            var contentResult = response as OkNegotiatedContentResult<ResultData<string>>;
            var message = contentResult.Content.Message;            

            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<ResultData<string>>));
            Assert.AreEqual(message, "Contact not found");
        }
    }
}
