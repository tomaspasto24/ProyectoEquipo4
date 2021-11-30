
using System;
using Bot;
using NUnit.Framework;

namespace BotTests
{
    /// <summary>
    /// Clase TokenHandlerTest la cual se encarga de testear las funcionalidades de la clase tokenHandler, se declaran las variables globales que se van a utilizar.
    /// </summary>
    public class StartHandlerTest
    {
        private UserInfo user1;
        private Message testmessage;
        private StartHandler stHandler;
        private String response;
        private IHandler result;

        /// <summary>
        /// Se inicializan las variables.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(this.user1);
            this.user1.Permissions = UserInfo.DefaultPermissions;
            this.stHandler = new StartHandler(null);
        }

        /// <summary>
        /// Se testea que el StartHandler responda al comando /hola.
        /// </summary>
        [Test]
        public void StartHandlerHelloTest()
        {
            this.testmessage = new Message(5433261, "/hola");
            this.result = this.stHandler.Handle(this.testmessage, out this.response);

            Assert.That(this.result, Is.Not.Null);
            Assert.That(this.response, Does.Contain("¡Bienvenido al bot del equipo 4! \n ¿Qué desea hacer?"));
        }

        /// <summary>
        /// Se testea que el StartHandler no envie el mensaje dado en caso de que el comando que se ingresa sea diferente al esperado.
        /// </summary>
        [Test]
        public void StartHandlerWrongTextTest()
        {
            this.testmessage = new Message(5433261, "/hello");
            this.result = this.stHandler.Handle(this.testmessage, out this.response);

            Assert.That(this.result, Is.Null);
            Assert.That(this.response, Is.EqualTo(""));
        }
    }
}
