
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
            this.userInfo = new("Usuario", 1234);
            this.userInfo.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new("Camino Maldonado 2416", "Montevideo");
            this.entrepreneurInfo = new("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(this.userInfo, this.entrepreneurInfo);
            SessionRelated.Instance.AllUsers.Add(this.userInfo);

            GeoLocation companyLocation = new("Camino Maldonado 2415", "Montevideo");
            this.company = new Company("Las Acacias", "carpinteria", companyLocation, "094654315");
            Material materialTest = new("Madera de pino", 500, 9000);
            this.publicationTest = new Publication("Madera de Pino", this.company, companyLocation, materialTest);
            PublicationSet.Instance.AddElement(this.publicationTest);
        }

        /// <summary>
        /// Test de reporte empresa cuando la publicacion esta cerrada.
        /// </summary>
        [Test]
        public void CompanyReportClosedpublicationTest()
        {
            this.entrepreneurInfo.ContactCompany(this.publicationTest);
            this.publicationTest.ClosePublication();
            CompanyReport reporte = new(this.company);
            String expected = "Publicaciones cerradas de los ultimos 30 dias de la empresa: Las Acacias";

            StringAssert.Contains(expected, reporte.GiveReport());
        }

        /// <summary>
        /// Test de reporte empresa cuando la publicacion no esta cerrada.
        /// </summary>
        [Test]
        public void CompanyReportPublicationNotClosedTest()
        {
            CompanyReport reporte = new(this.company);
            String expected = "No hay publicaciones cerradas en los ultimos 30 dias para la empresa: Las Acacias";
            StringAssert.Contains(expected, reporte.GiveReport());
        }

        /// <summary>
        /// Test del reporte emprendedor cuando la publicacion esta cerrada.
        /// </summary>
        [Test]
        public void EntrepreneurReportClosedPublicationTest()
        {
            this.entrepreneurInfo.ContactCompany(this.publicationTest);
            this.publicationTest.ClosePublication();
            EntrepreneurReport reporte = new(this.entrepreneurInfo);
            String expected = "Materiales consumidos en los ultimos 30 dias por el emprendedor #1 - Madera de Pino";
            StringAssert.Contains(expected, reporte.GiveReport());
        }

        /// <summary>
        /// Test del reporte emprendedor en caso de que la publicacion no este cerrada.
        /// </summary>
        [Test]
        public void EntrepreneurReportPublicationNotClosedTest()
        {
            EntrepreneurReport reporte = new(this.entrepreneurInfo);
            String expected = $"El emprendedor no tiene publicaciones asignadas en los ultimos 30 dias";
            Assert.AreEqual(expected, reporte.GiveReport());
        }

    }
}
