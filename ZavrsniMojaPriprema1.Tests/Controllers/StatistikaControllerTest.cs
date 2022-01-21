using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using ZavrsniMojaPriprema1.Controllers;
using ZavrsniMojaPriprema1.Interfaces;
using ZavrsniMojaPriprema1.Models;

namespace ZavrsniMojaPriprema1.Tests.Controllers
{
    [TestClass]
    public class StatistikaControllerTest
    {
        // ----------------------------------------------------- STATISTIKA ----------------------------------------------------------
        [TestMethod]
        public void GetStatistika_ReturnsMultipleObjects()
        {
            // Arrange
            List<StatistikaDTO> automobili = new List<StatistikaDTO>();
            automobili.Add(new StatistikaDTO { MarkaAutomobila = "Audi", BrojAutomobila = 6 });
            automobili.Add(new StatistikaDTO { MarkaAutomobila = "BMW", BrojAutomobila = 3 });

            var mockRepository = new Mock<IStatistikaRepository>();
            mockRepository.Setup(x => x.PrikaziStatistiku()).Returns(automobili.AsEnumerable());
            var controller = new StatistikaController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetStatistika();
            var createdResult = actionResult as OkNegotiatedContentResult<IEnumerable<StatistikaDTO>>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(automobili.Count(), createdResult.Content.Count());
            Assert.AreEqual("Audi", createdResult.Content.ElementAt(0).MarkaAutomobila);
            Assert.AreEqual("BMW", createdResult.Content.ElementAt(1).MarkaAutomobila);
        }

        // ------------------------------------------ NAJSLABIJI (lista IQueryable DTO) ----------------------------------------------------

        [TestMethod]
        public void GetNajslabiji_ReturnsMultipleObjects()
        {
            // Arrange
            List<Automobil> automobili = new List<Automobil>();
            automobili.Add(new Automobil { Id = 1, Model = "A6", Cena = 54000.0m, GodinaProizvodnje = 2016, KonjskihSnaga = 175, MarkaId = 1, Marka = new Marka { Id = 1, Naziv = "Audi", Drzava = "DE", Automobili = null } });
            automobili.Add(new Automobil { Id = 2, Model = "A8", Cena = 86000.0m, GodinaProizvodnje = 2017, KonjskihSnaga = 255, MarkaId = 1, Marka = new Marka { Id = 1, Naziv = "Audi", Drzava = "DE", Automobili = null } });

            var mockRepository = new Mock<IStatistikaRepository>();
            mockRepository.Setup(x => x.PrikaziNajslabije()).Returns(automobili.AsQueryable());
            var controller = new StatistikaController(mockRepository.Object);

            // Act
            var actionResult = controller.GetNajslabiji();
            var result = actionResult as OkNegotiatedContentResult<IQueryable<AutomobilDTO>>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count());
            Assert.AreEqual(Mapper.Map<AutomobilDTO>(automobili.ElementAt(0)), result.Content.ElementAt(0));
            Assert.AreEqual(Mapper.Map<AutomobilDTO>(automobili.ElementAt(1)), result.Content.ElementAt(1));
        }
    }
}
