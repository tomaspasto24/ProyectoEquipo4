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
        UserInfo user;
        SessionRelated sessionRelated;
        Message message;
        CommandHandler handler;

        /// <summary>
        /// Metodo SetUp de los tests
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            sessionRelated = SessionRelated.Instance;
            handler = new CommandHandler(null);
        }

        /// <summary>
        /// Test para probar los comandos que tiene un admin
        /// </summary>
        [Test]
        public void TestAdminCommandHandler()
        {
            user = new UserInfo("Admin", 1);
            user.Permissions = UserInfo.AdminPermissions;
            sessionRelated.DiccAdminInfo.Add(user, new AdminInfo());
            sessionRelated.AddNewUser(user);

            message = new Message(user.Id, "/comandos");
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
            user = new UserInfo("UserCompany", 2);
            user.Permissions = UserInfo.UserCompanyPermissions;
            sessionRelated.DiccUserCompanyInfo.Add(user, new UserCompanyInfo(company));
            sessionRelated.AddNewUser(user);

            message = new Message(user.Id, "/comandos");
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
            user = new UserInfo("Entrepreneur", 3);
            sessionRelated.DiccEntrepreneurInfo.Add(user, new EntrepreneurInfo("heading", location));
            sessionRelated.AddNewUser(user);

            message = new Message(user.Id, "/comandos");
            string response;

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Estos son todos los comandos: \n/comandos\n/registro\n/hola\nexit\n/busqueda\n/reporte\n/contacto\n/infoemprendedor\n"));
        }
    }
}