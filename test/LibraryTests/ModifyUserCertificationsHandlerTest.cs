using System;
using Bot;
using NUnit.Framework;

namespace BotTests
{
    /// <summary>
    /// Clase ModifyUserCertificationsHandlerTest que se encarga de testear todas las funcionalidades del handler "ModifyUserCertificationsHandlerTest".
    /// </summary>
    public class ModifyUserCertificationsHandlerTest
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
            GeoLocation entrepreneurLocation = new("Camino Maldonado 2415", "Montevideo");
            this.entrepreneur = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(this.user1, this.entrepreneur);
        }

        /// <summary>
        /// Test que se encarga de verificar ModifyUseraddCertificationHanlder en caso de enviar el comando "1".
        /// </summary>
        [Test]
        public void ModifyUserAddNewCertificationHanlderTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            ModifyUserCertificationsHandler modifyUserCertificationsHandler = new ModifyUserCertificationsHandler(null);
            this.user1.HandlerState = Bot.State.ChangingUserCertifications;
            this.testMessage = new Message(5433261, "1");
            this.result = modifyUserCertificationsHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Que certificacion quieres agregar?\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el ModifyUseraddCertificationHanlder en caso que se le ingrese el comando "2".
        /// </summary>
        [Test]
        public void ModifyUserdelateCertificationHanlderTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            ModifyUserCertificationsHandler modifyUserCertificationsHandler = new ModifyUserCertificationsHandler(null);
            this.user1.HandlerState = Bot.State.ChangingUserCertifications;
            this.testMessage = new Message(5433261, "2");
            this.result = modifyUserCertificationsHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Que certificacion quieres eliminar?\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el ModifyUseraddCertificationHanlder en caso que se le ingrese un comando incorrecto.
        /// </summary>
        [Test]
        public void ModifyUserCertificationWrongTextHanlderTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            ModifyUserCertificationsHandler modifyUserCertificationsHandler = new ModifyUserCertificationsHandler(null);
            this.user1.HandlerState = Bot.State.ChangingUserCertifications;
            this.testMessage = new Message(5433261, "5");
            this.result = modifyUserCertificationsHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Esta opcion no esta disponible. \nPor favor, dinos una opcion del 1 al 2.\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el ModifyUseraddCertificationHanlder en caso que se le ingrese una certificación ya agregada anteriormente.
        /// </summary>
        [Test]
        public void ModifyUserHasCertificationHanlderTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            this.entrepreneur.AddCertification("certification");
            ModifyUserCertificationsHandler modifyUserCertificationsHandler = new ModifyUserCertificationsHandler(null);
            this.user1.HandlerState = Bot.State.AddingUserCertification;
            this.testMessage = new Message(5433261, "certification");
            this.result = modifyUserCertificationsHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Esta certificacion ya fue agregada anteriormente.\nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el ModifyUseraddCertificationHanlder en caso que se le ingrese una nueva certificación.
        /// </summary>
        [Test]
        public void ModifyUserAddCertificationHanlderTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            this.entrepreneur.AddCertification("certification");

            ModifyUserCertificationsHandler modifyUserCertificationsHandler = new ModifyUserCertificationsHandler(null);
            this.user1.HandlerState = Bot.State.AddingUserCertification;
            this.testMessage = new Message(5433261, "NewCertification");
            this.result = modifyUserCertificationsHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Certificacion agregada correctamente. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el ModifyUseraddCertificationHanlder en caso que se le ingrese una certificación para eliminarla ya agregada anteriormente.
        /// </summary>
        [Test]
        public void ModifyUserDelateCertificationHanlderTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            this.entrepreneur.AddCertification("certification");
            ModifyUserCertificationsHandler modifyUserCertificationsHandler = new ModifyUserCertificationsHandler(null);
            this.user1.HandlerState = Bot.State.DeletingUserCertification;
            this.testMessage = new Message(5433261, "certification");
            this.result = modifyUserCertificationsHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Certificacion eliminada correctamente. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el ModifyUseraddCertificationHanlder en caso que se le ingrese una certificación que no existe para eliminarla.
        /// </summary>
        [Test]
        public void ModifyUserNoFindCertificationHanlderTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            this.entrepreneur.AddCertification("certification");
            ModifyUserCertificationsHandler modifyUserCertificationsHandler = new ModifyUserCertificationsHandler(null);

            this.testMessage = new Message(5433261, "2");
            this.user1.HandlerState = Bot.State.ChangingUserCertifications;
            this.result = modifyUserCertificationsHandler.Handle(this.testMessage, out this.response);

            this.testMessage = new Message(5433261, "WrongText");
            this.result = this.result.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Esta Certificacion no existe. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el ModifyUseraddCertificationHanlder en caso que se le ingrese una string vacía.
        /// </summary>
        [Test]
        public void ModifyUserCertificationEmptyTextHanlderTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            this.entrepreneur.AddCertification("certification");
            ModifyUserCertificationsHandler modifyUserCertificationsHandler = new ModifyUserCertificationsHandler(null);
            this.user1.HandlerState = Bot.State.DeletingUserCertification;
            this.testMessage = new Message(5433261, "");

            this.result = modifyUserCertificationsHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.result, Is.Not.Null);
        }
    }
}