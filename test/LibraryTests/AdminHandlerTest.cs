
using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    /// <summary>
    /// Clase AdminHandlerTest la cual se encarga de testear las funcionalidades de la clase AdminHandler, se declaran las variables globales que se van a utilizar.
    /// </summary>
    public class AdminHandlerTest
    {
        UserInfo user1;
        Message testMessage;
        String response;
        IHandler result;

        /// <summary>
        /// Se inicializan las variables que se van a utilizar en los test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(user1);
            user1.Permissions = UserInfo.AdminPermissions;
        }

        /// <summary>
        /// Test que se encarga de testear el AdminHandler y verificar que se le asigne los permisos de admin al usuario al mandar el comando /admin
        /// </summary>
        [Test]
        public void AdminHandlerAdminTest()
        {
            testMessage = new Message(5433261, "/admin");
            AdminHandler adminHandler = new AdminHandler(null);
            result = adminHandler.Handle(testMessage, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Does.Contain("Ahora eres admin"));
        }
        /// <summary>
        /// Test que se encarga de verificar que el AdminHandler devuelva false (null), y un string vacio al momento de enviar un comando incorrecto.
        /// </summary>
        [Test]
        public void AdminHandlerWrongCommandTest()
        {
            testMessage = new Message(5433261, "/WrongCommand");
            AdminHandler adminHandler = new AdminHandler(null);
            result = adminHandler.Handle(testMessage, out response);

            Assert.That(result, Is.Null);
            Assert.That(response, Is.Empty);
        }
    }
}