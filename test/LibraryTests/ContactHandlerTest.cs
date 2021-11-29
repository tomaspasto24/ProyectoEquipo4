/*
using System;
using Bot;
using NUnit.Framework;

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

        /// <summary>
        /// Se prueba el mensaje y que se cancele la operación cuando no se tiene un permiso.
        /// </summary>
        [Test]
        public void ContactHandlerNoHasPermissionTest()
        {
            this.user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.AddNewUser(this.user1);
            ContactHandler contactHandler = new ContactHandler(null);
            this.testMessage = new Message(5433261, "");

            this.result = contactHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Operación cancelada."));
            Assert.That(this.result, Is.Null);
        }

        /// <summary>
        /// Se prueba el mensaje y que se cancele la operación cuando se tiene un permiso.
        /// </summary>
        [Test]
        public void ContactHandlerHasPermissionTest()
        {
            GeoLocation pruebaLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            this.user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(this.user1, new EntrepreneurInfo("", pruebaLocation));
            SessionRelated.Instance.AddNewUser(this.user1);
            ContactHandler contactHandler = new ContactHandler(null);
            this.testMessage = new Message(5433261, "/contacto");

            this.result = contactHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Por favor dinos con que empresa te quieres contactar. \nEnvia \"/cancelar\" para cancelar la operación."));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Se prueba que se devuelva al contacto de la empresa.
        /// </summary>
        [Test]
        public void ContactHandlerContactCompanyTest()
        {
            GeoLocation companyLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            this.company = new Company("Las Acacias", "carpinteria", companyLocation, "094654315");
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            this.user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(this.user1, new EntrepreneurInfo("", entrepreneurLocation));
            SessionRelated.Instance.AddNewUser(this.user1);
            SessionRelated.Instance.DiccUserTokens.Add("5433261", this.company);
            this.user1.HandlerState = Bot.State.AskingCompanyName;
            ContactHandler contactHandler = new ContactHandler(null);
            this.testMessage = new Message(5433261, "Las Acacias");

            this.result = contactHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo(this.company.ReturnContact()));
        }

        /// <summary>
        /// Se prueba cuando no se encuentra el contacto.
        /// </summary>
        [Test]
        public void ContactHandlerCompanyNoFoundTest()
        {
            GeoLocation pruebaLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            this.user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(this.user1, new EntrepreneurInfo("", pruebaLocation));
            SessionRelated.Instance.AddNewUser(this.user1);
            this.user1.HandlerState = Bot.State.AskingCompanyName;
            ContactHandler contactHandler = new ContactHandler(null);
            this.testMessage = new Message(5433261, "NoCompany");

            this.result = contactHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.response, Is.EqualTo("Disculpa, no encontramos esa Empresa"));
            Assert.That(this.result, Is.Not.Null);
        }

        /// <summary>
        /// Se prueba si se tiene un estado equivocado.
        /// </summary>
        [Test]
        public void ContactHandlerCompanyWrongHandlerState()
        {
            GeoLocation pruebaLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            this.user1 = new UserInfo("name1", 5433261);
            SessionRelated.Instance.DiccEntrepreneurInfo.Add(this.user1, new EntrepreneurInfo("", pruebaLocation));
            SessionRelated.Instance.AddNewUser(this.user1);
            //user1.HandlerState = Bot.State.AskingCompanyName;
            this.user1.HandlerState = Bot.State.AskingDataNumber;
            ContactHandler contactHandler = new ContactHandler(null);
            this.testMessage = new Message(5433261, "NoCompany");

            this.result = contactHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.result, Is.Null);
            Assert.That(this.response, Is.Empty);
        }
    }
}
*/