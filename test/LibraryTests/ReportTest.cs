using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    public class ReportTest
    {
        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public void CompanyReportTest()
        {
            GeoLocation direccionLasAcacias = new GeoLocation("Camino Maldonado 2415", "Montevideo", "Montevideo");
            Company LasAcacias = new Company("las acacias", "carpinteria", direccionLasAcacias, "094654315");
            Material materialTest = new Material("Madera de pino", 500, 9000);
            Publication publicationTest = new Publication("Madera de pino", LasAcacias, direccionLasAcacias, materialTest);
            publicationTest.ClosePublication();

            Assert.AreEqual()
        }

    }
}