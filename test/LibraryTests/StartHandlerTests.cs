using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    /// <summary>
    /// Clase TokenHandlerTest la cual se encarga de testear las funcionalidades de la clase tokenHandler.
    /// </summary>
    public class StartHandlerTest
    {
        IRole role;
        UserInfo user1;
        Message testMessage;
        StartHandler stHandler;
        String response;
        IHandler result;

        [SetUp]
        public void Setup()
        {
            role = new RoleDefault();
            user1 = new UserInfo("name1", 5433261, role);
            SessionRelated.Instance.AddNewUser(user1);
            stHandler = new StartHandler(null);
        }

        [Test]
        public void StartHandlerHelloTest()
        {
            testMessage = new Message(5433261, "/hola");
            result = stHandler.Handle(testMessage, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Does.Contain("¡Bienvenido al bot del equipo 4! \n ¿Qué desea hacer?"));
        }

        [Test]
        public void StartHandlerWrongTextTest()
        {
            testMessage = new Message(5433261, "/hello");
            result = stHandler.Handle(testMessage, out response);

            Assert.That(result, Is.Null);
            // Assert.That(response, Does.Contain("¡Bienvenido al bot del equipo 4! \n ¿Qué desea hacer?"));
            Assert.That(response, Is.EqualTo(""));
        }
    }
}