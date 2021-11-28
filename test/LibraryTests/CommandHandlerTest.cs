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
            Assert.That(response, Is.EqualTo("Estos son todos los comandos: \n/comandos - Muestra la lista de comandos.\n/hola - Saluda al bot.\n/crearinvitacion - Genera una nueva invitación para una Empresa.\n"));
        }

        /// <summary>
        /// Test para probar los comandos que tiene un UserCompany
        /// </summary>
        [Test]
        public void TestUserCompanyCommandHandler()
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
            Assert.That(response, Is.EqualTo("Estos son todos los comandos: \n/comandos - Muestra la lista de comandos.\n/hola - Saluda al bot.\n/reporte - Obtener un reporte de las entregas realizadas en los últimos 30 días.\n/publicar - Crear una nueva publicación con un material o varios.\n/agregarmaterial - Agregar un material a una publicación existente.\n"));
        }

        /// <summary>
        /// Test para probar los comandos que tiene un Emprendedor
        /// </summary>
        [Test]
        public void TestEntrepreneurCommandHandler()
        {
            GeoLocation location = new GeoLocation("adress", "city");
            user = new UserInfo("Entrepreneur", 3);
            user.Permissions = UserInfo.EntrepreneurPermissions;
            sessionRelated.DiccEntrepreneurInfo.Add(user, new EntrepreneurInfo("heading", location));
            sessionRelated.AddNewUser(user);

            message = new Message(user.Id, "/comandos");
            string response;

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Estos son todos los comandos: \n/comandos - Muestra la lista de comandos.\n/hola - Saluda al bot.\n/busqueda - Buscar.\n/reporte - Obtener un reporte de las compras realizadas en los últimos 30 días.\n/contacto - Obtener el contacto de una Empresa.\n/datos - Gestionar los datos del Usuario.\n/modificardatos - Modifica los datos del Usuario.\n"));
        }
        /// <summary>
        /// Test para probar los comandos que tiene un usuario Default
        /// </summary>
        [Test]
        public void TestDefaultCommandHandler()
        {
            user = new UserInfo("Default", 4);
            sessionRelated.AddNewUser(user);

            message = new Message(user.Id, "/comandos");
            string response;

            IHandler result = handler.Handle(message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Estos son todos los comandos: \n/comandos - Muestra la lista de comandos.\n/hola - Saluda al bot.\n/registro - Registrate como usuario de una empresa.\n/emprender - Registrate como un Emprendedor.\n"));
        }
    }
}