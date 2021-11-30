using System;
using Bot;
using NUnit.Framework;

namespace BotTests
{
    /// <summary>
    /// Clase ModifyUserHeaderHanlderTest se encarga de testear las funcionalidades de la clase ModifyUserHeaderHanlder.
    /// </summary>
    public class ModifyUserHeaderHanlderTest
    {
        private UserInfo user1;
        private Message testMessage;
        private String response;
        private IHandler result;

        /// <summary>
        /// Se inicializan todas las  variables que se van a utilizar en los test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            SessionRelated.Instance = null;
            this.user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(this.user1);
        }

        /// <summary>
        /// Test que se encarga de verificar la funcionalidad de actualizar la certificacion del emprendedor.
        /// </summary>
        [Test]
        public void ModifyUseraddCertificationHanlderTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(this.user1, entrepreneur);

            ModifyUserHeaderHandler modifyUserHeaderHandler = new(null);
            this.user1.HandlerState = Bot.State.ChangingUserHeader;
            this.testMessage = new Message(5433261, "Herrero");
            this.result = modifyUserHeaderHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Informacion actualizada. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el handler en caso que se le envíe un string vacío.
        /// </summary>
        [Test]
        public void ModifyUserCertificationEmptyTextHanlderTest()
        {
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            GeoLocation entrepreneurLocation = new("Camino Maldonado 2415", "Montevideo");
            EntrepreneurInfo entrepreneur = new("carpintero", entrepreneurLocation);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(this.user1, entrepreneur);

            ModifyUserHeaderHandler modifyUserHeaderHandler = new(null);

            // Cambio de estado
            this.user1.HandlerState = Bot.State.DeletingUserCertification;

            this.testMessage = new Message(5433261, "");
            this.result = modifyUserHeaderHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.result, Is.Null);
        }
    }
}