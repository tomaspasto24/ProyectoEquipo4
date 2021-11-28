/*
using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    /// <summary>
    /// Clase TokenTest, esta se va a encargar de testear las funciones de generar el token el cual estara compuesto por una string alfanumerica.
    /// </summary>
    public class TokenTest
    {
        /// <summary>
        /// Defino la variable afuera para que sea global y adentro del metodo la instancio.
        /// </summary>
        Company company;
        UserInfo user1;

        /// <summary>
        /// Método que crea y asgina las instancias a los atributos que seran utilizados en los test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            GeoLocation companyLocation = new GeoLocation("Camino Maldonado 2416", "Montevideo");
            this.company = new Company("Las Acacias", "carpinteria", companyLocation, "094654315");
        }

        /// <summary>
        /// Test del token para ver si lo que retorna es de tipo int.
        /// </summary>
        [Test]
        public void TokenType()
        {
            user1 = new UserInfo("name1", 5433261);
            user1.Permissions = UserInfo.AdminPermissions;
            String token = user1.GenerateToken(this.company);
            Assert.AreEqual(8, token.GetType());
        }

        /// <summary>
        /// Test para agregar el token generado a la lista de globalRatingsList.
        /// </summary>
        [Test]
        public void TokenAddedTest()
        {
            String token = this.admin.GenerateToken(this.company);
            Assert.AreEqual(token, TokenGenerator.Instance.GenerateToken());
        }
    }
}
*/