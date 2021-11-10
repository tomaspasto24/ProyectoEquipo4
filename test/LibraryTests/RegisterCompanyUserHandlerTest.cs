using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    /// <summary>
    /// Clase para testear el RegisterCompanyUserHandler
    /// </summary>
    public class RegisterCompanyUserHandlerTest
    {

        User user;
        UserRelated userRelated;
        Role role;
        Company company;
        GeoLocation location;
        SessionRelated sessionRelated;
        Message message;
        RegisterHandler handler;
        RoleUserCompany newRole;

        /// <summary>
        /// Metodo SetUp para los tests
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            location = new GeoLocation("8 de octubre", "Montevideo");
            company = new Company("Test", "itemTest", location, "093929434");

            newRole = new RoleUserCompany(company, "Seba", 123);

            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2416", "Montevideo");
            role = new RoleEntrepreneur("name", 123, "carpintero", entrepreneurLocation, "oficial", "lustrado");
            user = new User("Seba", 123, role);
            userRelated = UserRelated.Instance;
            userRelated.User = user;

            sessionRelated = SessionRelated.Instance;
            sessionRelated.AddNewUser("Seba", 123, role);

            message = new Message(user.Id, null);
            handler = new RegisterHandler(null);
        }

        /// <summary>
        /// Test para probar que el RegisterHandler actua
        /// </summary>
        [Test]
        public void TestRegisterHandled()
        {
            message.Text = "/registro";
            string response;

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(handler.State, Is.EqualTo(RegisterHandler.RegisterState.ConfirmingToken));
            Assert.That(response, Is.EqualTo("Inserta tu token de usuario empresa: "));
        }

        /// <summary>
        /// Test para probar que pasa cuando no se encuentra un token
        /// </summary>
        [Test]
        public void TestNoTokenFound()
        {
            message.Text = "/registro";
            string response;
            handler.Handle(message, out response);

            message.Text = "IHaveNoToken";
            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Disculpa, no hemos encontrado ese token :("));
            Assert.That(handler.State, Is.EqualTo(RegisterHandler.RegisterState.Start));
        }

        /// <summary>
        /// Test para probar que pasa cuando se encuentra un token
        /// </summary>
        [Test]
        public void TestTokenFound()
        {
            //SessionRelated.DiccUserTokens.Add("IHaveAToken", company);

            message.Text = "/registro";
            string response;
            handler.Handle(message, out response);

            message.Text = "IHaveAToken";
            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Token verificado, ahora eres un usuario empresa! :)"));
            Assert.That(handler.State, Is.EqualTo(RegisterHandler.RegisterState.Start));
            // Chequear si quedan los roles cambiados
        }

        /// <summary>
        /// Test para probar el Cancel del handler
        /// </summary>
        [Test]
        public void TestCancel()
        {
            handler.Cancel();

            Assert.That(handler.State, Is.EqualTo(RegisterHandler.RegisterState.Start));
            Assert.That(handler.Data.Token, Is.EqualTo(default(string)));
        }
    }
}