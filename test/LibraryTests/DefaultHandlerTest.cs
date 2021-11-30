using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    public class DefaultHandlerTest
    {
        UserInfo user1;
        Message testMessage;
        String response;
        IHandler result;


        [SetUp]
        public void Setup()
        {
            SessionRelated.Instance = null;
            user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(user1);
        }

        [Test]
        public void DefaultHandlerSlashTest()
        {
            user1.Permissions = UserInfo.UserCompanyPermissions;
            DefaultHandler defaultHandler = new DefaultHandler(null);
            testMessage = new Message(5433261, "/Command");

            result = defaultHandler.Handle(testMessage, out response);
            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Tu comando no fue encontrado o no tienes el rango necesario para utilizarlo."));
        }
        [Test]
        public void DefaultHandlerWithOutSlashTest()
        {
            user1.Permissions = UserInfo.UserCompanyPermissions;
            DefaultHandler defaultHandler = new DefaultHandler(null);
            testMessage = new Message(5433261, "Command");

            result = defaultHandler.Handle(testMessage, out response);
            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("Disculpa, no te entiendo"));
        }
    }
}