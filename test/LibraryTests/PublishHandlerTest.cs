using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    /// <summary>
    /// Clase PublishHandlerTest que se encarga de testear las funcionalidades de PublishHandler.
    /// </summary>
    public class PublishHandlerTest
    {
        private UserInfo user1;
        private Message testMessage;
        private String response;
        private IHandler result;

        /// <summary>
        /// Se declaran las variables que se van a utilizar en los test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            SessionRelated.Instance = null;
            user1 = new UserInfo("name1", 5433261);
            user1.Permissions = UserInfo.UserCompanyPermissions;
            SessionRelated.Instance.AddNewUser(user1);
        }
        /// <summary>
        /// test que se encarga de verificar el comportamiento del hanlder al enviar el comando "/publicar" y no tener el permiso.
        /// </summary>
        [Test]
        public void PublishHandlerNoHasPermissionTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            PublishHandler publishHandler = new PublishHandler(null);
            user1.HandlerState = Bot.State.Start;
            testMessage = new Message(5433261, "/publicar");
            result = publishHandler.Handle(testMessage, out response);

            Assert.That(response, Is.Empty);
            Assert.That(result, Is.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del handler al enviar el comando "/publicar" y tener el permiso.
        /// </summary>
        [Test]
        public void PublishHandlerHasPermissionTest()
        {
            PublishHandler publishHandler = new PublishHandler(null);
            user1.HandlerState = Bot.State.Start;
            testMessage = new Message(5433261, "/publicar");
            result = publishHandler.Handle(testMessage, out response);

            Assert.That(response, Is.EqualTo("Envía el título de la nueva publicación \nEnvía \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del handler al ingresar el nombre de la publicación".
        /// </summary>
        [Test]
        public void PublishHandlerPublicationNameTest()
        {
            PublishHandler publishHandler = new PublishHandler(null);
            user1.HandlerState = Bot.State.AskingPublicationName;
            testMessage = new Message(5433261, "Madera De Pino");
            result = publishHandler.Handle(testMessage, out response);

            Assert.That(response, Is.EqualTo("Envía el nombre del material que quieres agregar \nEnvía \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }

        /*
                [Test]
                public void PublishHandlerPublicatifonNameTest()
                {
                    PublishHandler publishHandler = new PublishHandler(null);
                    user1.HandlerState = Bot.State.AskingCompanyName;
                    testMessage = new Message(5433261, "Madera De Pino");
                    result = publishHandler.Handle(testMessage, out response);

                    testMessage = new Message(5433261, "Las Acacias");
                    result = result.Handle(testMessage, out response);

                    Assert.That(response, Is.EqualTo("Envía el nombre empresa \nEnvía \"/cancelar\" para cancelar la operación"));
                    Assert.That(result, Is.Not.Null);
                }
        */
    }
}