using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    public class ModifyUserCertificationsHanlderTest
    {
        UserInfo user1;
        Message testMessage;
        String response;
        IHandler result;
        Company company;

        [SetUp]
        public void Setup()
        {
            user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(user1);
        }
        /// <summary>
        /// Arreglar
        /// </summary>
        [Test]
        public void ModifyUseraddCertificationHanlderTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, entrepreneur);

            ModifyUserCertificationsHandler modifyUserCertificationsHandler = new ModifyUserCertificationsHandler(null);
            user1.HandlerState = Bot.State.ChangingUserCertifications;
            testMessage = new Message(5433261, "1");
            result = modifyUserCertificationsHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Que certificacion quieres agregar?\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyUserdelateCertificationHanlderTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, entrepreneur);

            ModifyUserCertificationsHandler modifyUserCertificationsHandler = new ModifyUserCertificationsHandler(null);
            user1.HandlerState = Bot.State.ChangingUserCertifications;
            testMessage = new Message(5433261, "2");
            result = modifyUserCertificationsHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Que certificacion quieres eliminar?\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyUserCertificationWrongTextHanlderTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, entrepreneur);

            ModifyUserCertificationsHandler modifyUserCertificationsHandler = new ModifyUserCertificationsHandler(null);
            user1.HandlerState = Bot.State.ChangingUserCertifications;
            testMessage = new Message(5433261, "5");
            result = modifyUserCertificationsHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Esta opcion no esta disponible. \nPor favor, dinos una opcion del 1 al 2.\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyUserHasCertificationHanlderTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            entrepreneur.AddCertification("certification");
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, entrepreneur);

            ModifyUserCertificationsHandler modifyUserCertificationsHandler = new ModifyUserCertificationsHandler(null);
            user1.HandlerState = Bot.State.AddingUserCertification;
            testMessage = new Message(5433261, "certification");

            result = modifyUserCertificationsHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Esta certificacion ya fue agregada anteriormente.\nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyUserAddCertificationHanlderTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            entrepreneur.AddCertification("certification");
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, entrepreneur);

            ModifyUserCertificationsHandler modifyUserCertificationsHandler = new ModifyUserCertificationsHandler(null);
            user1.HandlerState = Bot.State.AddingUserCertification;
            testMessage = new Message(5433261, "NewCertification");

            result = modifyUserCertificationsHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Certificacion agregada correctamente. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyUserDelateCertificationHanlderTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            entrepreneur.AddCertification("certification");
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, entrepreneur);

            ModifyUserCertificationsHandler modifyUserCertificationsHandler = new ModifyUserCertificationsHandler(null);
            user1.HandlerState = Bot.State.DeletingUserCertification;
            testMessage = new Message(5433261, "certification");

            result = modifyUserCertificationsHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Certificacion eliminada correctamente. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyUserNoFindCertificationHanlderTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            entrepreneur.AddCertification("certification");
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, entrepreneur);

            ModifyUserCertificationsHandler modifyUserCertificationsHandler = new ModifyUserCertificationsHandler(null);
            user1.HandlerState = Bot.State.DeletingUserCertification;
            testMessage = new Message(5433261, "WrongText");

            result = modifyUserCertificationsHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Esta Certificacion no existe. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyUserCertificationEmptyTextHanlderTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            entrepreneur.AddCertification("certification");
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, entrepreneur);

            ModifyUserCertificationsHandler modifyUserCertificationsHandler = new ModifyUserCertificationsHandler(null);
            user1.HandlerState = Bot.State.DeletingUserCertification;
            testMessage = new Message(5433261, "");

            result = modifyUserCertificationsHandler.Handle(testMessage, out response);
            Assert.That(result, Is.Not.Null);
        }
    }
}