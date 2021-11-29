using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    /// <summary>
    /// Clase TokenHandlerTest la cual se encarga de testear las funcionalidades de la clase tokenHandler, se declaran las variables globales que se van a utilizar.
    /// </summary>
    public class TokenHandlerTest
    {
        UserInfo user1;
        Message testMessage;
        TokenHandler tkhandler;
        String response;
        IHandler result;

        /// <summary>
        /// Se inicializan las variables que se van a usar en los test. 
        /// </summary>
        [SetUp]
        public void Setup()
        {
            user1 = new UserInfo("name1", 5433261);
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            SessionRelated.Instance.AddNewUser(user1);
            testMessage = new Message(5433261, "CompanyName");
            user1.HandlerState = Bot.State.ConfirmingCompanyName;
            tkhandler = new TokenHandler(null);
        }
        /// <summary>
        /// Test que se encarga de verificar que el handler retorne "false" en caso de intentar generar el token y no tener el permiso "GenerateToken".
        /// </summary>
        [Test]
        public void TokenHandleNoPermissionTokenTest()
        {
            user1 = new UserInfo("name1", 5433261);
            user1.Permissions = UserInfo.DefaultPermissions;
            Boolean result = user1.HasPermission(Permission.GenerateToken);
            Assert.False(result);
        }
        /// <summary>
        /// Test que se encarga de verificar que el handler devuelva True en caso de que se intente generar el token y se tenga el permiso necesario.
        /// </summary>
        [Test]
        public void TokenHandlePermissionTokenTest()
        {
            user1 = new UserInfo("name1", 5433261);
            user1.Permissions = UserInfo.AdminPermissions;

            Boolean result = user1.HasPermission(Permission.GenerateToken);
            Assert.True(result);
        }
        /// <summary>
        /// Test que se encarga de verificar que el handler responda correctamente y devuelva true en cas de enviar el comando "/crearinvitacion"
        /// </summary>
        [Test]
        public void TokenHandleInvitationTest()
        {
            testMessage = new Message(5433261, "/crearinvitacion");
            user1.HandlerState = Bot.State.Start;
            TokenHandler tkhandler = new TokenHandler(null);
            String response;
            result = tkhandler.Handle(testMessage, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Procedamos a crear la empresa para el token. \nDinos el nombre del empresa.\nEnvia \"/cancelar\" para cancelar la operación"));
        }
        /// <summary>
        /// Se testea que se le pase una compañia ya registrada y debe devolver el mensaje correspondiente.
        /// </summary>
        [Test]
        public void TokenHandleNoCompanyTest()
        {
            GeoLocation companyLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            Company company = new Company("Las Acacias", "carpinteria", companyLocation, "094654315");

            testMessage = new Message(5433261, "Las Acacias");
            String response;
            SessionRelated.Instance.DiccUserTokens.Add("5433261", company);
            result = tkhandler.Handle(testMessage, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo($"Esa empresa ya está registrada y su token es: \n{SessionRelated.Instance.GetTokenByCompany(company)}\nEnvia \"/cancelar\" para cancelar la operación"));
        }
        /// <summary>
        /// Se testea que asigne el nombre de la compañia que se le pasa.
        /// </summary>
        [Test]
        public void TokenHandleCompanyTest()
        {
            String response;
            testMessage = new Message(5433261, "CompanyName");
            user1.HandlerState = Bot.State.ConfirmingCompanyName;
            result = tkhandler.Handle(testMessage, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Does.Contain("Genial, tenemos el nombre de la empresa. \nAhora dinos el rubro.\nEnvia \"/cancelar\" para cancelar la operación"));
        }
        /// <summary>
        /// Se testea que se asigne correctamente el rubro de la empresa.
        /// </summary>
        [Test]
        public void TokenHandleCompanyHeadingTest()
        {
            result = tkhandler.Handle(testMessage, out response);

            testMessage = new Message(5433261, "Company heading");
            user1.HandlerState = Bot.State.ConfirmingCompanyHeader;
            result = tkhandler.Handle(testMessage, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Does.Contain("Genial, tenemos el rubro de la empresa. \nAhora dinos la ciudad donde se ubica la empresa.\nEnvia \"/cancelar\" para cancelar la operación"));
        }
        /// <summary>
        /// Se testea que se le asigne correctamente la ciudad donde está ubicada la empresa.
        /// </summary>
        [Test]
        public void TokenHandleCompanyCityTest()
        {
            result = tkhandler.Handle(testMessage, out response);

            testMessage = new Message(5433261, "Company City");
            user1.HandlerState = Bot.State.ConfirmingCompanyCity;
            result = tkhandler.Handle(testMessage, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Does.Contain("Genial, tenemos la ciudad de la empresa. \nAhora dinos la direccion de la empresa.\nEnvia \"/cancelar\" para cancelar la operación"));
        }
        /// <summary>
        /// Se testea que se le asigne correctamente la direccion de la empresa.
        /// </summary>
        [Test]
        public void TokenHandleCompanyAddressTest()
        {

            result = tkhandler.Handle(testMessage, out response);

            testMessage = new Message(5433261, "Company address");
            user1.HandlerState = Bot.State.ConfirmingCompanyAddress;
            result = tkhandler.Handle(testMessage, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Does.Contain("Genial, tenemos la direccion de la empresa. \nAhora dinos el contacto de la empresa (e-mail o telefono).\nEnvia \"/cancelar\" para cancelar la operación"));
        }
        [Test]
        /// <summary>
        /// 
        /// </summary>
        public void TokenHandleNullCompanyAddressTest()
        {

            result = tkhandler.Handle(testMessage, out response);

            testMessage = new Message(5433261, null); //nulo se chequea en el primer handler
            user1.HandlerState = Bot.State.ConfirmingCompanyAddress;
            result = tkhandler.Handle(testMessage, out response);

            Assert.That(result, Is.Null);
            //Assert.That(response, Does.Contain("falta adress de la empresa"));
        }
        [Test]
        public void TokenHandleEmptyCompanyAddressTest()
        {

            result = tkhandler.Handle(testMessage, out response);

            testMessage = new Message(5433261, "");  //string vacio
            user1.HandlerState = Bot.State.ConfirmingCompanyAddress;
            result = tkhandler.Handle(testMessage, out response);

            Assert.That(result, Is.Null);
            //Assert.That(response, Does.Contain("falta adress de la empresa"));
        }
        /// <summary>
        /// Se testea que se le asigne el contacto de la empresa y para llegar a este punto debe pasar por las condiciones anteriores.
        /// </summary>
        [Test]
        public void TokenHandleCompanyContactTest()
        {

            testMessage = new Message(5433261, "CompanyName");
            user1.HandlerState = Bot.State.ConfirmingCompanyName;
            result = tkhandler.Handle(testMessage, out response);

            testMessage = new Message(5433261, "Company heading");
            user1.HandlerState = Bot.State.ConfirmingCompanyHeader;
            result = tkhandler.Handle(testMessage, out response);

            testMessage = new Message(5433261, "Montevideo");
            user1.HandlerState = Bot.State.ConfirmingCompanyCity;
            result = tkhandler.Handle(testMessage, out response);

            testMessage = new Message(5433261, "Av. 8 de Octubre 2738");
            user1.HandlerState = Bot.State.ConfirmingCompanyAddress;
            result = tkhandler.Handle(testMessage, out response);

            testMessage = new Message(5433261, "Company contact");
            user1.HandlerState = Bot.State.ConfirmingCompanyContact;
            result = tkhandler.Handle(testMessage, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Does.Contain("Genial, tenemos el contacto de la empresa. \nEmpresa creada con exito. El token para esta empresa es"));
        }
    }
}