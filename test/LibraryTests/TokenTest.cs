using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    public class TokenTest
    {
        /// <summary>
        /// defino la variable afuera para que sea global y adentro del metodo la instancio
        /// </summary>
        RoleAdmin admin;
        Company company;

        [SetUp]
        public void Setup()
        {
            admin = new RoleAdmin("admin1", 1234);
            GeoLocation companyLocation = new GeoLocation("Camino Maldonado 2416", "Montevideo", "Montevideo");
            company = new Company("Las Acacias", "carpinteria", companyLocation, "094654315");
        }

        [Test]
        public void TokenLenghtTest()
        {
            String token = admin.GenerateToken(company);
            Assert.AreEqual(8, token.Length);
        }

        [Test]
        public void TokenAddedTest()
        {
            String token = admin.GenerateToken(company);
            Assert.Contains(token, RoleAdmin.globalRatingsList);
        }

    }
}