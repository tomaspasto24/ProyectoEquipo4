using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    public class RegisterCompanyUserTest
    {

        User usuario;
        Role role;
        Company company;
        GeoLocation location;
        SessionRelated sessionRelated;
        Message message;
        RegisterHandler handler;

        [SetUp]
        public void SetUp()
        {
            sessionRelated = SessionRelated.Instance;
            location = new GeoLocation("8 de octubre", "Montevideo", "Montevideo");
            company = new Company("Test", "itemTest", location, "093929434");
            role = new RoleEntrepreneur("name", 123, "heading", location, "certification", "specialization");
            usuario = new User("Seba", 123, role);
            sessionRelated.AddNewUser("Seba", 123, role);
            SessionRelated.DiccUserTokens.Add("testToken", company);

            message = new Message(usuario.Id, string.Empty);
            handler = new RegisterHandler(new RegisterCondition());
        }

        [Test]
        public void TestRegisterAsAEntrepreneurStep1()
        {
            message.Text = "/registro";
            string response;

            AbstractHandler result = handler.HandleRequest(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("¡Hola! ¿Cómo estás?"));
        }

        [Test]
        public void TestRegisterAsAEntrepreneurStep2()
        {

        }
    }
}