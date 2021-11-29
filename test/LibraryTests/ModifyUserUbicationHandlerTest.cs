using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    public class ModifyUserUbicationHanlderTest
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
        public void ModifyUserChangeCityHanlderTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, entrepreneur);

            ModifyUserUbicationHandler modifyUserUbicationHandler = new ModifyUserUbicationHandler(null);
            user1.HandlerState = Bot.State.ChangingUserUbication;
            testMessage = new Message(5433261, "1");
            result = modifyUserUbicationHandler.Handle(testMessage, out response);


            Assert.That(response, Is.EqualTo("En que ciudad vives ahora?\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyUserChangeAdressHanlderTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, entrepreneur);

            ModifyUserUbicationHandler modifyUserUbicationHandler = new ModifyUserUbicationHandler(null);
            user1.HandlerState = Bot.State.ChangingUserUbication;
            testMessage = new Message(5433261, "2");
            result = modifyUserUbicationHandler.Handle(testMessage, out response);


            Assert.That(response, Is.EqualTo("Cual es tu nueva direccion?\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void ModifyUserCorrectChangeAdressHanlderTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, entrepreneur);

            //Estado del handler anterior 
            user1.HandlerState = Bot.State.ChangingUserAddress;
            ModifyUserUbicationHandler modifyUserUbicationHandler = new ModifyUserUbicationHandler(null);
            testMessage = new Message(5433261, "2");
            result = modifyUserUbicationHandler.Handle(testMessage, out response);

            //estado del handler para cambiar la dirección.
            user1.HandlerState = Bot.State.ChangingUserAddress;
            testMessage = new Message(5433261, "Rivera 2740");
            result = result.Handle(testMessage, out response);


            Assert.That(response, Is.EqualTo("Informacion actualizada. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyUserCorrectChangeCityHanlderTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, entrepreneur);

            //Estado del handler anterior 
            user1.HandlerState = Bot.State.ChangingUserAddress;
            ModifyUserUbicationHandler modifyUserUbicationHandler = new ModifyUserUbicationHandler(null);
            testMessage = new Message(5433261, "1");
            result = modifyUserUbicationHandler.Handle(testMessage, out response);

            //estado del handler para cambiar la ciudad.
            user1.HandlerState = Bot.State.ChangingUserCity;
            testMessage = new Message(5433261, "Durazno");
            result = result.Handle(testMessage, out response);


            Assert.That(response, Is.EqualTo("Informacion actualizada. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyUserChangeWrongTextHanlderTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, entrepreneur);

            ModifyUserUbicationHandler modifyUserUbicationHandler = new ModifyUserUbicationHandler(null);
            user1.HandlerState = Bot.State.ChangingUserUbication;
            testMessage = new Message(5433261, "4");
            result = modifyUserUbicationHandler.Handle(testMessage, out response);


            Assert.That(response, Is.EqualTo("Esta opcion no esta disponible. \nPor favor, dinos una opcion del 1 al 2.\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ModifyUserChangeEmptyTextHanlderTest()
        {
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new EntrepreneurInfo("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, entrepreneur);

            ModifyUserUbicationHandler modifyUserUbicationHandler = new ModifyUserUbicationHandler(null);
            user1.HandlerState = Bot.State.ChangingUserUbication;
            testMessage = new Message(5433261, "");
            result = modifyUserUbicationHandler.Handle(testMessage, out response);

            Assert.That(result, Is.Not.Null);
        }
    }
}