using Bot;
using NUnit.Framework;
using System.IO;

namespace BotTests
{
    public class SerializeManagerTests
    {
        private const string PathContainerCompany = @"..\..\..\..\..\docs\CompanyDataBase.json";
        private const string PathContainerPublication = @"..\..\..\..\..\docs\PublicationDataBase.json";
        private const string PathContainerToken = @"..\..\..\..\..\docs\TokenDataBase.json";
        private const string PathContainerSessionRelated = @"..\..\..\..\..\docs\UserDataBase.json";
        GeoLocation locationTest = new GeoLocation("Universidad Católica", "Montevideo");

        [Test]
        public void SerializeCompaniesTest()
        {
            Company companyTest1 = new Company("Test1", "TestItem1", locationTest, "TestContact");
            Company companyTest2 = new Company("Test2", "TestItem2", locationTest, "TestContact");
            Company companyTest3 = new Company("Test3", "TestItem3", locationTest, "TestContact");
            string jsonTest = File.ReadAllText(PathContainerCompany);

            CompanySet.Instance.AddElement(companyTest1);
            CompanySet.Instance.AddElement(companyTest2);
            CompanySet.Instance.AddElement(companyTest3);

            SerializeManager.Instance.SerializeProgram();

            Assert.That(File.Exists(PathContainerCompany));
            Assert.IsNotEmpty(jsonTest);
            Assert.That(jsonTest is string);
            Assert.That(jsonTest.Contains("Test1"));
            Assert.That(jsonTest.Contains("Test2"));
            Assert.That(jsonTest.Contains("Test3"));
        }

        [Test]
        public void SerializePublicationsTest()
        {
            Company companyTest1 = new Company("Test1", "TestItem1", locationTest, "TestContact");
            Material Materialtest = new Material("Wood", 10, 0);
            Publication publicationTest1 = new Publication("Test1", companyTest1, locationTest, Materialtest);
            Publication publicationTest2 = new Publication("Test2", companyTest1, locationTest, Materialtest);
            Publication publicationTest3 = new Publication("Test3", companyTest1, locationTest, Materialtest);
            string jsonTest = File.ReadAllText(PathContainerCompany);

            PublicationSet.Instance.AddElement(publicationTest1);
            PublicationSet.Instance.AddElement(publicationTest2);
            PublicationSet.Instance.AddElement(publicationTest3);

            SerializeManager.Instance.SerializeProgram();

            Assert.That(File.Exists(PathContainerPublication));
            Assert.IsNotEmpty(jsonTest);
            Assert.That(jsonTest is string);
            Assert.That(jsonTest.Contains("Test1"));
            Assert.That(jsonTest.Contains("Test2"));
            Assert.That(jsonTest.Contains("Test3"));
        }

        [Test]
        public void SerializeTokenTest()
        {
            int tokenTest1 = TokenGenerator.Instance.GenerateToken();
            SerializeManager.Instance.SerializeProgram();
            string jsonTestToken = File.ReadAllText(PathContainerToken);

            Assert.That(File.Exists(PathContainerToken));
            Assert.IsNotNull(jsonTestToken);
            Assert.IsNotEmpty(jsonTestToken);
        }

        [Test]
        public void SerializeSessionRelated()
        {
            SerializeManager.Instance.SerializeProgram();
            string jsonTestSessionRelated = File.ReadAllText(PathContainerSessionRelated);

            Assert.That(File.Exists(PathContainerSessionRelated));
            Assert.IsNotNull(jsonTestSessionRelated);
            Assert.IsNotEmpty(jsonTestSessionRelated);
        }
    }
}