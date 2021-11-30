using System;
using Bot;
using NUnit.Framework;

namespace BotTests
{
    /// <summary>
    /// Test para modificar las especificaciones del Emprendedor desde el handler.
    /// </summary>
    public class ModifyUserSpecializationsHandlerTest
    {
        private UserInfo user1;
        private Message testMessage;
        private String response;
        private IHandler result;
        private EntrepreneurInfo entrepreneur;

        /// <summary>
        /// Se inicializan las variables que se van a utilizar en los tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            SessionRelated.Instance = null;
            this.user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(this.user1);
            GeoLocation entrepreneurLocation = new("Camino Maldonado 2415", "Montevideo");
            this.entrepreneur = new("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(this.user1, this.entrepreneur);
        }

        /// <summary>
        /// Test que se encarga de verificar la funcionalidad del handler en caso que se le quiera añadir una "specialization".
        /// </summary>
        [Test]
        public void ModifyEntrepreneurInformationHandldlerAddSpecializationTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            this.user1.HandlerState = Bot.State.ChangingUserSpecializations;
            ModifyUserSpecializationsHandler modifyUserSpecializationsHandler = new(null);
            this.testMessage = new Message(5433261, "1");

            this.result = modifyUserSpecializationsHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Que especialidad quieres agregar?\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el handler en caso que se quiera borrar una "specialization".
        /// </summary>
        [Test]
        public void ModifyEntrepreneurInformationHandldlerDelateSpecializationTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            this.user1.HandlerState = Bot.State.ChangingUserSpecializations;
            ModifyUserSpecializationsHandler modifyUserSpecializationsHandler = new(null);
            this.testMessage = new Message(5433261, "2");

            this.result = modifyUserSpecializationsHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Que especialidad quieres eliminar?\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el handler en caso que se ingrese un comando incorrecto.
        /// </summary>
        [Test]
        public void ModifyEntrepreneurInformationHandldlerWrongCommandTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            this.user1.HandlerState = Bot.State.ChangingUserSpecializations;
            ModifyUserSpecializationsHandler modifyUserSpecializationsHandler = new(null);
            this.testMessage = new Message(5433261, "WrongCommand");

            this.result = modifyUserSpecializationsHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Esta opcion no esta disponible. \nPor favor, dinos una opcion del 1 al 2.\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que verifica si la "specialization" ya fue agregada anteriormente.
        /// </summary>
        [Test]
        public void ModifyEntrepreneurInformationHandldlerAlreadyAddSpecializationTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            this.entrepreneur.AddSpecialization("Specialization");
            this.user1.HandlerState = Bot.State.AddingUserSpecializations;
            ModifyUserSpecializationsHandler modifyUserSpecializationsHandler = new(null);
            this.testMessage = new Message(5433261, "Specialization");

            this.result = modifyUserSpecializationsHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Esta especialidad ya fue agregada anteriormente.\nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar sel handler en caso de agregar la "specialization"
        /// </summary>
        [Test]
        public void ModifyEntrepreneurInformationHandldleraddSpecializationTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            this.entrepreneur.AddSpecialization("Specialization");
            this.user1.HandlerState = Bot.State.AddingUserSpecializations;
            ModifyUserSpecializationsHandler modifyUserSpecializationsHandler = new(null);
            this.testMessage = new Message(5433261, "NewSpecialization");

            this.result = modifyUserSpecializationsHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Especialidad agregada correctamente. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar si se elimina la "Specialization" correctamente.
        /// </summary>
        [Test]
        public void ModifyEntrepreneurInformationHandldlerdelateSpecializationTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            this.entrepreneur.AddSpecialization("Specialization");

            this.user1.HandlerState = Bot.State.DeletingUserSpecializations;
            ModifyUserSpecializationsHandler modifyUserSpecializationsHandler = new(null);
            this.testMessage = new Message(5433261, "Specialization");

            this.result = modifyUserSpecializationsHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Especialidad eliminada correctamente. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar 
        /// </summary>
        [Test]
        public void ModifyEntrepreneurInformationHandldlerWrongSpecializationTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            this.entrepreneur.AddSpecialization("Specialization");

            this.user1.HandlerState = Bot.State.DeletingUserSpecializations;
            ModifyUserSpecializationsHandler modifyUserSpecializationsHandler = new(null);
            this.testMessage = new Message(5433261, "WrongSpecialization");

            this.result = modifyUserSpecializationsHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Esta especialidad no existe. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar la respuesta del handler en caso que se le ingrese un string vacio a la hora de modificar la "Specialization".
        /// </summary>
        [Test]
        public void ModifyEntrepreneurInformationHandldlerEmptyTextTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            this.entrepreneur.AddSpecialization("Specialization");

            this.user1.HandlerState = Bot.State.DeletingUserSpecializations;
            ModifyUserSpecializationsHandler modifyUserSpecializationsHandler = new(null);
            this.testMessage = new(5433261, "");

            this.result = modifyUserSpecializationsHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Esta especialidad no existe. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                    + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }
    }
}