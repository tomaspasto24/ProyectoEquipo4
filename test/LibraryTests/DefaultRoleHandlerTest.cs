using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    public class DefaultRoleHandlerTest
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
        public void DefaultRoleHandlerDefaultTest()
        {
            user1.Permissions = UserInfo.UserCompanyPermissions;
            DefaultRoleHandler defaultRoleHandler = new DefaultRoleHandler(null);
            testMessage = new Message(5433261, "/default");

            result = defaultRoleHandler.Handle(testMessage, out response);
            Assert.That(result, Is.Not.Null);
            Assert.That(response, Is.EqualTo("ahora eres default"));
        }
        [Test]
        public void DefaultRoleHandlerWrongCommandTest()
        {
            user1.Permissions = UserInfo.UserCompanyPermissions;
            DefaultRoleHandler defaultRoleHandler = new DefaultRoleHandler(null);
            testMessage = new Message(5433261, "/WrongCommand");

            result = defaultRoleHandler.Handle(testMessage, out response);
            Assert.That(result, Is.Null);
            Assert.That(response, Is.Empty);
        }
    }
}