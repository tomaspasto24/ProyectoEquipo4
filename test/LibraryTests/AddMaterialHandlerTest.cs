using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    public class AddMaterialHandlerTest
    {
        UserInfo user1;
        Message testMessage;
        String response;
        IHandler result;


        [Test]
        public void AddMaterialHandlerStartTest()
        {
            SessionRelated.Instance = null;
            user1 = new UserInfo("name1", 5433261);
            user1.Permissions = UserInfo.UserCompanyPermissions;
            SessionRelated.Instance.AddNewUser(user1);
            AddMaterialHandler addMaterialHandler = new AddMaterialHandler(null);

            user1.HandlerState = Bot.State.Start;
            testMessage = new Message(5433261, "/agregarmaterial");
            result = addMaterialHandler.Handle(testMessage, out response);

            Assert.That(response, Is.EqualTo("Envía el título de la publicación en la que quieres agregar el material"));
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void CancelHandlerMessageTest()
        {
            SessionRelated.Instance = null;
            user1 = new UserInfo("name1", 5433261);
            user1.Permissions = UserInfo.UserCompanyPermissions;
            SessionRelated.Instance.AddNewUser(user1);

            GeoLocation companyLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            Company company = new Company("Las Acacias", "carpinteria", companyLocation, "094654315");
            Publication publication = new Publication("Madera de pino", company, companyLocation, new Material("Madera", 500, 280));
            PublicationSet.Instance.AddElement(publication);
            SessionRelated.Instance.DiccUserTokens.Add("IHaveAToken", company);
            SessionRelated.Instance.DiccUserCompanyInfo.Add(user1, new UserCompanyInfo(SessionRelated.Instance.GetCompanyByToken("IHaveAToken")));

            AddMaterialHandler addMaterialHandler = new AddMaterialHandler(null);
            SessionRelated.Instance.DiccUserTokens.Add("5433261", company);

            user1.HandlerState = Bot.State.Start;
            testMessage = new Message(5433261, "/agregarmaterial");
            result = addMaterialHandler.Handle(testMessage, out response);

            testMessage = new Message(5433261, "NotValidMaterial");

            ArgumentException ex = Assert.Throws<ArgumentException>(() => result.Handle(testMessage, out response));
            Assert.That(ex.Message, Is.EqualTo("No existe una publicación con ese nombre"));
            Assert.That(result, Is.Not.Null);
        }
        /*
        [Test]
        public void AddMaterialHandlerAddQuantityTest()
        {
            user1 = new UserInfo("name1", 5433261);
            user1.Permissions = UserInfo.UserCompanyPermissions;
            SessionRelated.Instance.AddNewUser(user1);
            AddMaterialHandler addMaterialHandler = new AddMaterialHandler(null);

            GeoLocation companyLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            Company company = new Company("Las Acacias", "carpinteria", companyLocation, "094654315");
            Publication publication = new Publication("Madera de pino", company, companyLocation, new Material("Madera", 500, 280));
            PublicationSet.Instance.AddElement(publication);
            SessionRelated.Instance.DiccUserTokens.Add("IHaveAToken", company);
            SessionRelated.Instance.DiccUserCompanyInfo.Add(user1, new UserCompanyInfo(SessionRelated.Instance.GetCompanyByToken("IHaveAToken")));

            PublicationSet.Instance.AddElement(publication);
            company.AddOwnPublication(publication);
            user1.HandlerState = Bot.State.AskingMaterialNameToAdd;
            testMessage = new Message(5433261, "MaterialName");
            result = addMaterialHandler.Handle(testMessage, out response);


            Assert.That(response, Is.EqualTo("Envía la cantidad del material"));
            Assert.That(result, Is.Not.Null);
        }
        */
    }
}
