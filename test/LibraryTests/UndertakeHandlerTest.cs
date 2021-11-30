using System;
using Bot;
using NUnit.Framework;

namespace BotTests
{
    /// <summary>
    /// Clase UnderTakeHanlderTest que se encarga de verificar las funcionalidades del handler UnderTakeHanlder
    /// </summary>
    public class UnderTakeHanlderTest
    {
        private UserInfo user1;
        private Message testMessage;
        private String response;
        private IHandler result;

        /// <summary>
        /// Se inicializan las variables que se van a usar en los test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            SessionRelated.Instance = null;
            this.user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(this.user1);
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del handler en caso que el usuario no tenga el permiso.
        /// </summary>
        [Test]
        public void UnderTakeHandlerNoPermissionTest()
        {
            this.user1.Permissions = UserInfo.UserCompanyPermissions;
            SearchHandler searchHandler = new SearchHandler(null);
            this.testMessage = new Message(5433261, "/busqueda");

            this.result = searchHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.result, Is.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del handler en caso que el usuario tenga el permiso.
        /// </summary>
        [Test]
        public void UnderTakeHandlerHasPermissionTest()
        {
            this.user1.Permissions = UserInfo.DefaultPermissions;
            UndertakeHandler undertakeHandler = new UndertakeHandler(null);
            this.user1.HandlerState = Bot.State.Start;
            this.testMessage = new Message(5433261, "/emprender");

            this.result = undertakeHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Por favor, dinos tu rubro. \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del handler en caso de intentar registrar un rubro.
        /// </summary>
        [Test]
        public void UnderTakeHandlerConfimingHeadingTest()
        {
            this.user1.Permissions = UserInfo.DefaultPermissions;
            UndertakeHandler undertakeHandler = new UndertakeHandler(null);
            this.user1.HandlerState = Bot.State.ConfirmingHeadingEntrepreneur;
            this.testMessage = new Message(5433261, "Carpintero");

            this.result = undertakeHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Rubro registrado. Ahora dinos en que ciudad vives. \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del handler en caso de intentar registrar la ciudad.
        /// </summary>
        [Test]
        public void UnderTakeHandlerConfirmingCityTest()
        {
            this.user1.Permissions = UserInfo.DefaultPermissions;
            UndertakeHandler undertakeHandler = new UndertakeHandler(null);
            this.user1.HandlerState = Bot.State.ConfirmingHeadingEntrepreneur;
            this.testMessage = new Message(5433261, "Carpintero");
            this.result = undertakeHandler.Handle(this.testMessage, out this.response);

            this.user1.HandlerState = Bot.State.ConfirmingCityEntrepreneur;
            this.testMessage = new Message(5433261, "Montevideo");

            this.result = this.result.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Ciudad registrada. Ahora dinos tu direccion. \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del handler en caso de intentar verificar la direccion.
        /// </summary>
        [Test]
        public void UnderTakeHandlerConfirmingAdressTest()
        {
            this.user1.Permissions = UserInfo.DefaultPermissions;
            UndertakeHandler undertakeHandler = new UndertakeHandler(null);
            this.user1.HandlerState = Bot.State.ConfirmingHeadingEntrepreneur;
            this.testMessage = new Message(5433261, "Carpintero");
            this.result = undertakeHandler.Handle(this.testMessage, out this.response);

            this.user1.HandlerState = Bot.State.ConfirmingAdressEntrepreneur;
            this.testMessage = new Message(5433261, "8 de octubre");

            this.result = this.result.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Direccion registrada. \nAhora eres un emprendedor!"));
            Assert.That(this.result, Is.Not.Null);
        }
    }
}
