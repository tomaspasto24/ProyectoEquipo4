using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    /// <summary>
    /// Clase TokenHandlerTest la cual se encarga de testear las funcionalidades de la clase tokenHandler, se declaran las variables globales que se van a utilizar.
    /// </summary>
    public class TextNullHandlerTest
    {
        IRole role;
        UserInfo user1;
        Message testMessage;

        [Test]
        public void TextNullHandlerTest1()
        {
            role = new RoleDefault();
            user1 = new UserInfo("name1", 5433261, role);
            testMessage = new Message(5433261, "CompanyName");

            // Assert.False(result);
        }
    }
}