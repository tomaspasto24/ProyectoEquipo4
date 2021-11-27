using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{

    public class SalesReportHandlerTest
    {
        IRole role;
        UserInfo user1;
        Message testMessage;
        String response;
        IHandler result;
        Company company;
        GeoLocation location;
        Material material;
        Publication publication;


        [Test]
        public void SearchHandlerNoHasPermissionTest()
        {
            role = new RoleDefault();
            user1 = new UserInfo("name1", 5433261, role);
            SessionRelated.Instance.AddNewUser(user1);
            SalesReportHandler salesreportHandler = new SalesReportHandler(null);
            testMessage = new Message(5433261, "");

            result = salesreportHandler.Handle(testMessage, out response);
            Assert.That(result, Is.Null);
        }
        [Test]
        public void SearchHandlerHasPermissionTest()
        {
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2415", "Montevideo");
            // user1 = new UserInfo("name1", 5433261, role);
            SessionRelated.Instance.AddNewUser(user1);
            SalesReportHandler salesReportHandler = new SalesReportHandler(null);
            user1.HandlerState = Bot.State.Start;
            testMessage = new Message(5433261, "/reporte");

            location = new GeoLocation("Av. Italia", "Montevideo");
            material = new Material("Alambre", 800, 200);
            Company empresa = new Company("Ferretería Mdeo", "herramientas", location, "091234567");
            RoleUserCompany role = new RoleUserCompany(empresa);
            publication = new Publication("Publicacion especial", empresa, location, material);
            // RoleUserCompany.ContactCompany(publication);
            publication.ClosePublication();

            result = salesReportHandler.Handle(testMessage, out response);
            Assert.That(response, Is.EqualTo("Publicaciones cerradas de los ultimos 30 dias de la empresa: Ferretería Mdeo"));
            Assert.That(result, Is.Not.Null);
        }
    }
}