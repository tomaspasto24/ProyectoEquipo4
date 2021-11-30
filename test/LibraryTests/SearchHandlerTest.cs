
using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    /// <summary>
    /// Clase SearchHandlerTest la cual se encarga de testear las funcionalidades de la clase SearchHandler, se declaran las variables globales que se van a utilizar.
    /// </summary>
    public class SearchHandlerTest
    {
        private UserInfo user1;
        private Message testMessage;
        private String response;
        private IHandler result;

        [SetUp]
        public void Setup()
        {
            SessionRelated.Instance = null;
            this.user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(this.user1);
        }

        /// <summary>
        /// Test que se encarga de verificar si devuelve false en caso de un usario no tener el permiso de "Search". 
        /// </summary>
        [Test]
        public void SearchHandlerNoHasPermissionTest()
        {
            this.user1.Permissions = UserInfo.DefaultPermissions;
            SearchHandler searchHandler = new(null);
            this.testMessage = new Message(5433261, "/busqueda");

            this.result = searchHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.Empty);
            Assert.That(this.result, Is.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar si el handler responde correctamente al comando enviádo.
        /// </summary>
        [Test]
        public void SearchHandlerCommandTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            SearchHandler searchHandler = new(null);
            this.user1.HandlerState = Bot.State.Start;
            this.testMessage = new Message(5433261, "/busqueda");

            this.result = searchHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Por favor dinos el metodo de busqueda que quieres usar. \nEnvía \"/pormaterial\" para buscar por material. \nEnvia \"/porubicacion\" para buscar por ubicación. \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar si retorna falso en caso de que se envíe un comando incorrecto.
        /// </summary>
        [Test]
        public void SearchHandlerWrongCommandTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            SearchHandler searchHandler = new(null);

            this.testMessage = new Message(5433261, "WrongCommand");
            this.user1.HandlerState = Bot.State.Start;
            this.result = searchHandler.Handle(this.testMessage, out this.response);

            Assert.That(this.result, Is.Null);
            Assert.That(this.response, Is.Empty);
        }
    }
}

