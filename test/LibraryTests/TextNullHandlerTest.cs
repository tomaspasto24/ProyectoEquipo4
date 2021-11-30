
using System;
using Bot;
using NUnit.Framework;

namespace BotTests
{

    /// <summary>
    /// Clase TextNullHandlerTest la cual se encarga de testear las funcionalidades de la clase TextNullHandler, se declaran las variables globales que se van a utilizar.
    /// </summary>
    public class TextNullHandlerTest
    {
        private UserInfo user1;
        private Message testMessage;
        private String response;
        private IHandler result;

        /// <summary>
        /// Test que se encarga de verificar la respuesta del handler en caso de que el mensaje que se ingrea sea null. 
        /// </summary>
        [Test]
        public void TextNullHandlerTest1()
        {
            this.user1 = new("name1", 5433261);
            this.user1.Permissions = UserInfo.DefaultPermissions;
            TextNullHandler textNullHandler = new(null);
            this.testMessage = new Message(5433261, null);

            NullReferenceException ex = Assert.Throws<NullReferenceException>(() => textNullHandler.Handle(this.testMessage, out this.response));
            Assert.That(ex.Message, Is.EqualTo("El mensaje no puede estar vacio, ni ser una imagen o video"));

        }

        /// <summary>
        /// Test que se encarga de verificar si el handler convierte el string ingresado en el formato deseadocy no sea null.
        /// </summary>
        [Test]
        public void TextNoNullHandlerTest()
        {
            this.user1 = new UserInfo("name1", 5433261);
            this.user1.Permissions = UserInfo.DefaultPermissions;
            TextNullHandler textNullHandler = new(null);
            this.testMessage = new Message(5433261, "PrueBa ");

            this.result = textNullHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.result, Is.Null);
            Assert.That(this.response, Is.EqualTo(String.Empty));
            Assert.That(this.testMessage.Text, Is.EqualTo("prueba"));
        }
    }
}
