using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    /// <summary>
    /// Clase para testear el CommandHandler
    /// </summary>
    public class CommandHandlerTest
    {

        User user;
        Role role;
        UserRelated userRelated;
        SessionRelated sessionRelated;
        Message message;
        CommandHandler handler;

        /// <summary>
        /// Metodo SetUp de los tests
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            role = new RoleAdmin("name", 123);
            user = new User("Seba", 123, role);
            userRelated = UserRelated.Instance;
            userRelated.User = user;

            sessionRelated = SessionRelated.Instance;
            sessionRelated.AddNewUser("Seba", 123, role);

            message = new Message(user.Id, null);
            handler = new CommandHandler(null);
        }
        
        /// <summary>
        /// Test para probar los comandos que tiene un admin
        /// </summary>
        [Test]
        public void TestAdminCommandHandler()
        {
            message.Text = "/comandos";
            string response;

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Estos son todos los comandos: \n/comandos\n/hola\nexit\n/generartoken\n"));
        }

        /// <summary>
        /// Test para probar los comandos que tiene un UserCompany
        /// </summary>
        [Test]
        public void TestRoleUserCompanyCommandHandler()
        {
            GeoLocation location = new GeoLocation("adress", "city");
            Company company = new Company("nombre", "rubro", location, "contacto");

            user.ChangeRoleToUserCompany(company);

            message.Text = "/comandos";
            string response;

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Estos son todos los comandos: \n/comandos\n/hola\nexit\n/reporte\n/publicar\n"));
        }

        /// <summary>
        /// Test para probar los comandos que tiene un Emprendedor
        /// </summary>
        [Test]
        public void TestRoleEntrepreneurCommandHandler()
        {
            GeoLocation location = new GeoLocation("adress", "city");

            user.ChangeRoleToEntrepreneur("heading", location, "certification", "specialization");

            message.Text = "/comandos";
            string response;

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Estos son todos los comandos: \n/comandos\n/registro\n/hola\nexit\n/busqueda\n/reporte\n/contacto\n/infoemprendedor\n"));
        }
    }
}