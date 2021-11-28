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
        UserInfo userInfo;
        EntrepreneurInfo entrepreneurInfo;

        /// <summary>
        /// Método que crea y asgina las instancias a los atributos que seran utilizados para ejecutar los test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            userInfo = new UserInfo("Usuario", 1234);
            userInfo.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2416", "Montevideo");
            entrepreneurInfo = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(userInfo, entrepreneurInfo);
            SessionRelated.Instance.AllUsers.Add(userInfo);

            GeoLocation companyLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            company = new Company("Las Acacias", "carpinteria", companyLocation, "094654315");
            Material materialTest = new Material("Madera de pino", 500, 9000);
            publicationTest = new Publication("Madera de Pino", company, companyLocation, materialTest);
            PublicationSet.Instance.AddElement(publicationTest);
        }

        /// <summary>
        /// Test de reporte empresa cuando la publicacion esta cerrada.
        /// </summary>
        [Test]
        public void CompanyReportClosedPublicationTest()
        {
            entrepreneurInfo.ContactCompany(this.publicationTest);
            publicationTest.ClosePublication();
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
            entrepreneurInfo.ContactCompany(publicationTest);
            publicationTest.ClosePublication();
            EntrepreneurReport reporte = new EntrepreneurReport(entrepreneurInfo);
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
            EntrepreneurReport reporte = new EntrepreneurReport(entrepreneurInfo);
            String expected = $"El emprendedor: emprendedor1, no tiene publicaciones asignadas en los ultimos 30 dias";
            // StringAssert.Contains(expected, reporte.GiveReport());
            Assert.AreEqual(expected, reporte.GiveReport());
        }

    }
}
*/