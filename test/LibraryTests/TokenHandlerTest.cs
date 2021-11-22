using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    /// <summary>
    /// Clase TokenHandlerTest la cual se encarga de testear las funcionalidades de la clase tokenHandler.
    /// </summary>
    public class TokenHandlerTest
    {
        IRole role;
        UserInfo user1;
        Message testMessage;
        TokenHandler tkhandler;
        String response;
        IHandler result;

        [SetUp]
        public void Setup()
        {
            role = new RoleAdmin();
            user1 = new UserInfo("name1", 5433261, role);
            SessionRelated.Instance.AddNewUser(user1);
            testMessage = new Message(5433261, "CompanyName");
            user1.HandlerState = Bot.State.ConfirmingCompanyName;
            tkhandler = new TokenHandler(null);
        }

        [Test]
        public void InternalHandleNoPermissionTokenTest()
        {
            role = new RoleDefault();
            user1 = new UserInfo("name1", 5433261, role);
            Boolean result = user1.UserRole.HasPermission(Permission.GenerateToken);
            Assert.False(result);
        }
        [Test]
        public void InternalHandlePermissionTokenTest()
        {
            role = new RoleAdmin();
            user1 = new UserInfo("name1", 5433261, role);

            Boolean result = user1.UserRole.HasPermission(Permission.GenerateToken);
            Assert.True(result);


        }
        [Test]
        public void InternalHandleInvitationTest()
        {
            testMessage = new Message(5433261, "/crearinvitacion");
            user1.HandlerState = Bot.State.Start;
            TokenHandler tkhandler = new TokenHandler(null);
            String response;
            result = tkhandler.Handle(testMessage, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Procedamos a crear la empresa para el token. \nDinos el nombre del empresa.\nEnvia \"/cancelar\" para cancelar la operación"));
        }
        [Test]
        public void InternalHandleNoCompanyTest()
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
        [Test]
        public void InternalHandleCompanyTest()
        {
            String response;
            testMessage = new Message(5433261, "CompanyName");
            user1.HandlerState = Bot.State.ConfirmingCompanyName;
            result = tkhandler.Handle(testMessage, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Does.Contain("Genial, tenemos el nombre de la empresa. \nAhora dinos el rubro.\nEnvia \"/cancelar\" para cancelar la operación"));
        }
        [Test]
        public void InternalHandleCompanyHeadingTest()
        {
            result = tkhandler.Handle(testMessage, out response);

            testMessage = new Message(5433261, "Company heading");
            user1.HandlerState = Bot.State.ConfirmingCompanyHeader;
            result = tkhandler.Handle(testMessage, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Does.Contain("Genial, tenemos el rubro de la empresa. \nAhora dinos la ciudad donde se ubica la empresa.\nEnvia \"/cancelar\" para cancelar la operación"));
        }
        [Test]
        public void InternalHandleCompanyCityTest()
        {
            result = tkhandler.Handle(testMessage, out response);

            testMessage = new Message(5433261, "Company City");
            user1.HandlerState = Bot.State.ConfirmingCompanyCity;
            result = tkhandler.Handle(testMessage, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Does.Contain("Genial, tenemos la ciudad de la empresa. \nAhora dinos la direccion de la empresa.\nEnvia \"/cancelar\" para cancelar la operación"));
        }
        [Test]
        public void InternalHandleCompanyAddressTest()
        {

            result = tkhandler.Handle(testMessage, out response);

            testMessage = new Message(5433261, "Company address");
            user1.HandlerState = Bot.State.ConfirmingCompanyAddress;
            result = tkhandler.Handle(testMessage, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Does.Contain("Genial, tenemos la direccion de la empresa. \nAhora dinos el contacto de la empresa (e-mail o telefono).\nEnvia \"/cancelar\" para cancelar la operación"));
        }
        [Test]
        public void InternalHandleNullCompanyAddressTest()
        {

            result = tkhandler.Handle(testMessage, out response);

            testMessage = new Message(5433261, null);
            user1.HandlerState = Bot.State.ConfirmingCompanyAddress;
            result = tkhandler.Handle(testMessage, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Does.Contain("Genial, tenemos la direccion de la empresa. \nAhora dinos el contacto de la empresa (e-mail o telefono).\nEnvia \"/cancelar\" para cancelar la operación"));
        }

        [Test]
        public void InternalHandleCompanyContactTest()
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