using Bot;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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
            publicationToCompare.AddRating("Habilitación de Prueba");
            publicationToCompare.AddRating("Habilitación de Prueba1");
            publicationToCompare.AddRating("Habilitación de Prueba2");
            PublicationSet.AddPublication("PublicationTest", companyTest, location, initialMaterial);

            Assert.That(publicationToCompare.Title == "PublicationTest");
            Assert.That(publicationToCompare.Date is DateTime);
            
            Assert.IsNotNull(publicationToCompare);
            Assert.IsNotNull(PublicationSet.listPublications);
            Assert.That(publicationToCompare.ReturnListRatings() is not null);
            Assert.That(publicationToCompare.DeleteMaterial(0));
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
            PublicationSet.listPublications[0].AddMaterial(material1);
            PublicationSet.listPublications[0].AddMaterial(material2);
            PublicationSet.listPublications[0].AddMaterial(material3);

            Assert.IsNotEmpty(PublicationSet.listPublications[0].ReturnListMaterials());
        }

        [Test]
        public void TestPublicationClosed()
        {
            Publication publicationToCompare;
            publicationToCompare = new Publication("PublicationTest", companyTest, location, initialMaterial);

            Assert.IsNull(publicationToCompare.ClosePublication());
            Assert.IsTrue(publicationToCompare.IsClosed);     
            Assert.That(publicationToCompare.ClosedDate is DateTime);                  
        }

        [Test]
        public void TestPublicationClosedWithInterestedPerson()
        {
            Publication publicationToCompare;
            publicationToCompare = new Publication("PublicationTest", companyTest, location, initialMaterial);
            RoleEntrepreneur entrepreneur = new RoleEntrepreneur("Prueba", 20, "Prueba", location, "Prueba", "Prueba");

            entrepreneur.AskContactToPublication(publicationToCompare);
            Assert.IsInstanceOf(typeof(RoleEntrepreneur), publicationToCompare.ClosePublication()); 

            Assert.IsNotNull(publicationToCompare.interestedPerson);
            Assert.That(entrepreneur.ListHistorialPublications.Contains(publicationToCompare));    
        }
    }
}