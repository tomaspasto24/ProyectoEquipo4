using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    public class RegisterHandlerTest
    {
        UserInfo user1;
        Message testMessage;
        String response;
        IHandler result;
        Company company;


        [Test]
        public void RegisterHandlerNoPermissionTest()
        {
            user1 = new UserInfo("name1", 5433261);
            user1.Permissions = UserInfo.AdminPermissions;
            SessionRelated.Instance.AddNewUser(user1);
            RegisterHandler registerhandler = new RegisterHandler(null);
            testMessage = new Message(5433261, "");

            result = registerhandler.Handle(testMessage, out response);
            Assert.That(result, Is.Null);
        }
        [Test]
        public void RegisterHandlerHasPermissionTest()
        {
            user1 = new UserInfo("name1", 5433261);
            user1.Permissions = UserInfo.DefaultPermissions;
            SessionRelated.Instance.AddNewUser(user1);
            RegisterHandler registerhandler = new RegisterHandler(null);
            user1.HandlerState = Bot.State.Start;
            testMessage = new Message(5433261, "/registro");

            result = registerhandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Inserta tu token de usuario empresa: "));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void RegisterHandlerConfirmTokenTest()
        {
            user1 = new UserInfo("name1", 5433261);
            user1.Permissions = UserInfo.DefaultPermissions;
            SessionRelated.Instance.AddNewUser(user1);
            RegisterHandler registerhandler = new RegisterHandler(null);
            user1.HandlerState = Bot.State.Start;
            testMessage = new Message(5433261, "/registro");
            result = registerhandler.Handle(testMessage, out response);

            //Agrego el token al dicc para que valide la empresa
            SessionRelated.Instance.DiccUserTokens.Add("IHaveAToken", company);
            user1.HandlerState = Bot.State.ConfirmingToken;
            //le paso el mensaje con mi id y el token para que lo busque en el dicc.
            testMessage = new Message(5433261, "IHaveAToken");

            result = result.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Token verificado, ahora eres un usuario empresa! :)"));
            Assert.That(result, Is.Not.Null);
        }
        /// <summary>
        /// Arreglar
        /// </summary>
        [Test]
        public void RegisterHandlerTokenNoFndTest()
        {
            user1 = new UserInfo("name1", 5433261);
            user1.Permissions = UserInfo.DefaultPermissions;
            SessionRelated.Instance.AddNewUser(user1);
            RegisterHandler registerhandler = new RegisterHandler(null);
            user1.HandlerState = Bot.State.Start;
            testMessage = new Message(5433261, "/registro");
            result = registerhandler.Handle(testMessage, out response);

            user1.HandlerState = Bot.State.ConfirmingToken;
            testMessage = new Message(5433261, "NoToken");

            result = result.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Disculpa, no hemos encontrado ese token :("));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void RegisterHandlerWrongTextTest()
        {
            user1 = new UserInfo("name1", 5433261);
            user1.Permissions = UserInfo.AdminPermissions;
            SessionRelated.Instance.AddNewUser(user1);
            RegisterHandler registerhandler = new RegisterHandler(null);
            user1.HandlerState = Bot.State.ConfirmingToken;
            testMessage = new Message(5433261, "WrongText");

            result = registerhandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo(string.Empty));
            Assert.That(result, Is.Null);
        }
    }
}