
using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    /// <summary>
    /// Clase TokenHandlerTest la cual se encarga de testear las funcionalidades de la clase tokenHandler, se declaran las variables globales que se van a utilizar.
    /// </summary>
    public class AdminHandlerTest
    {
        UserInfo user1;
        Message testMessage;
        String response;
        IHandler result;

        [SetUp]
        public void Setup()
        {
            user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(user1);
            user1.Permissions = UserInfo.AdminPermissions;
        }

        [Test]
        public void AdminHandlerAdminTest()
        {
            testMessage = new Message(5433261, "/admin");
            AdminHandler adminHandler = new AdminHandler(null);
            result = adminHandler.Handle(testMessage, out response);

            Assert.That(result, Is.Not.Null);
            Assert.That(response, Does.Contain("Ahora eres admin"));
        }
        [Test]
        public void AdminHandlerWrongCommandTest()
        {
            testMessage = new Message(5433261, "/WrongCommand");
            AdminHandler adminHandler = new AdminHandler(null);
            result = adminHandler.Handle(testMessage, out response);

            Assert.That(result, Is.Null);
            Assert.That(response, Is.Empty);
        }
    }
}