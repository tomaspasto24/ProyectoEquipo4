/*
using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    /// <summary>
    /// Clase TextNullHandlerTest la cual se encarga de testear las funcionalidades de la clase TextNullHandler, se declaran las variables globales que se van a utilizar.
    /// </summary>
    public class ContactHandlerTest
    {
        UserInfo user1;
        Message testMessage;
        String response;
        IHandler result;
        Company company;


        [Test]
        public void ContactHandlerNoHasPermissionTest()
        {
            user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(user1);
            ContactHandler contactHandler = new ContactHandler(null);
            testMessage = new Message(5433261, "");

            result = contactHandler.Handle(testMessage, out response);
            //Assert.That(response, Is.EqualTo("Operación cancelada."));
            Assert.That(result, Is.Null);
        }
        [Test]
        public void ContactHandlerHasPermissionTest()
        {
            GeoLocation PruebaLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, new EntrepreneurInfo("", PruebaLocation));
            SessionRelated.Instance.AddNewUser(user1);
            ContactHandler contactHandler = new ContactHandler(null);
            testMessage = new Message(5433261, "/contacto");

            result = contactHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Por favor dinos con que empresa te quieres contactar. \nEnvia \"/cancelar\" para cancelar la operación."));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void ContactHandlerContactCompanyTest()
        {
            GeoLocation companyLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            company = new Company("Las Acacias", "carpinteria", companyLocation, "094654315");
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, new EntrepreneurInfo("", entrepreneurLocation));
            SessionRelated.Instance.AddNewUser(user1);
            SessionRelated.Instance.DiccUserTokens.Add("5433261", company);
            user1.HandlerState = Bot.State.AskingCompanyName;
            ContactHandler contactHandler = new ContactHandler(null);
            testMessage = new Message(5433261, "Las Acacias");

            result = contactHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo(company.ReturnContact()));
        }
        [Test]
        public void ContactHandlerCompanyNoFoundTest()
        {
            GeoLocation PruebaLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, new EntrepreneurInfo("", PruebaLocation));
            SessionRelated.Instance.AddNewUser(user1);
            user1.HandlerState = Bot.State.AskingCompanyName;
            ContactHandler contactHandler = new ContactHandler(null);
            testMessage = new Message(5433261, "NoCompany");

            result = contactHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Disculpa, no encontramos esa Empresa"));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void ContactHandlerCompanyWrongHandlerState()
        {
            GeoLocation PruebaLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(user1, new EntrepreneurInfo("", PruebaLocation));
            SessionRelated.Instance.AddNewUser(user1);
            //user1.HandlerState = Bot.State.AskingCompanyName;
            user1.HandlerState = Bot.State.AskingDataNumber;
            ContactHandler contactHandler = new ContactHandler(null);
            testMessage = new Message(5433261, "NoCompany");

            result = contactHandler.Handle(testMessage, out response);
            Assert.That(result, Is.Null);
            Assert.That(response, Is.Empty);
        }
    }
}
*/