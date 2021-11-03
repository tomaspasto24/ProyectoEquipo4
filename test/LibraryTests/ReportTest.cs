using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    public class ReportTest
    {
        Publication publicationTest;
        Company company;
        RoleEntrepreneur entrepreneur;

        [SetUp]
        public void Setup()

        {
            GeoLocation companyLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo", "Montevideo");
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2416", "Montevideo", "Montevideo");
            company = new Company("Las Acacias", "carpinteria", companyLocation, "094654315");
            String title = "Madera de pino";
            Material materialTest = new Material(title, 500, 9000);
            publicationTest = PublicationSet.AddPublication(title, company, companyLocation, materialTest);
            entrepreneur = new RoleEntrepreneur("emprendedor1", 5433264, "carpintero", entrepreneurLocation, "oficial", "lustrado");
        }

        [Test]
        public void CompanyReportClosedPublicationTest()
        {

            publicationTest.ClosePublication(entrepreneur);
            CompanyReport reporte = new CompanyReport(company);
            String expected = "Publicaciones cerradas de los ultimos 30 dias de la empresa: Las Acacias1- Madera de pino";

            StringAssert.Contains(expected, reporte.GiveReport());
        }

        [Test]
        public void CompanyReportPublicationNotClosedTest()
        {
            CompanyReport reporte = new CompanyReport(company);
            String expected = "No hay publicaciones cerradas en los ultimos 30 dias para la empresa: Las Acacias";
            StringAssert.Contains(expected, reporte.GiveReport());
        }
        [Test]
        /// <summary>
        /// test del reporte emprendedor cuando la publicacion esta cerrada
        /// </summary>
        public void EntrepreneurReportClosedPublicationTest()
        {
            publicationTest.ClosePublication(entrepreneur);
            EntrepreneurReport reporte = new EntrepreneurReport(entrepreneur);
            String expected = "Materiales consumidos en los ultimos 30 dias por el emprendedor: emprendedor1 #1 - Madera de pino";
            StringAssert.Contains(expected, reporte.GiveReport());
            //Assert.AreEqual(expected, reporte.GiveReport());
        }
        [Test]
        /// <summary>
        /// test del reporte emprendedor en caso de que la publicacion no este cerrada
        /// </summary>
        public void EntrepreneurReportPublicationNotClosedTest()
        {
            EntrepreneurReport reporte = new EntrepreneurReport(entrepreneur);
            String expected = $"El emprendedor: emprendedor1, no tiene publicaciones asignadas en los ultimos 30 dias";
            // StringAssert.Contains(expected, reporte.GiveReport());
            Assert.AreEqual(expected, reporte.GiveReport());
        }

    }
}