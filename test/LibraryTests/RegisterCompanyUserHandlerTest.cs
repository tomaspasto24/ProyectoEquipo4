using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    public class RegisterCompanyUserHandlerTest
    {

        User usuario;
        UserRelated userRelated;
        Role role;
        Company company;
        GeoLocation location;
        SessionRelated sessionRelated;
        Message message;
        RegisterHandler handler;
        RoleUserCompany newRole;

        [SetUp]
        public void SetUp()
        {
            location = new GeoLocation("8 de octubre", "Montevideo", "Montevideo");
            company = new Company("Test", "itemTest", location, "093929434");

            newRole = new RoleUserCompany(company, "Seba", 123);

            role = new RoleEntrepreneur("name", 123);
            usuario = new User("Seba", 123, role);
            userRelated = UserRelated.Instance;
            userRelated.User = usuario;

            sessionRelated = SessionRelated.Instance;
            sessionRelated.AddNewUser("Seba", 123, role);
            SessionRelated.DiccUserTokens.Add("IHaveAToken", company);

            message = new Message(usuario.Id, null);
            handler = new RegisterHandler(null);
        }

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

        [Test]
        public void TestTokenFound()
        {
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

        [Test]
        public void TestCancel()
        {
            handler.Cancel();

            Assert.That(handler.State, Is.EqualTo(RegisterHandler.RegisterState.Start));
            Assert.That(handler.Data.Token, Is.EqualTo(default(string)));
        }
    }
}