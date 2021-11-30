using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    public class ModifyEntrepreneurInformationHandlderTest
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
        public void ModifyEntrepreneurInformationHandlderNoHasPermissionTest()
        {
            user1.Permissions = UserInfo.DefaultPermissions;
            ModifyEntrepreneurInformationHandler modifyEntrepreneurInformationHandler = new ModifyEntrepreneurInformationHandler(null);
            testMessage = new Message(5433261, "1");
            result = modifyEntrepreneurInformationHandler.Handle(testMessage, out response);
            Assert.That(response, Is.Empty);
            Assert.That(result, Is.Null);
        }
        [Test]
        public void ModifyEntrepreneurInformationHandlderStartPermissionTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            ModifyEntrepreneurInformationHandler modifyEntrepreneurInformationHandler = new ModifyEntrepreneurInformationHandler(null);
            user1.HandlerState = Bot.State.Start;

            testMessage = new Message(5433261, "/modificardatos");
            result = modifyEntrepreneurInformationHandler.Handle(testMessage, out response);

            Assert.That(response, Is.EqualTo("Por favor, dinos que campo quieres modificar:\n1 - Ubicacion \n2 - Rubro"
                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyEntrepreneurInformationHandlderChangeTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            ModifyEntrepreneurInformationHandler modifyEntrepreneurInformationHandler = new ModifyEntrepreneurInformationHandler(null);
            user1.HandlerState = Bot.State.AskingDataNumber;

            testMessage = new Message(5433261, "1");
            result = modifyEntrepreneurInformationHandler.Handle(testMessage, out response);

            Assert.That(response, Is.EqualTo("Qué campo quieres modificar? \n1 - Ciudad\n2 - Direccion \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyEntrepreneurInformationHandlderChangeHeaderTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;

            user1.HandlerState = Bot.State.AskingDataNumber;
            ModifyEntrepreneurInformationHandler modifyEntrepreneurInformationHandler = new ModifyEntrepreneurInformationHandler(null);
            // testMessage = new Message(5433261, "1");
            //result = modifyEntrepreneurInformationHandler.Handle(testMessage, out response);

            //user1.HandlerState = Bot.State.AskingDataNumber;
            testMessage = new Message(5433261, "2");
            result = modifyEntrepreneurInformationHandler.Handle(testMessage, out response);

            Assert.That(response, Is.EqualTo("Cual es tu nuevo rubro?\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void ModifyEntrepreneurInformationHandlderChangeSpecializationTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;

            user1.HandlerState = Bot.State.AskingDataNumber;
            ModifyEntrepreneurInformationHandler modifyEntrepreneurInformationHandler = new ModifyEntrepreneurInformationHandler(null);

            testMessage = new Message(5433261, "3");
            result = modifyEntrepreneurInformationHandler.Handle(testMessage, out response);

            Assert.That(response, Is.EqualTo("Que desesa realizar? \n1 - Agregar una especialidad \n2 - Borrar una especialidad\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void ModifyEntrepreneurInformationHandlderChangeCertificationTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;

            user1.HandlerState = Bot.State.AskingDataNumber;
            ModifyEntrepreneurInformationHandler modifyEntrepreneurInformationHandler = new ModifyEntrepreneurInformationHandler(null);

            testMessage = new Message(5433261, "4");
            result = modifyEntrepreneurInformationHandler.Handle(testMessage, out response);

            Assert.That(response, Is.EqualTo("Que desea realizar? \n1 - Agregar una certificacion \n2 - Borrar una certificacion"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyEntrepreneurInformationHandlderWrongCommandTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;

            user1.HandlerState = Bot.State.AskingDataNumber;
            ModifyEntrepreneurInformationHandler modifyEntrepreneurInformationHandler = new ModifyEntrepreneurInformationHandler(null);

            testMessage = new Message(5433261, "WrongCommand");
            result = modifyEntrepreneurInformationHandler.Handle(testMessage, out response);

            Assert.That(response, Is.EqualTo("Esta opcion no esta disponible. \nPor favor, dinos una opcion del 1 al 4.\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }

    }
}