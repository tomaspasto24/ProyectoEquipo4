using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    public class UnderTakeHanlderTest
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

        [Test]
        public void UnderTakeHandlerNoPermissionTest()
        {
            user1.Permissions = UserInfo.UserCompanyPermissions;
            SearchHandler searchHandler = new SearchHandler(null);
            testMessage = new Message(5433261, "/busqueda");

            result = searchHandler.Handle(testMessage, out response);
            Assert.That(result, Is.Null);
        }
        [Test]
        public void UnderTakeHandlerHasPermissionTest()
        {
            user1.Permissions = UserInfo.DefaultPermissions;
            UndertakeHandler undertakeHandler = new UndertakeHandler(null);
            user1.HandlerState = Bot.State.Start;
            testMessage = new Message(5433261, "/emprender");

            result = undertakeHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Por favor, dinos tu rubro. \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void UnderTakeHandlerConfimingHeadingTest()
        {
            user1.Permissions = UserInfo.DefaultPermissions;
            UndertakeHandler undertakeHandler = new UndertakeHandler(null);
            user1.HandlerState = Bot.State.ConfirmingHeadingEntrepreneur;
            testMessage = new Message(5433261, "Carpintero");

            result = undertakeHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Rubro registrado. Ahora dinos en que ciudad vives. \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void UnderTakeHandlerConfirmingCityTest()
        {
            user1.Permissions = UserInfo.DefaultPermissions;
            UndertakeHandler undertakeHandler = new UndertakeHandler(null);
            user1.HandlerState = Bot.State.ConfirmingHeadingEntrepreneur;
            testMessage = new Message(5433261, "Carpintero");
            result = undertakeHandler.Handle(testMessage, out response);

            user1.HandlerState = Bot.State.ConfirmingCityEntrepreneur;
            testMessage = new Message(5433261, "Montevideo");

            result = result.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Ciudad registrada. Ahora dinos tu direccion. \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void UnderTakeHandlerConfirmingAdressTest()
        {
            user1.Permissions = UserInfo.DefaultPermissions;
            UndertakeHandler undertakeHandler = new UndertakeHandler(null);
            user1.HandlerState = Bot.State.ConfirmingHeadingEntrepreneur;
            testMessage = new Message(5433261, "Carpintero");
            result = undertakeHandler.Handle(testMessage, out response);

            user1.HandlerState = Bot.State.ConfirmingAdressEntrepreneur;
            testMessage = new Message(5433261, "8 de octubre");

            result = result.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Direccion registrada. \nAhora eres un emprendedor!"));
            Assert.That(result, Is.Not.Null);
        }
    }
}
