using Bot;
using NUnit.Framework;

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
        private UserInfo user1;
        private TokenGenerator tk;

        /// <summary>
        /// Método que crea y asgina las instancias a los atributos que seran utilizados en los test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.tk = new TokenGenerator();
            this.user1 = new UserInfo("name1", 5433261);
        }

        /// <summary>
        /// Test del token para ver si lo que retorna es de tipo int.
        /// </summary>
        [Test]
        public void TokenType()
        {
            this.user1.Permissions = UserInfo.AdminPermissions;
            Assert.That(typeof(int), Is.EqualTo(this.tk.GenerateToken().GetType()));
        }
    }
}
