using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    public class PublishHandlerTest
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
            user1.Permissions = UserInfo.UserCompanyPermissions;
            SessionRelated.Instance.AddNewUser(user1);
        }
        /// <summary>
        /// Arreglar
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
        [Test]
        public void PublishHandlerPublicationNameTest()
        {
            PublishHandler publishHandler = new PublishHandler(null);
            user1.HandlerState = Bot.State.AskingPublicationName;
            testMessage = new Message(5433261, "Madera De Pino");
            result = publishHandler.Handle(testMessage, out response);

            Assert.That(response, Is.EqualTo("Envía el nombre empresa \nEnvía \"/cancelar\" para cancelar la operación"));
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