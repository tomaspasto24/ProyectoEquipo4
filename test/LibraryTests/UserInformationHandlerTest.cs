using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    /// <summary>
    /// Clase TextNullHandlerTest la cual se encarga de testear las funcionalidades de la clase TextNullHandler, se declaran las variables globales que se van a utilizar.
    /// </summary>
    public class UserInformationHandlerTest
    {
        IRole role;
        UserInfo user1;
        Message testMessage;
        String response;
        IHandler result;


        [Test]
        public void UserNoPermissionInformationHandlerTest()
        {
            role = new RoleDefault();
            user1 = new UserInfo("name1", 5433261, role);
            SessionRelated.Instance.AddNewUser(user1);
            user1.HandlerState = Bot.State.ConfirmingHeadingEntrepreneur;
            UserInformationHandler userInfoHandler = new UserInformationHandler(null);
            testMessage = new Message(5433261, "prueba");

            result = userInfoHandler.Handle(testMessage, out response);
            Assert.That(result, Is.Null);
        }
        [Test]
        public void UserHasPermissionInformationHandlerTest()
        {
            GeoLocation PruebaLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            role = new RoleEntrepreneur("", PruebaLocation);
            user1 = new UserInfo("name1", 5433261, role);
            SessionRelated.Instance.AddNewUser(user1);
            user1.HandlerState = Bot.State.ConfirmingHeadingEntrepreneur;
            UserInformationHandler userInfoHandler = new UserInformationHandler(null);
            testMessage = new Message(5433261, "/datos");

            result = userInfoHandler.Handle(testMessage, out response);
            Assert.That(result, Is.Not.Null);
            Assert.That(response.Contains($"Estos son tus datos: \nNombre: name1"
               + $"\nRole: Emprendedor"
               + "\nUbicacion: Camino Maldonado 2415, Montevideo"));
        }
        [Test]
        public void UserInformationHandlerWrongMessageTest()
        {
            GeoLocation PruebaLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            role = new RoleEntrepreneur("", PruebaLocation);
            user1 = new UserInfo("name1", 5433261, role);
            SessionRelated.Instance.AddNewUser(user1);
            user1.HandlerState = Bot.State.ConfirmingHeadingEntrepreneur;
            UserInformationHandler userInfoHandler = new UserInformationHandler(null);
            testMessage = new Message(5433261, "/WrongMessage");

            result = userInfoHandler.Handle(testMessage, out response);
            Assert.That(result, Is.Null);
            Assert.That(response, Is.EqualTo(String.Empty));
        }
    }
}