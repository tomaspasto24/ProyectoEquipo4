using Bot;
using NUnit.Framework;
using System.IO;

namespace BotTests
{
    public class DeserializeManagerTests
    {
        private const string PathContainerCompany = @"..\..\..\..\..\docs\CompanyDataBase.json";
        private const string PathContainerPublication = @"..\..\..\..\..\docs\PublicationDataBase.json";
        private const string PathContainerToken = @"..\..\..\..\..\docs\TokenDataBase.json";
        private const string PathContainerSessionRelated = @"..\..\..\..\..\docs\UserDataBase.json";
        GeoLocation locationTest = new GeoLocation("Universidad Católica", "Montevideo");

        [Test]
        public void DeserializeCompaniesTest()
        {
            Company companyTest1 = new Company("Test1", "TestItem1", locationTest, "TestContact");
            Company companyTest2 = new Company("Test2", "TestItem2", locationTest, "TestContact");
            Company companyTest3 = new Company("Test3", "TestItem3", locationTest, "TestContact");

            CompanySet.Instance.AddElement(companyTest1);
            CompanySet.Instance.AddElement(companyTest2);
            CompanySet.Instance.AddElement(companyTest3);

            SerializeManager.Instance.SerializeProgram();
            
            CompanySet.Instance.DeleteElement(companyTest1);
            CompanySet.Instance.DeleteElement(companyTest2);
            CompanySet.Instance.DeleteElement(companyTest3);

            bool conditionDeserialize = DeserializeManager.Instance.DeserializeProgram();

            Assert.IsTrue(conditionDeserialize);
        }

        [Test]
        public void DeserializePublicationsTest()
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

            bool conditionDeserialize = DeserializeManager.Instance.DeserializeProgram();

            Assert.IsTrue(conditionDeserialize);
        }

        [Test]
        public void DeserializeTokenTest()
        {
            int tokenTest1 = TokenGenerator.Instance.GenerateToken();
            SerializeManager.Instance.SerializeProgram();
            
            bool conditionDeserialize = DeserializeManager.Instance.DeserializeProgram();

            Assert.IsTrue(conditionDeserialize);
        }

        [Test]
        public void DeserializeSessionRelated()
        {
            SerializeManager.Instance.SerializeProgram();

            bool conditionDeserialize = DeserializeManager.Instance.DeserializeProgram();

            Assert.IsTrue(conditionDeserialize);
        }
    }
}