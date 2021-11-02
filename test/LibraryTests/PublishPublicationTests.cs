using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    public class PublishPublicationTests
    {
        GeoLocation location;
        Company companyTest;
        Material initialMaterial;

        [SetUp]
        public void Setup()
        {
            location = new GeoLocation("Universidad Católica", "Montevideo", "Montevideo");
            companyTest = new Company("Test", "itemTest", location, "093929434");
            initialMaterial = new Material("Wood", 15, 0);
        }

        [Test]
        public void TestSimplePublication()
        {
            Publication publicationToCompare;

            publicationToCompare = new Publication("PublicationTest", companyTest, location, initialMaterial);
            PublicationSet.AddPublication("PublicationTest", companyTest, location, initialMaterial);

            Assert.IsNotNull(publicationToCompare);
            Assert.Contains(publicationToCompare, PublicationSet.ListPublications);
            Assert.AreEqual(publicationToCompare, PublicationSet.ListPublications[0]);
        }

        [Test]
        public void TestPublishPublicationAndAddMaterials()
        {
            Publication publicationToCompare;
            Material material1 = new Material("Iron", 20, 0);
            Material material2 = new Material("Toys", 10, 800);
            Material material3 = new Material("Paper", 60, 100);

            publicationToCompare = new Publication("PublicationTest", companyTest, location, initialMaterial);
            PublicationSet.AddPublication("PublicationTest", companyTest, location, initialMaterial);
            PublicationSet.ListPublications[0].AddMaterial(material1);
            PublicationSet.ListPublications[0].AddMaterial(material2);
            PublicationSet.ListPublications[0].AddMaterial(material3);

            Assert.Contains(material1, PublicationSet.ListPublications);
            Assert.Contains(material2, PublicationSet.ListPublications);
            Assert.Contains(material3, PublicationSet.ListPublications);
        }
    }
}