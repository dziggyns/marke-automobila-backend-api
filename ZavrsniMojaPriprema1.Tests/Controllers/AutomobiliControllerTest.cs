using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using ZavrsniMojaPriprema1;
using ZavrsniMojaPriprema1.Controllers;
using ZavrsniMojaPriprema1.Interfaces;
using ZavrsniMojaPriprema1.Models;

namespace ZavrsniMojaPriprema1.Tests.Controllers
{
    [TestClass]
    public class AutomobiliControllerTest
    {

        // ----------------------------------------- GET ---------------------------------------------

        [TestMethod]
        public void GetByIdAutomobil_ReturnsOKContent()
        {
            // Arrange
            var mockRepository = new Mock<IAutomobilRepository>();
            mockRepository.Setup(x => x.GetById(2)).Returns(new Automobil { Id = 2, Model = "Audi" });

            var controller = new AutomobiliController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(2);
            var contentResult = actionResult as OkNegotiatedContentResult<AutomobilDTO>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Id);
        }


        [TestMethod]
        public void GetByIdAutomobil_ReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IAutomobilRepository>();
            var controller = new AutomobiliController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }


        [TestMethod]
        public void GetAllAutomobil_ReturnsMultipleObjects()
        {
            // AutoMapper
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Marka, MarkaDTO>();
                cfg.CreateMap<Automobil, AutomobilDTO>();
            });

            // Arrange
            List<Automobil> automobili = new List<Automobil>();
            automobili.Add(new Automobil { Id = 1, Model = "A6", Cena = 54000.0m, GodinaProizvodnje = 2016, KonjskihSnaga = 175, MarkaId = 1, Marka = new Marka { Id = 1, Naziv = "Audi", Drzava = "DE", Automobili = null } });
            automobili.Add(new Automobil { Id = 2, Model = "A8", Cena = 86000.0m, GodinaProizvodnje = 2017, KonjskihSnaga = 255, MarkaId = 1, Marka = new Marka { Id = 1, Naziv = "Audi", Drzava = "DE", Automobili = null } });

            var mockRepository = new Mock<IAutomobilRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(automobili.AsQueryable());
            var controller = new AutomobiliController(mockRepository.Object);

            // Act
            IEnumerable<AutomobilDTO> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(automobili.Count, result.ToList().Count);
            Assert.AreEqual(Mapper.Map<AutomobilDTO>(automobili.ElementAt(0)), result.ElementAt(0));
            Assert.AreEqual(Mapper.Map<AutomobilDTO>(automobili.ElementAt(1)), result.ElementAt(1));
        }

        // ---------------------------------- POST ----------------------------------------------------

        [TestMethod]
        public void PostAutomobil_ReturnsOkObject()
        {
            // Arrange
            var mockRepository = new Mock<IAutomobilRepository>();
            var controller = new AutomobiliController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Post(new Automobil { Id = 1, Model = "Audi" });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Automobil>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(1, createdResult.RouteValues["id"]);
        }

        // -------------------------------- PUT -------------------------------------------------------

        [TestMethod]
        public void PutAutomobil_ReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IAutomobilRepository>();
            var controller = new AutomobiliController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(2, new Automobil { Id = 1, Model = "Audi" });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void PutAutomobili_ReturnsOkObject()
        {
            // Arrange
            var mockRepository = new Mock<IAutomobilRepository>();
            var controller = new AutomobiliController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(1, new Automobil { Id = 1, Model = "Audi", Cena = 55000m, GodinaProizvodnje = 2020, KonjskihSnaga = 200 });
            var createdResult = actionResult as OkNegotiatedContentResult<Automobil>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(1, createdResult.Content.Id);
        }

        // ------------------------------- DELETE -------------------------------------------------------

        [TestMethod]
        public void DeleteAutomobil_ReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IAutomobilRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(new Automobil { Id = 1, Model = "Audi" });
            var controller = new AutomobiliController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteAutomobil_ReturnsNotFound()
        {
            // Arrange 
            var mockRepository = new Mock<IAutomobilRepository>();
            var controller = new AutomobiliController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
