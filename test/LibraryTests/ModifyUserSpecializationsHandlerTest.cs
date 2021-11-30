using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    public class ModifyUserSpecializationsHandlerTest
    {
        UserInfo user1;
        Message testMessage;
        String response;
        IHandler result;
        EntrepreneurInfo entrepreneur;

        [SetUp]
        public void Setup()
        {
            SessionRelated.Instance = null;
            user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(user1);
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            entrepreneur = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, entrepreneur);

        }

        [Test]
        public void ModifyEntrepreneurInformationHandldlerAddSpecializationTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            user1.HandlerState = Bot.State.ChangingUserSpecializations;
            ModifyUserSpecializationsHandler modifyUserSpecializationsHandler = new ModifyUserSpecializationsHandler(null);
            testMessage = new Message(5433261, "1");

            result = modifyUserSpecializationsHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Que especialidad quieres agregar?\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyEntrepreneurInformationHandldlerDelateSpecializationTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            user1.HandlerState = Bot.State.ChangingUserSpecializations;
            ModifyUserSpecializationsHandler modifyUserSpecializationsHandler = new ModifyUserSpecializationsHandler(null);
            testMessage = new Message(5433261, "2");

            result = modifyUserSpecializationsHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Que especialidad quieres eliminar?\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyEntrepreneurInformationHandldlerWrongCommandTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            user1.HandlerState = Bot.State.ChangingUserSpecializations;
            ModifyUserSpecializationsHandler modifyUserSpecializationsHandler = new ModifyUserSpecializationsHandler(null);
            testMessage = new Message(5433261, "WrongCommand");

            result = modifyUserSpecializationsHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Esta opcion no esta disponible. \nPor favor, dinos una opcion del 1 al 2.\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }

        /// <summary>
        /// Test que verifica si la specialization ya fue agregada anteriormente.
        /// </summary>
        [Test]
        public void ModifyEntrepreneurInformationHandldlerAlreadyAddSpecializationTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            entrepreneur.AddSpecialization("Specialization");
            user1.HandlerState = Bot.State.AddingUserSpecializations;
            ModifyUserSpecializationsHandler modifyUserSpecializationsHandler = new ModifyUserSpecializationsHandler(null);
            testMessage = new Message(5433261, "Specialization");

            result = modifyUserSpecializationsHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Esta especialidad ya fue agregada anteriormente.\nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void ModifyEntrepreneurInformationHandldleraddSpecializationTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            entrepreneur.AddSpecialization("Specialization");
            user1.HandlerState = Bot.State.AddingUserSpecializations;
            ModifyUserSpecializationsHandler modifyUserSpecializationsHandler = new ModifyUserSpecializationsHandler(null);
            testMessage = new Message(5433261, "NewSpecialization");

            result = modifyUserSpecializationsHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Especialidad agregada correctamente. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void ModifyEntrepreneurInformationHandldlerdelateSpecializationTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            entrepreneur.AddSpecialization("Specialization");

            user1.HandlerState = Bot.State.DeletingUserSpecializations;
            ModifyUserSpecializationsHandler modifyUserSpecializationsHandler = new ModifyUserSpecializationsHandler(null);
            testMessage = new Message(5433261, "Specialization");

            result = modifyUserSpecializationsHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Especialidad eliminada correctamente. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyEntrepreneurInformationHandldlerWrongSpecializationTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            entrepreneur.AddSpecialization("Specialization");

            user1.HandlerState = Bot.State.DeletingUserSpecializations;
            ModifyUserSpecializationsHandler modifyUserSpecializationsHandler = new ModifyUserSpecializationsHandler(null);
            testMessage = new Message(5433261, "WrongSpecialization");

            result = modifyUserSpecializationsHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Esta especialidad no existe. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyEntrepreneurInformationHandldlerEmptyTextTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            entrepreneur.AddSpecialization("Specialization");

            user1.HandlerState = Bot.State.DeletingUserSpecializations;
            ModifyUserSpecializationsHandler modifyUserSpecializationsHandler = new ModifyUserSpecializationsHandler(null);
            testMessage = new Message(5433261, "");

            result = modifyUserSpecializationsHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Esta especialidad no existe. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                    + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
    }
}