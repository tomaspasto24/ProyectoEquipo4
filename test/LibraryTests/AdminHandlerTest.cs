using System;
using Bot;
using NUnit.Framework;

namespace BotTests
{
    /// <summary>
    /// Clase AdminHandlerTest la cual se encarga de testear las funcionalidades de la clase AdminHandler, se declaran las variables globales que se van a utilizar.
    /// </summary>
    public class AdminHandlerTest
    {
        private UserInfo user1;
        private Message testMessage;
        private String response;
        private IHandler result;

        /// <summary>
        /// Se inicializan las variables que se van a utilizar en los test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(this.user1);
            this.user1.Permissions = UserInfo.AdminPermissions;
        }

        /// <summary>
        /// Test que se encarga de testear el AdminHandler y verificar que se le asigne los permisos de admin al usuario al mandar el comando /admin
        /// </summary>
        [Test]
        public void AdminHandlerAdminTest()
        {
            this.testMessage = new Message(5433261, "/admin");
            AdminHandler adminHandler = new(null);
            this.result = adminHandler.Handle(this.testMessage, out this.response);

            Assert.That(this.result, Is.Not.Null);
            Assert.That(this.response, Does.Contain("Ahora eres admin"));
        }

        /// <summary>
        /// Test que se encarga de verificar que el AdminHandler devuelva false (null), y un string vacio al momento de enviar un comando incorrecto.
        /// </summary>
        [Test]
        public void AdminHandlerWrongCommandTest()
        {
            this.testMessage = new Message(5433261, "/WrongCommand");
            AdminHandler adminHandler = new(null);
            this.result = adminHandler.Handle(this.testMessage, out this.response);

            Assert.That(this.result, Is.Null);
            Assert.That(this.response, Is.Empty);
        }
    }
}