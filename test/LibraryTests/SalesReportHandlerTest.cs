using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    /// <summary>
    /// Clase SalesReportHandlerTest que se encarga de testear el comportamiento del hanlder.
    /// </summary>
    public class SalesReportHandlerTest
    {
        private UserInfo user1;
        private Message testMessage;
        private String response;
        private IHandler result;
        private GeoLocation location;
        private Material material;
        private Publication publication;

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del handler al enviar un mensaje y no tener el permiso necesario.
        /// </summary>
        [Test]
        public void SalesReportHandlerNoHasPermissionTest()
        {
            SessionRelated.Instance = null;
            this.user1 = new UserInfo("name1", 5433261);
            this.user1.Permissions = UserInfo.DefaultPermissions;
            SessionRelated.Instance.AddNewUser(this.user1);
            SalesReportHandler salesreportHandler = new SalesReportHandler(null);
            this.testMessage = new Message(5433261, "");

            this.result = salesreportHandler.Handle(this.testMessage, out this.response);
            Assert.That(this.result, Is.Null);
        }

        /// <summary>
        /// Test que se encarga de verificar el comportamiento del handler al enviar el comando "/reporte".
        /// </summary>
        [Test]
        public void SalesReportHandlerHasPermissionTest()
        {
            SessionRelated.Instance = null;
            Company company = new Company("Ferretería Mdeo", "herramientas", this.location, "091234567");

            this.user1 = new("name1", 5433261);
            this.user1.Permissions = UserInfo.UserCompanyPermissions;
            SessionRelated.Instance.AddNewUser(this.user1);
            SalesReportHandler salesReportHandler = new(null);

            SessionRelated.Instance.DiccUserTokens.Add("IHaveAToken", company);
            SessionRelated.Instance.DiccUserCompanyInfo.Add(this.user1, new(SessionRelated.Instance.GetCompanyByToken("IHaveAToken")));

            this.location = new GeoLocation("Av. Italia", "Montevideo");
            this.material = new Material("Alambre", 800, 200);
            this.publication = new Publication("Publicacion especial", company, this.location, this.material);
            this.publication.ClosePublication();

            this.testMessage = new Message(5433261, "/reporte");
            this.user1.HandlerState = Bot.State.Start;
            this.result = salesReportHandler.Handle(this.testMessage, out this.response);

            Assert.That(this.response.Contains("Ferretería Mdeo"));
            Assert.That(this.result, Is.Not.Null);
        }
    }
}
