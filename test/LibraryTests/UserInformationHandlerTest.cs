
using System;
using Bot;
using NUnit.Framework;

namespace BotTests
{
    /// <summary>
    /// Clase UserInformationHandlerTest la cual se encarga de testear las funcionalidades de la clase UserInformationHandler, se declaran las variables globales que se van a utilizar.
    /// </summary>
    public class UserInformationHandlerTest
    {
        private UserInfo user1;
        private Message testMessage;
        private String response;
        private IHandler result;

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del hanlder al momento de un usuario enviar un mensaje y no tener el permiso necesario.
        /// </summary>
        [Test]
        public void UserNoPermissionInformationHandlerTest()
        {
            SessionRelated.Instance = null;
            this.user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(this.user1);
            this.user1.HandlerState = Bot.State.ConfirmingHeadingEntrepreneur;
            UserInformationHandler userInfoHandler = new(null);
            this.testMessage = new Message(5433261, "prueba");

            this.result = userInfoHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.result, Is.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del hanlder en caso de un usuario enviar el comando "/datos".
        /// </summary>
        [Test]
        public void UserHasPermissionInformationHandlerTest()
        {
            SessionRelated.Instance = null;
            GeoLocation PruebaLocation = new("Camino Maldonado 2415", "Montevideo");
            this.user1 = new("this.user1", 5433261);
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            EntrepreneurInfo entrepreneur = new("Carpintero", PruebaLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(this.user1, entrepreneur);
            SessionRelated.Instance.AddNewUser(this.user1);
            this.user1.HandlerState = Bot.State.ConfirmingHeadingEntrepreneur;
            UserInformationHandler userInfoHandler = new(null);
            this.testMessage = new Message(5433261, "/datos");

            this.result = userInfoHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.result, Is.Not.Null);
            Assert.That(this.response, Is.EqualTo("Estos son tus datos: \nNombre: this.user1"
                            + "\nUbicacion: Camino Maldonado 2415, Montevideo"
                            + "\nRubro: Carpintero"
                            + "\nEspecialidades: Ninguna"
                            + "\nCertificaciones: Ninguna"));
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del hanlder en caso que se le envie un mensaje incorrecto.
        /// </summary>
        [Test]
        public void UserInformationHandlerWrongMessageTest()
        {
            SessionRelated.Instance = null;
            this.user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(this.user1);
            this.user1.HandlerState = Bot.State.ConfirmingHeadingEntrepreneur;
            UserInformationHandler userInfoHandler = new(null);
            this.testMessage = new Message(5433261, "/WrongMessage");

            this.result = userInfoHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.result, Is.Null);
            Assert.That(this.response, Is.EqualTo(String.Empty));
        }
    }
}