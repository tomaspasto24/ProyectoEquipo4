
using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    /// <summary>
    /// Clase TokenHandlerTest la cual se encarga de testear las funcionalidades de la clase tokenHandler, se declaran las variables globales que se van a utilizar.
    /// </summary>
    public class StartHandlerTest
    {
        UserInfo user1;
        Message testMessage;
        StartHandler stHandler;
        String response;
        IHandler result;

        /// <summary>
        /// Se inicializan las variables.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            user1 = new UserInfo("name1", 5433261);
            user1.Permissions = UserInfo.DefaultPermissions;
        }
        /// <summary>
        /// Se testea que el StartHandler responda al comando /hola.
        /// </summary>
        [Test]
        public void StartHandlerHelloTest()
        {
            testMessage = new Message(5433261, "/hola");
            result = stHandler.Handle(testMessage, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Does.Contain("¡Bienvenido al bot del equipo 4! \n ¿Qué desea hacer?"));
        }
        /// <summary>
        /// Se testea que el StartHandler no envie el mensaje dado en caso de que el comando que se ingresa sea diferente al esperado.
        /// </summary>
        [Test]
        public void StartHandlerWrongTextTest()
        {
            testMessage = new Message(5433261, "/hello");
            result = stHandler.Handle(testMessage, out response);

            Assert.That(result, Is.Null);
            //Assert.That(response, Does.Contain("¡Bienvenido al bot del equipo 4! \n ¿Qué desea hacer?"));
            Assert.That(response, Is.EqualTo(""));
        }
    }
}
