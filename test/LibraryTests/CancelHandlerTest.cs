using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    public class CancelHandlerTest
    {
        private UserInfo user1;
        private Message testmessage;
        private String response;
        private IHandler result;

        /// <summary>
        /// Test que verifica el funcionamiento del handler al enviar el comando "/cancelar".
        /// </summary>
        [Test]
        public void CancelHandlerMessageTest()
        {
            this.user1 = new("name1", 5433261);
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            SessionRelated.Instance.AddNewUser(this.user1);
            CancelHandler cancelHandler = new(null);
            this.testmessage = new Message(5433261, "/cancelar");

            this.result = cancelHandler.Handle(this.testmessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Operación cancelada."));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que verifica el funcionamiento del handler al enviar un comando incorrecto.
        /// </summary>

        [Test]
        public void CancelHandlerWrongTextTest()
        {
            this.user1 = new("name1", 5433261);
            this.user1.Permissions = UserInfo.EntrepreneurPermissions;
            SessionRelated.Instance.AddNewUser(this.user1);
            CancelHandler cancelHandler = new(null);
            this.testmessage = new Message(5433261, "/WrongText");

            this.result = cancelHandler.Handle(this.testmessage, out this.response);
            Assert.That(this.result, Is.Null);
            Assert.That(this.response, Is.EqualTo(String.Empty)); //verifico que la respuesta sea un string vacio
        }
    }
}