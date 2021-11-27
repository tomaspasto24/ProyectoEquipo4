using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    public class CancelHandlerTest
    {
        IRole role;
        UserInfo user1;
        Message testMessage;
        String response;
        IHandler result;


        [Test]
        public void CancelHandlerMessageTest()
        {
            GeoLocation PruebaLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            role = new RoleEntrepreneur("", PruebaLocation);
            user1 = new UserInfo("name1", 5433261, role);
            SessionRelated.Instance.AddNewUser(user1); ;
            CancelHandler cancelHandler = new CancelHandler(null);
            testMessage = new Message(5433261, "/cancelar");

            result = cancelHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Operación cancelada."));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void CancelHandlerWrongTextTest()
        {
            GeoLocation PruebaLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            role = new RoleEntrepreneur("", PruebaLocation);
            user1 = new UserInfo("name1", 5433261, role);
            SessionRelated.Instance.AddNewUser(user1); ;
            CancelHandler cancelHandler = new CancelHandler(null);
            testMessage = new Message(5433261, "/WrongText");

            result = cancelHandler.Handle(testMessage, out response);
            Assert.That(result, Is.Null);
            Assert.That(response, Is.EqualTo(String.Empty)); //verifico que la respuesta sea un string vacio
        }
    }
}