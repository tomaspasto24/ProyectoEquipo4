/*
using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    /// <summary>
    /// Clase ReportTest la cual se encarga de testear las funcionalidades de la clase EntrepreneurReport y CompanyReport.
    /// </summary>
    public class ReportTest
    {
        Publication publicationTest;
        Company company;
        RoleEntrepreneur entrepreneur;

        /// <summary>
        /// Método que crea y asgina las instancias a los atributos que seran utilizados para ejecutar los test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            GeoLocation companyLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2416", "Montevideo");
            this.company = new Company("Las Acacias", "carpinteria", companyLocation, "094654315");
            String title = "Madera de pino";
            Material materialTest = new Material(title, 500, 9000);
            publicationTest = new Publication(title, company, companyLocation, materialTest);
            PublicationSet.Instance.AddElement(publicationTest);
            entrepreneur = new RoleEntrepreneur("carpintero", entrepreneurLocation);
        }

        /// <summary>
        /// Test de reporte empresa cuando la publicacion esta cerrada.
        /// </summary>
        [Test]
        public void CompanyReportClosedPublicationTest()
        {
            this.entrepreneur.ContactCompany(this.publicationTest);
            this.publicationTest.ClosePublication();
            CompanyReport reporte = new CompanyReport(this.company);
            String expected = "Publicaciones cerradas de los ultimos 30 dias de la empresa: Las Acacias";

            StringAssert.Contains(expected, reporte.GiveReport());
        }

        /// <summary>
        /// Test de reporte empresa cuando la publicacion no esta cerrada.
        /// </summary>
        [Test]
        public void CompanyReportPublicationNotClosedTest()
        {
            CompanyReport reporte = new CompanyReport(this.company);
            String expected = "No hay publicaciones cerradas en los ultimos 30 dias para la empresa: Las Acacias";
            StringAssert.Contains(expected, reporte.GiveReport());
        }

        /// <summary>
        /// Test del reporte emprendedor cuando la publicacion esta cerrada.
        /// </summary>
        [Test]
        public void EntrepreneurReportClosedPublicationTest()
        {
            this.entrepreneur.ContactCompany(this.publicationTest);
            this.publicationTest.ClosePublication();
            EntrepreneurReport reporte = new EntrepreneurReport(this.entrepreneur);
            String expected = "Materiales consumidos en los ultimos 30 dias por el emprendedor: emprendedor1 #1 - Madera de pino";
            StringAssert.Contains(expected, reporte.GiveReport());
            //Assert.AreEqual(expected, reporte.GiveReport());
        }

        /// <summary>
        /// Test del reporte emprendedor en caso de que la publicacion no este cerrada.
        /// </summary>
        [Test]
        public void EntrepreneurReportPublicationNotClosedTest()
        {
            EntrepreneurReport reporte = new EntrepreneurReport(this.entrepreneur);
            String expected = $"El emprendedor: emprendedor1, no tiene publicaciones asignadas en los ultimos 30 dias";
            // StringAssert.Contains(expected, reporte.GiveReport());
            Assert.AreEqual(expected, reporte.GiveReport());
        }

    }
}
*/