using System;
using Bot;
using NUnit.Framework;

namespace BotTests
{
    /// <summary>
    /// Clase ModifyEntrepreneurInformationHandlerTest la cual se encarga de testear las funcionalidades de la clase ModifyEntrepreneurInformationHandlder, se declaran las variables globales que se van a utilizar.
    /// </summary>
    public class ModifyEntrepreneurInformationHandlerTest
    {
        private UserInfo user1;
        private Message testMessage;
        private String response;
        private IHandler result;
        private EntrepreneurInfo entrepreneur;

        /// <summary>
        /// Se inicializan las variables que se van a utilizar en los test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            SessionRelated.Instance = null;
            this.user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(this.user1);
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            this.entrepreneur = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(this.user1, this.entrepreneur);
        }

        /// <summary>
        /// Test que se encarga de verificar la clase ModifyEntrepreneurInformationHandlder en caso de no tener el permiso necesario.
        /// </summary>
        [Test]
        public void ModifyEntrepreneurInformationHandlderNoHasPermissionTest()
        {
            this.user1.Permissions = UserInfo.DefaultPermissions;
            ModifyEntrepreneurInformationHandler modifyEntrepreneurInformationHandler = new ModifyEntrepreneurInformationHandler(null);
            this.testMessage = new Message(5433261, "1");
            this.result = modifyEntrepreneurInformationHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.Empty);
            Assert.That(this.result, Is.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar la clase ModifyEntrepreneurInformationHandlder al enviar el comando "/modificardatos".
        /// </summary>
        [Test]
        public void ModifyEntrepreneurInformationHandlderStartPermissionTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            ModifyEntrepreneurInformationHandler modifyEntrepreneurInformationHandler = new ModifyEntrepreneurInformationHandler(null);
            this.user1.HandlerState = Bot.State.Start;

            this.testMessage = new Message(5433261, "/modificardatos");
            this.result = modifyEntrepreneurInformationHandler.Handle(this.testMessage, out this.response);

            Assert.That(this.response, Is.EqualTo("Por favor, dinos que campo quieres modificar:\n1 - Ubicacion \n2 - Rubro"
                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que encarga de verificar la clase ModifyEntrepreneurInformationHandler en caso de enviarle el comando "1".
        /// </summary>
        [Test]
        public void ModifyEntrepreneurInformationHandlderChangeTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            ModifyEntrepreneurInformationHandler modifyEntrepreneurInformationHandler = new ModifyEntrepreneurInformationHandler(null);
            this.user1.HandlerState = Bot.State.AskingDataNumber;

            this.testMessage = new Message(5433261, "1");
            this.result = modifyEntrepreneurInformationHandler.Handle(this.testMessage, out this.response);

            Assert.That(this.response, Is.EqualTo("Qué campo quieres modificar? \n1 - Ciudad\n2 - Direccion \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que encarga de verificar la clase ModifyEntrepreneurInformationHandler en caso de enviar el comando "2".
        /// </summary>
        [Test]
        public void ModifyEntrepreneurInformationHandlderChangeHeaderTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;

            this.user1.HandlerState = Bot.State.AskingDataNumber;
            ModifyEntrepreneurInformationHandler modifyEntrepreneurInformationHandler = new ModifyEntrepreneurInformationHandler(null);
            this.testMessage = new Message(5433261, "1");
            this.result = modifyEntrepreneurInformationHandler.Handle(this.testMessage, out this.response);

            this.user1.HandlerState = Bot.State.AskingDataNumber;
            this.testMessage = new Message(5433261, "2");
            this.result = modifyEntrepreneurInformationHandler.Handle(this.testMessage, out this.response);

            Assert.That(this.response, Is.EqualTo("Cual es tu nuevo rubro?\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que encarga de verificar la clase ModifyEntrepreneurInformationHandler en caso de enviar el comando "23.
        /// </summary>
        [Test]
        public void ModifyEntrepreneurInformationHandlderChangeSpecializationTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;

            this.user1.HandlerState = Bot.State.AskingDataNumber;
            ModifyEntrepreneurInformationHandler modifyEntrepreneurInformationHandler = new ModifyEntrepreneurInformationHandler(null);

            this.testMessage = new Message(5433261, "3");
            this.result = modifyEntrepreneurInformationHandler.Handle(this.testMessage, out this.response);

            Assert.That(this.response, Is.EqualTo("Que desesa realizar? \n1 - Agregar una especialidad \n2 - Borrar una especialidad\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que encarga de verificar la clase ModifyEntrepreneurInformationHandler en caso de enviar el comando "4".
        /// </summary>
        [Test]
        public void ModifyEntrepreneurInformationHandlderChangeCertificationTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;

            this.user1.HandlerState = Bot.State.AskingDataNumber;
            ModifyEntrepreneurInformationHandler modifyEntrepreneurInformationHandler = new ModifyEntrepreneurInformationHandler(null);

            this.testMessage = new Message(5433261, "4");
            this.result = modifyEntrepreneurInformationHandler.Handle(this.testMessage, out this.response);

            Assert.That(this.response, Is.EqualTo("Que desea realizar? \n1 - Agregar una certificacion \n2 - Borrar una certificacion"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que encarga de verificar la clase ModifyEntrepreneurInformationHandler en caso de enviar un comando incorrecto.
        /// </summary>
        [Test]
        public void ModifyEntrepreneurInformationHandlderWrongCommandTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;

            this.user1.HandlerState = Bot.State.AskingDataNumber;
            ModifyEntrepreneurInformationHandler modifyEntrepreneurInformationHandler = new ModifyEntrepreneurInformationHandler(null);

            this.testMessage = new Message(5433261, "WrongCommand");
            this.result = modifyEntrepreneurInformationHandler.Handle(this.testMessage, out this.response);

            Assert.That(this.response, Is.EqualTo("Esta opcion no esta disponible. \nPor favor, dinos una opcion del 1 al 4.\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }
    }
}