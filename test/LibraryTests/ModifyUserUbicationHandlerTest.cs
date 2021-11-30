using System;
using Bot;
using NUnit.Framework;

namespace BotTests
{
    /// <summary>
    /// Clase ModifyUserUbicationHanlderTest que se encarga de testear las funcionalidades del handler ModifyUserUbicationHanlder.
    /// </summary>
    public class ModifyUserUbicationHanlderTest
    {
        private UserInfo user1;
        private Message testMessage;
        private String response;
        private IHandler result;

        /// <summary>
        /// Se inicializan las variables que se van a utilizar en los test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            SessionRelated.Instance = null;
            this.user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(this.user1);
        }

        /// <summary>
        /// Test que verifica el comportamiento del handler al intentar cambiar la ciudad en la que vive el "entrepreneur".
        /// </summary>
        [Test]
        public void ModifyUserChangeCityHanlderTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(this.user1, entrepreneur);

            ModifyUserUbicationHandler modifyUserUbicationHandler = new(null);
            this.user1.HandlerState = Bot.State.ChangingUserUbication;
            this.testMessage = new Message(5433261, "1");
            this.result = modifyUserUbicationHandler.Handle(this.testMessage, out this.response);


            Assert.That(this.response, Is.EqualTo("En que ciudad vives ahora?\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que verifica el comportamiento del handler al intentar cambiar la dirección en la que vive el "entrepreneur".
        /// </summary>
        [Test]
        public void ModifyUserChangeAdressHanlderTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(this.user1, entrepreneur);

            ModifyUserUbicationHandler modifyUserUbicationHandler = new(null);
            this.user1.HandlerState = Bot.State.ChangingUserUbication;
            this.testMessage = new Message(5433261, "2");
            this.result = modifyUserUbicationHandler.Handle(this.testMessage, out this.response);


            Assert.That(this.response, Is.EqualTo("Cual es tu nueva direccion?\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que verifica el comportamiento del handler al cambiar la ciudad en la que vive el "entrepreneur".
        /// </summary>
        [Test]
        public void ModifyUserCorrectChangeAdressHanlderTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(this.user1, entrepreneur);

            // Estado del handler anterior 
            this.user1.HandlerState = Bot.State.ChangingUserAddress;
            ModifyUserUbicationHandler modifyUserUbicationHandler = new(null);
            this.testMessage = new Message(5433261, "2");
            this.result = modifyUserUbicationHandler.Handle(this.testMessage, out this.response);

            // estado del handler para cambiar la dirección.
            this.user1.HandlerState = Bot.State.ChangingUserAddress;
            this.testMessage = new Message(5433261, "Rivera 2740");
            this.result = this.result.Handle(this.testMessage, out this.response);


            Assert.That(this.response, Is.EqualTo("Informacion actualizada. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que verifica el comportamiento del handler al cambiar la ciudad en la que vive el "entrepreneur".
        /// </summary>
        [Test]
        public void ModifyUserCorrectChangeCityHanlderTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(this.user1, entrepreneur);

            // Estado del handler anterior 
            this.user1.HandlerState = Bot.State.ChangingUserAddress;
            ModifyUserUbicationHandler modifyUserUbicationHandler = new(null);
            this.testMessage = new Message(5433261, "1");
            this.result = modifyUserUbicationHandler.Handle(this.testMessage, out this.response);

            // Estado del handler para cambiar la ciudad.
            this.user1.HandlerState = Bot.State.ChangingUserCity;
            this.testMessage = new Message(5433261, "Durazno");
            this.result = this.result.Handle(this.testMessage, out this.response);

            Assert.That(this.response, Is.EqualTo("Informacion actualizada. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que verifica el comportamiento del handler al ingresar un comando incorrecto.
        /// </summary>
        [Test]
        public void ModifyUserChangeWrongTextHanlderTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(this.user1, entrepreneur);

            ModifyUserUbicationHandler modifyUserUbicationHandler = new(null);
            this.user1.HandlerState = Bot.State.ChangingUserUbication;
            this.testMessage = new Message(5433261, "4");
            this.result = modifyUserUbicationHandler.Handle(this.testMessage, out this.response);

            Assert.That(this.response, Is.EqualTo("Esta opcion no esta disponible. \nPor favor, dinos una opcion del 1 al 2.\nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que verifica el comportamiento del handler al ingresar una string vacía"
        /// </summary>
        [Test]
        public void ModifyUserChangeEmptyTextHanlderTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(this.user1, entrepreneur);

            ModifyUserUbicationHandler modifyUserUbicationHandler = new(null);
            this.user1.HandlerState = Bot.State.ChangingUserUbication;
            this.testMessage = new Message(5433261, "");
            this.result = modifyUserUbicationHandler.Handle(this.testMessage, out this.response);

            Assert.That(this.result, Is.Not.Null);
        }
    }
}