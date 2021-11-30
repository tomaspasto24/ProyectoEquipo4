using Bot;
using NUnit.Framework;

namespace BotTests
{
    /// <summary>
    /// Clase para testear el CommandHandler.
    /// </summary>
    public class CommandHandlerTest
    {
        private UserInfo User;
        private SessionRelated sessionRelated;
        private Message message;
        private CommandHandler handler;

        /// <summary>
        /// Metodo SetUp de los tests en el cual inicializo las variables que voy a utilizar.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.sessionRelated = SessionRelated.Instance;
            this.handler = new CommandHandler(null);
        }

        /// <summary>
        /// Test para probar los comandos que tiene un admin.
        /// </summary>
        [Test]
        public void TestAdminCommandHandler()
        {
            this.User = new("Admin", 1);
            this.User.Permissions = UserInfo.AdminPermissions;
            this.sessionRelated.DiccAdminInfo.Add(this.User, new AdminInfo());
            this.sessionRelated.AddNewUser(this.User);

            this.message = new(this.User.Id, "/comandos");
            string response;

            IHandler result = this.handler.Handle(this.message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Estos son todos los comandos: \n/comandos - Muestra la lista de comandos.\n/hola - Saluda al bot.\n/crearinvitacion - Genera una nueva invitación para una Empresa.\n"));
        }

        /// <summary>
        /// Test para probar los comandos que tiene un this.UserCompany.
        /// </summary>
        [Test]
        public void TestUserCompanyCommandHandler()
        {
            GeoLocation location = new("adress", "city");
            Company company = new("nombre", "rubro", location, "contacto");
            this.User = new("this.UserCompany", 2);
            this.User.Permissions = UserInfo.UserCompanyPermissions;
            this.sessionRelated.DiccUserCompanyInfo.Add(this.User, new UserCompanyInfo(company));
            this.sessionRelated.AddNewUser(this.User);

            this.message = new(this.User.Id, "/comandos");
            string response;

            IHandler result = this.handler.Handle(this.message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Estos son todos los comandos: \n/comandos - Muestra la lista de comandos.\n/hola - Saluda al bot.\n/reporte - Obtener un reporte de las entregas realizadas en los últimos 30 días.\n/publicar - Crear una nueva publicación con un material o varios.\n/agregarmaterial - Agregar un material a una publicación existente.\n"));
        }

        /// <summary>
        /// Test para probar los comandos que tiene un Emprendedor.
        /// </summary>
        [Test]
        public void TestEntrepreneurCommandHandler()
        {
            GeoLocation location = new("adress", "city");
            this.User = new("Entrepreneur", 3);
            this.User.Permissions = UserInfo.EntrepreneurPermissions;
            this.sessionRelated.DiccEntrepreneurInfo.Add(this.User, new EntrepreneurInfo("heading", location));
            this.sessionRelated.AddNewUser(this.User);

            this.message = new(this.User.Id, "/comandos");
            string response;

            IHandler result = this.handler.Handle(this.message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Estos son todos los comandos: \n/comandos - Muestra la lista de comandos.\n/hola - Saluda al bot.\n/busqueda - Buscar.\n/reporte - Obtener un reporte de las compras realizadas en los últimos 30 días.\n/contacto - Obtener el contacto de una Empresa.\n/datos - Gestionar los datos del Usuario.\n/modificardatos - Modifica los datos del Usuario.\n"));
        }

        /// <summary>
        /// Test para probar los comandos que tiene un usuario Default.
        /// </summary>
        [Test]
        public void TestDefaultCommandHandler()
        {
            this.User = new UserInfo("Default", 4);
            this.sessionRelated.AddNewUser(this.User);

            this.message = new(this.User.Id, "/comandos");
            string response;

            IHandler result = this.handler.Handle(this.message, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Estos son todos los comandos: \n/comandos - Muestra la lista de comandos.\n/hola - Saluda al bot.\n/registro - Registrate como usuario de una empresa.\n/emprender - Registrate como un Emprendedor.\n"));
        }
    }
}