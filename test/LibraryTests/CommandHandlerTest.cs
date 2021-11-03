using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    public class CommandHandlerTest
    {

        User usuario;
        Role role;
        UserRelated userRelated;
        SessionRelated sessionRelated;
        Message message;
        CommandHandler handler;

        [SetUp]
        public void SetUp()
        {
            role = new RoleAdmin("name", 123);
            usuario = new User("Seba", 123, role);
            userRelated = UserRelated.Instance;
            userRelated.User = usuario;

            sessionRelated = SessionRelated.Instance;
            sessionRelated.AddNewUser("Seba", 123, role);

            message = new Message(usuario.Id, null);
            handler = new CommandHandler(null);
        }

        [Test]
        public void TestAdminCommandHandler()
        {
            message.Text = "/comandos";
            string response;

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Estos son todos los comandos: \n/comandos\n/hola\nexit\n/generartoken\n"));
        }

        [Test]
        public void TestRoleUserCompanyCommandHandler()
        {
            GeoLocation location = new GeoLocation("adress", "city", "city");
            Company company = new Company("nombre", "rubro", location, "contacto");

            userRelated.ChangeRoleToUserCompany(company);

            message.Text = "/comandos";
            string response;

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Estos son todos los comandos: \n/comandos\n/hola\nexit\n/reporte\n/publicar\n"));
        }

        [Test]
        public void TestRoleEntrepreneurCommandHandler()
        {
            GeoLocation location = new GeoLocation("adress", "city", "city");

            userRelated.ChangeRoleToEntrepreneur("heading", location, "certification", "specialization");
            
            message.Text = "/comandos";
            string response;

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Estos son todos los comandos: \n/comandos\n/registro\n/hola\nexit\n/busqueda\n/reporte\n/contacto\n/infoemprendedor\n"));
        }
    }
}