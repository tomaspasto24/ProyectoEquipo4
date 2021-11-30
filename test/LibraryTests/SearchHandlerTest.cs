
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
        UserInfo user1;
        Message testMessage;
        String response;
        IHandler result;
        Company company;

        [SetUp]
        public void Setup()
        {
            SessionRelated.Instance = null;
            user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(user1);


        }
        /// <summary>
        /// Test que se encarga de verificar si devuelve false en caso de un usario no tener el permiso de "Search". 
        /// </summary>
        [Test]
        public void SearchHandlerNoHasPermissionTest()
        {
            user1.Permissions = UserInfo.DefaultPermissions;
            SearchHandler searchHandler = new SearchHandler(null);
            testMessage = new Message(5433261, "/busqueda");

            result = searchHandler.Handle(testMessage, out response);
            Assert.That(response, Is.Empty);
            Assert.That(result, Is.Null);
        }
        /// <summary>
        /// Test que se encarga de verificar si el handler responde correctamente al comando enviádo.
        /// </summary>
        [Test]
        public void SearchHandlerCommandTest()
        {
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            SearchHandler searchHandler = new SearchHandler(null);
            user1.HandlerState = Bot.State.Start;
            testMessage = new Message(5433261, "/busqueda");

            result = searchHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Por favor dinos el metodo de busqueda que quieres usar. \nEnvía \"/pormaterial\" para buscar por material. \nEnvia \"/porubicacion\" para buscar por ubicación. \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        /// <summary>
        /// Test que se encarga de verificar si retorna falso en caso de que se envíe un comando incorrecto.
        /// </summary>
        [Test]
        public void SearchHandlerWrongCommandTest()
        {
            GeoLocation PruebaLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            SearchHandler searchHandler = new SearchHandler(null);

            testMessage = new Message(5433261, "WrongCommand");
            user1.HandlerState = Bot.State.Start;
            result = searchHandler.Handle(testMessage, out response);

            Assert.That(result, Is.Null);
            Assert.That(response, Is.Empty);
        }
    }
}

