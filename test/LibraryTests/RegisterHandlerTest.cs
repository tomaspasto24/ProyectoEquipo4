using System;
using Bot;
using NUnit.Framework;

namespace BotTests
{
    /// <summary>
    /// Test de handler de registro.
    /// </summary>
    public class RegisterHandlerTest
    {
        private UserInfo user1;
        private Message testMessage;
        private String response;
        private IHandler result;
        private Company company;

        /// <summary>
        /// Se setea el sessionRelated.Instance en null para que no queden todas las variables anteriores cargadas en el mismo debido a que este es un singleton.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            SessionRelated.Instance = null;
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del handler al no tener el permiso necesario.
        /// </summary>
        [Test]
        public void RegisterHandlerNoPermissionTest()
        {
            this.user1 = new UserInfo("name1", 5433261);
            this.user1.Permissions = UserInfo.AdminPermissions;
            SessionRelated.Instance.AddNewUser(this.user1);
            RegisterHandler registerhandler = new(null);
            this.testMessage = new(5433261, "");

            this.result = registerhandler.Handle(this.testMessage, out this.response);
            Assert.That(this.result, Is.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del handler al intentar registrarse y tener el permiso necesario.
        /// </summary>
        [Test]
        public void RegisterHandlerHasPermissionTest()
        {
            this.user1 = new UserInfo("name1", 5433261);
            this.user1.Permissions = UserInfo.DefaultPermissions;
            SessionRelated.Instance.AddNewUser(this.user1);
            RegisterHandler registerhandler = new(null);
            this.user1.HandlerState = Bot.State.Start;
            this.testMessage = new(5433261, "/registro");

            this.result = registerhandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Inserta tu token de usuario empresa: "));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del handler  al validar el token.
        /// </summary>
        [Test]
        public void RegisterHandlerConfirmTokenTest()
        {
            this.user1 = new UserInfo("name1", 5433261);
            this.user1.Permissions = UserInfo.DefaultPermissions;
            SessionRelated.Instance.AddNewUser(this.user1);
            RegisterHandler registerhandler = new(null);
            this.user1.HandlerState = Bot.State.Start;
            this.testMessage = new(5433261, "/registro");
            this.result = registerhandler.Handle(this.testMessage, out this.response);

            // Agrego el token al dicc para que valide la empresa
            SessionRelated.Instance.DiccUserTokens.Add("IHaveAToken", this.company);
            this.user1.HandlerState = Bot.State.ConfirmingToken;
            // le paso el mensaje con mi id y el token para que lo busque en el dicc.
            this.testMessage = new(5433261, "IHaveAToken");

            this.result = this.result.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Token verificado, ahora eres un usuario empresa! :)"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del handler al no encontrar el token.
        /// </summary>
        [Test]
        public void RegisterHandlerTokenNoFndTest()
        {
            this.user1 = new UserInfo("name1", 5433261);
            this.user1.Permissions = UserInfo.DefaultPermissions;
            SessionRelated.Instance.AddNewUser(this.user1);
            RegisterHandler registerhandler = new(null);
            this.user1.HandlerState = Bot.State.Start;
            this.testMessage = new(5433261, "/registro");
            this.result = registerhandler.Handle(this.testMessage, out this.response);

            this.user1.HandlerState = Bot.State.ConfirmingToken;
            this.testMessage = new(5433261, "NoToken");

            this.result = this.result.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Disculpa, no hemos encontrado ese token :("));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del handler al ingresar un comando incorrecto.
        /// </summary>
        [Test]
        public void RegisterHandlerWrongTextTest()
        {
            this.user1 = new("name1", 5433261);
            this.user1.Permissions = UserInfo.AdminPermissions;
            SessionRelated.Instance.AddNewUser(this.user1);
            RegisterHandler registerhandler = new(null);
            this.user1.HandlerState = Bot.State.ConfirmingToken;
            this.testMessage = new Message(5433261, "WrongText");

            this.result = registerhandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo(string.Empty));
            Assert.That(this.result, Is.Null);
        }
    }
}