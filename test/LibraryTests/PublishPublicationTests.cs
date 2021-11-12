using Bot;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BotTests
{
    /// <summary>
    /// PublishPublicationTests se encarga de testear el funcionamiento de la 
    /// funcionalidad de Publicar Publicación que en un futuro será implementada 
    /// en el Bot de Telegram como /publicar.
    /// </summary>
    public class PublishPublicationTests
    {
        GeoLocation location;
        Company companyTest;
        Material initialMaterial;
        RoleEntrepreneur entrepreneur;

        /// <summary>
        /// Método que crea y asgina las instancias a los atributos location, companyTest, initialMaterial,
        /// entrepreneurLocation, entrepreneur; que serán usados por los siguientes métodos. 
        /// </summary>
        [SetUp]
        public void Setup()
        {
            location = new GeoLocation("Universidad Católica", "Montevideo");
            companyTest = new Company("Test", "itemTest", location, "093929434");
            initialMaterial = new Material("Wood", 15, 0);
            GeoLocation entrepreneurLocation = new GeoLocation("Camino Maldonado 2416", "Montevideo");
            entrepreneur = new RoleEntrepreneur("emprendedor1", 5433264, "carpintero", entrepreneurLocation, "oficial", "lustrado");

        }

        /// <summary>
        /// Este test se encarga de crear una publicación simple y de comprobar que sus 
        /// atributos no sean null.
        /// </summary>
        [Test]
        public void TestSimplePublication()
        {
            Publication publicationToCompare;

            publicationToCompare = new Publication("PublicationTest", companyTest, location, initialMaterial);
            publicationToCompare.AddQualification("Habilitación de Prueba");
            publicationToCompare.AddQualification("Habilitación de Prueba1");
            publicationToCompare.AddQualification("Habilitación de Prueba2");
            PublicationSet.AddPublication("PublicationTest", companyTest, location, initialMaterial);

            Assert.That(publicationToCompare.Title == "PublicationTest");

            Assert.IsNotNull(publicationToCompare);
            Assert.IsNotNull(PublicationSet.ListPublications);
            Assert.That(publicationToCompare.DeleteMaterial(initialMaterial));
        }

        /// <summary>
        /// Test que se encarga de comprobar la creación de una publicación y además de crear materiales
        /// para comprobar que se agregan a la lista de materiales de la publicación.
        /// </summary>
        [Test]
        public void TestPublishPublicationAndAddMaterials()
        {
            Publication publicationToCompare;
            Material material1 = new Material("Iron", 20, 0);
            Material material2 = new Material("Toys", 10, 800);
            Material material3 = new Material("Paper", 60, 100);

            publicationToCompare = new Publication("PublicationTest", companyTest, location, initialMaterial);
            PublicationSet.AddPublication("PublicationTest", companyTest, location, initialMaterial);
        }

        /// <summary>
        /// Test que se encarga de comprobar el funcionamiento de la clase de cerrarse
        /// a si misma.
        /// </summary>
        [Test]
        public void TestPublicationClosed()
        {
            Publication publicationToCompare;
            publicationToCompare = new Publication("PublicationTest", companyTest, location, initialMaterial);

            Assert.IsNull(publicationToCompare.ClosePublication());
            Assert.IsTrue(publicationToCompare.IsClosed);
            Assert.IsNotNull(publicationToCompare.ClosedDate);
        }

        /// <summary>
        /// Test que se encarga de comprobar el funcionamiento de la clase de cerrarse
        /// a si misma y de setar y devolver a la persona interesada.
        /// </summary>
        [Test]
        public void TestPublicationClosedWithInterestedPerson()
        {
            Publication publicationToCompare;
            publicationToCompare = new Publication("PublicationTest", companyTest, location, initialMaterial);
            RoleEntrepreneur entrepreneur = new RoleEntrepreneur("Prueba", 20, "Prueba", location, "Prueba", "Prueba");

            entrepreneur.ContactCompany(publicationToCompare);
            Assert.IsInstanceOf(typeof(RoleEntrepreneur), publicationToCompare.ClosePublication()); 

            Assert.IsNotNull(publicationToCompare.interestedPerson);
            Assert.That(entrepreneur.ReturnListHistorialPublications().Contains(publicationToCompare));    
        }
    }
}