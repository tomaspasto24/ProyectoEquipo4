
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
        UserInfo user1;
        Message testMessage;
        String response;
        IHandler result;


        [Test]
        public void UserNoPermissionInformationHandlerTest()
        {
            user1 = new UserInfo("name1", 5433261);
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
            user1 = new UserInfo("user1", 5433261);
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            EntrepreneurInfo entrepreneur = new EntrepreneurInfo("Carpintero", PruebaLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, entrepreneur);
            SessionRelated.Instance.AddNewUser(user1);
            user1.HandlerState = Bot.State.ConfirmingHeadingEntrepreneur;
            UserInformationHandler userInfoHandler = new UserInformationHandler(null);
            testMessage = new Message(5433261, "/datos");

            result = userInfoHandler.Handle(testMessage, out response);
            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Estos son tus datos: \nNombre: user1"
                            + "\nUbicacion: Camino Maldonado 2415, Montevideo"
                            + "\nRubro: Carpintero"
                            + "\nEspecialidades: Ninguna"
                            + "\nCertificaciones: Ninguna"));
        }
        [Test]
        public void UserInformationHandlerWrongMessageTest()
        {
            GeoLocation PruebaLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new EntrepreneurInfo("", PruebaLocation);
            user1 = new UserInfo("name1", 5433261);
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