using System;
using Bot;
using NUnit.Framework;

namespace BotTests
{
    /// <summary>
    /// Clase TokenHandlerTest la cual se encarga de testear las funcionalidades de la clase tokenHandler, se declaran las variables globales que se van a utilizar.
    /// </summary>
    public class TokenHandlerTest
    {
        private UserInfo user1;
        private Message testMessage;
        private TokenHandler tkhandler;
        private String response;
        private IHandler result;

        /// <summary>
        /// Se inicializan las variables que se van a usar en los test. 
        /// </summary>
        [SetUp]
        public void Setup()
        {
            SessionRelated.Instance = null;
            this.user1 = new UserInfo("name1", 5433261);
            this.user1.Permissions = UserInfo.AdminPermissions;
            SessionRelated.Instance.AddNewUser(this.user1);
            this.tkhandler = new TokenHandler(null);
        }

        /// <summary>
        /// Test que se encarga de verificar que el handler retorne "false" en caso de intentar generar el token y no tener el permiso "GenerateToken".
        /// </summary>
        [Test]
        public void TokenHandleNoPermissionTokenTest()
        {
            this.user1 = new("name1", 5433261);
            this.user1.Permissions = UserInfo.DefaultPermissions;
            Boolean result = this.user1.HasPermission(Permission.GenerateToken);
            Assert.False(result);
        }
        /// <summary>
        /// Test que se encarga de verificar que el handler devuelva True en caso de que se intente generar el token y se tenga el permiso necesario.
        /// </summary>
        [Test]
        public void TokenHandlePermissionTokenTest()
        {
            this.user1 = new UserInfo("name1", 5433261);
            this.user1.Permissions = UserInfo.AdminPermissions;
            Boolean result = this.user1.HasPermission(Permission.GenerateToken);
            Assert.True(result);
        }

        /// <summary>
        /// Test que se encarga de verificar que el handler responda correctamente y devuelva true en cas de enviar el comando "/crearinvitacion"
        /// </summary>
        [Test]
        public void TokenHandleInvitationTest()
        {
            this.testMessage = new Message(5433261, "/crearinvitacion");
            this.user1.HandlerState = Bot.State.Start;
            TokenHandler tkhandler = new TokenHandler(null);
            this.result = this.tkhandler.Handle(this.testMessage, out this.response);

            Assert.That(this.result, Is.Not.Null);
            Assert.That(this.response, Is.EqualTo("Procedamos a crear la empresa para el token. \nDinos el nombre del empresa.\nEnvia \"/cancelar\" para cancelar la operación"));
        }

        /// <summary>
        /// Se testea que se le pase una compañia ya registrada y debe devolver el mensaje correspondiente.
        /// </summary>
        [Test]
        public void TokenHandleNoCompanyTest()
        {
            GeoLocation companyLocation = new("Camino Maldonado 2415", "Montevideo");
            Company company = new("Las Acacias", "carpinteria", companyLocation, "094654315");
            SessionRelated.Instance.DiccUserTokens.Add("5433261", company);

            this.user1.HandlerState = Bot.State.ConfirmingCompanyName;
            this.testMessage = new Message(5433261, "Las Acacias");
            this.result = this.tkhandler.Handle(this.testMessage, out this.response);

            Assert.That(this.result, Is.Not.Null);
            Assert.That(this.response, Is.EqualTo($"Esa empresa ya está registrada y su token es: \n{SessionRelated.Instance.GetTokenByCompany(company)}\nEnvia \"/cancelar\" para cancelar la operación"));
        }

        /// <summary>
        /// Se testea que asigne el nombre de la compañia que se le pasa.
        /// </summary>
        [Test]
        public void TokenHandleCompanyTest()
        {
            this.testMessage = new Message(5433261, "Company Name");
            this.user1.HandlerState = Bot.State.ConfirmingCompanyName;
            this.result = this.tkhandler.Handle(this.testMessage, out this.response);

            this.testMessage = new Message(5433261, "CompanyName");
            this.user1.HandlerState = Bot.State.ConfirmingCompanyName;
            this.result = this.tkhandler.Handle(this.testMessage, out this.response);

            Assert.That(this.result, Is.Not.Null);
            Assert.That(this.response, Does.Contain("Genial, tenemos el nombre de la empresa. \nAhora dinos el rubro.\nEnvia \"/cancelar\" para cancelar la operación"));
        }

        /// <summary>
        /// Se testea que se asigne correctamente el rubro de la empresa.
        /// </summary>
        [Test]
        public void TokenHandleCompanyHeadingTest()
        {
            this.testMessage = new Message(5433261, "Company Name");
            this.user1.HandlerState = Bot.State.ConfirmingCompanyName;
            this.result = this.tkhandler.Handle(this.testMessage, out this.response);

            this.testMessage = new Message(5433261, "Company heading");
            this.user1.HandlerState = Bot.State.ConfirmingCompanyHeader;
            this.result = this.result.Handle(this.testMessage, out this.response);

            Assert.That(this.result, Is.Not.Null);
            Assert.That(this.response, Does.Contain("Genial, tenemos el rubro de la empresa. \nAhora dinos la ciudad donde se ubica la empresa.\nEnvia \"/cancelar\" para cancelar la operación"));
        }

        /// <summary>
        /// Se testea que se le asigne correctamente la ciudad donde está ubicada la empresa.
        /// </summary>
        [Test]
        public void TokenHandleCompanyCityTest()
        {
            this.testMessage = new Message(5433261, "Company Name");
            this.user1.HandlerState = Bot.State.ConfirmingCompanyName;
            this.result = this.tkhandler.Handle(this.testMessage, out this.response);

            this.testMessage = new Message(5433261, "Company City");
            this.user1.HandlerState = Bot.State.ConfirmingCompanyCity;
            this.result = this.result.Handle(this.testMessage, out this.response);

            Assert.That(this.result, Is.Not.Null);
            Assert.That(this.response, Does.Contain("Genial, tenemos la ciudad de la empresa. \nAhora dinos la direccion de la empresa.\nEnvia \"/cancelar\" para cancelar la operación"));
        }

        /// <summary>
        /// Se testea que se le asigne correctamente la direccion de la empresa.
        /// </summary>
        [Test]
        public void TokenHandleCompanyAddressTest()
        {
            this.testMessage = new Message(5433261, "Company Name");
            this.user1.HandlerState = Bot.State.ConfirmingCompanyName;
            this.result = this.tkhandler.Handle(this.testMessage, out this.response);

            this.testMessage = new Message(5433261, "Company address");
            this.user1.HandlerState = Bot.State.ConfirmingCompanyAddress;
            this.result = this.result.Handle(this.testMessage, out this.response);

            Assert.That(this.result, Is.Not.Null);
            Assert.That(this.response, Does.Contain("Genial, tenemos la direccion de la empresa. \nAhora dinos el contacto de la empresa (e-mail o telefono).\nEnvia \"/cancelar\" para cancelar la operación"));
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del handler al recibir un string vacío.
        /// </summary>
        [Test]
        public void TokenHandleEmptyCompanyAddressTest()
        {
            this.testMessage = new Message(5433261, "Company Name");
            this.user1.HandlerState = Bot.State.ConfirmingCompanyName;
            this.result = this.tkhandler.Handle(this.testMessage, out this.response);

            this.testMessage = new Message(5433261, "");  //string vacio
            this.user1.HandlerState = Bot.State.ConfirmingCompanyAddress;
            this.result = this.result.Handle(this.testMessage, out this.response);
            Assert.That(this.result, Is.Not.Null);
            Assert.That(this.response, Does.Contain("Genial, tenemos la direccion de la empresa. \nAhora dinos el contacto de la empresa (e-mail o telefono).\nEnvia \"/cancelar\" para cancelar la operación"));
        }

        /// <summary>
        /// Se testea que se le asigne el contacto de la empresa y para llegar a este punto debe pasar por las condiciones anteriores.
        /// </summary>
        [Test]
        public void TokenHandleCompanyContactTest()
        {
            this.testMessage = new Message(5433261, "CompanyName");
            this.user1.HandlerState = Bot.State.ConfirmingCompanyName;
            this.result = this.tkhandler.Handle(this.testMessage, out this.response);

            this.testMessage = new Message(5433261, "Company heading");
            this.user1.HandlerState = Bot.State.ConfirmingCompanyHeader;
            this.result = this.tkhandler.Handle(this.testMessage, out this.response);

            this.testMessage = new Message(5433261, "Montevideo");
            this.user1.HandlerState = Bot.State.ConfirmingCompanyCity;
            this.result = this.tkhandler.Handle(this.testMessage, out this.response);

            this.testMessage = new Message(5433261, "Av. 8 de Octubre 2738");
            this.user1.HandlerState = Bot.State.ConfirmingCompanyAddress;
            this.result = this.tkhandler.Handle(this.testMessage, out this.response);

            this.testMessage = new Message(5433261, "Company contact");
            this.user1.HandlerState = Bot.State.ConfirmingCompanyContact;
            this.result = this.tkhandler.Handle(this.testMessage, out this.response);

            Assert.That(this.result, Is.Not.Null);
            Assert.That(this.response, Does.Contain("Genial, tenemos el contacto de la empresa. \nEmpresa creada con exito. El token para esta empresa es"));
        }
    }
}