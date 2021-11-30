using System;
using Bot;
using NUnit.Framework;

namespace BotTests
{
    /// <summary>
    /// Clase DefaultRoleHandlerTest que se encarga de testear las funcionalidades del handler DefaultRoleHandler.
    /// </summary>

    public class DefaultRoleHandlerTest
    {
        private UserInfo user1;
        private Message testMessage;
        private String response;
        private IHandler result;

        /// <summary>
        /// Se inicializan las variables que se usan en los test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            SessionRelated.Instance = null;
            this.user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(this.user1);
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del handler al enviar el comando "/default".
        /// </summary>
        [Test]
        public void DefaultRoleHandlerDefaultTest()
        {
            this.user1.Permissions = UserInfo.UserCompanyPermissions;
            DefaultRoleHandler defaultRoleHandler = new(null);
            this.testMessage = new Message(5433261, "/default");

            this.result = defaultRoleHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.result, Is.Not.Null);
            Assert.That(this.response, Is.EqualTo("ahora eres default"));
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del handler al enviar un comando incorrecto.
        /// </summary>
        [Test]
        public void DefaultRoleHandlerWrongCommandTest()
        {
            this.user1.Permissions = UserInfo.UserCompanyPermissions;
            DefaultRoleHandler defaultRoleHandler = new(null);
            this.testMessage = new Message(5433261, "/WrongCommand");

            this.result
             = defaultRoleHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.result, Is.Null);
            Assert.That(this.response, Is.Empty);
        }
    }
}