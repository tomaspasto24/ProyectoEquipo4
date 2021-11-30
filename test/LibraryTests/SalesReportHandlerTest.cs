using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    public class SalesReportHandlerTest
    {
        UserInfo user1;
        Message testMessage;
        String response;
        IHandler result;
        Company company;
        GeoLocation location;
        Material material;
        Publication publication;


        [Test]
        public void SalesReportHandlerNoHasPermissionTest()
        {
            SessionRelated.Instance = null;
            user1 = new UserInfo("name1", 5433261);
            user1.Permissions = UserInfo.DefaultPermissions;
            SessionRelated.Instance.AddNewUser(user1);
            SalesReportHandler salesreportHandler = new SalesReportHandler(null);
            testMessage = new Message(5433261, "");

            result = salesreportHandler.Handle(testMessage, out response);
            Assert.That(result, Is.Null);
        }
        [Test]
        public void SalesReportHandlerHasPermissionTest()
        {
            SessionRelated.Instance = null;
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            Company company = new Company("Ferretería Mdeo", "herramientas", location, "091234567");

            user1 = new UserInfo("name1", 5433261);
            user1.Permissions = UserInfo.UserCompanyPermissions;
            SessionRelated.Instance.AddNewUser(user1);
            SalesReportHandler salesReportHandler = new SalesReportHandler(null);

            SessionRelated.Instance.DiccUserTokens.Add("IHaveAToken", company);
            SessionRelated.Instance.DiccUserCompanyInfo.Add(user1, new UserCompanyInfo(SessionRelated.Instance.GetCompanyByToken("IHaveAToken")));

            location = new GeoLocation("Av. Italia", "Montevideo");
            material = new Material("Alambre", 800, 200);
            publication = new Publication("Publicacion especial", company, location, material);
            publication.ClosePublication();

            testMessage = new Message(5433261, "/reporte");
            user1.HandlerState = Bot.State.Start;
            result = salesReportHandler.Handle(testMessage, out response);

            Assert.That(response.Contains("Publicaciones cerradas de los ultimos 30 dias de la empresa: Ferretería Mdeo"));
            Assert.That(result, Is.Not.Null);
        }
    }
}
