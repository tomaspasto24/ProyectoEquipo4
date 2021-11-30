using System;
using Bot;
using NUnit.Framework;

namespace BotTests
{
    /// <summary>
    /// clase DefaultHandlerTest la cual se encarga de testear las funcionalidades del handler DefaultHandler
    /// </summary>

    public class DefaultHandlerTest
    {
        private UserInfo user1;
        private Message testmessage;
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
        /// Test que se encarga de verificar la respuesta del handler al enviar el comando "/Command".
        /// </summary>
        [Test]
        public void DefaultHandlerSlashTest()
        {
            this.user1.Permissions = UserInfo.UserCompanyPermissions;
            DefaultHandler defaultHandler = new(null);
            this.testmessage = new Message(5433261, "/Command");

            this.result = defaultHandler.Handle(this.testmessage, out this.response);
            Assert.That(this.result, Is.Not.Null);
            Assert.That(this.response, Is.EqualTo("Tu comando no fue encontrado o no tienes el rango necesario para utilizarlo."));
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del handler al enviar un comando incorrecto.
        /// </summary>
        [Test]
        public void DefaultHandlerWithOutSlashTest()
        {
            this.user1.Permissions = UserInfo.UserCompanyPermissions;
            DefaultHandler defaultHandler = new(null);
            this.testmessage = new Message(5433261, "Command");

            this.result = defaultHandler.Handle(this.testmessage, out this.response);
            Assert.That(this.result, Is.Not.Null);
            Assert.That(this.response, Is.EqualTo("Disculpa, no te entiendo"));
        }
    }
}