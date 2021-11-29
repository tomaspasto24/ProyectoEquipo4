using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    public class ModifyUserHeaderHanlderTest
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

            ModifyUserHeaderHandler modifyUserHeaderHandler = new ModifyUserHeaderHandler(null);
            user1.HandlerState = Bot.State.ChangingUserHeader;
            testMessage = new Message(5433261, "Herrero");
            result = modifyUserHeaderHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Informacion actualizada. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyUserCertificationEmptyTextHanlderTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, entrepreneur);

            ModifyUserHeaderHandler modifyUserHeaderHandler = new ModifyUserHeaderHandler(null);

            //Cambio de estado
            user1.HandlerState = Bot.State.DeletingUserCertification;

            testMessage = new Message(5433261, "");
            result = modifyUserHeaderHandler.Handle(testMessage, out response);
            Assert.That(result, Is.Null);
        }
    }
}