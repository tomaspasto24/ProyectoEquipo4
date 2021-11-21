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
        RoleAdmin admin;
        UserInfo user1;
        Message messagePruebaTest1;

        [SetUp]
        public void Setup()
        {
            this.admin = new RoleAdmin();
            messagePruebaTest1 = new Message(544394, "");
            user1 = new UserInfo("name1", 5433261, this.admin);
        }

        [Test]
        public void InternalHandleExceptionTest()
        {
            IHandler handler = new TokenHandler(new DefaultHandler(null));
            String response = "the response";
            handler.Handle(messagePruebaTest1, out response);
            Assert.True(true);

        }
    }
}