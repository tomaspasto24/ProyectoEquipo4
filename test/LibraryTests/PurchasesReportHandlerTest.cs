using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    /// <summary>
    /// Test del funcionamiento del handler de reporte de compras.
    /// </summary>
    public class PurchasesReportHandler
    {
        UserInfo user1;
        Message testMessage;
        String response;
        IHandler result;
        Company company;

        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            SessionRelated.Instance = null;
            user1 = new UserInfo("name1", 5433261);
            user1.Permissions = UserInfo.EntrepreneurPermissions;
            SessionRelated.Instance.AddNewUser(user1);
        }
        /*
                [Test]

                public void PurchasesReportNoHasPermissionHandler()
                {
                    user1.Permissions = UserInfo.DefaultPermissions;
                    // IHandler PurchasesReportHandler = new PurchasesReportHandler(null);
                    user1.HandlerState = Bot.State.Start;
                    testMessage = new Message(5433261, "/reporte");
                    // result = purchasesReportHandler.Handle(testMessage, out response);

                    Assert.That(response, Is.Empty);
                    Assert.That(result, Is.Null);
                }
                */
    }
}