using System;
using Bot;
using NUnit.Framework;

namespace BotTests
{
    /// <summary>
    /// Clase para testear la clase PublicationSet.
    /// </summary>
    public class PublicationSetTest
    {
        GeoLocation location = new GeoLocation("Universidad Católica", "Montevideo");
        Publication publicationTest1;
        Publication publicationTest2;
        Publication publicationTest3;
        Company companyTest;
        Material initialMaterial;

        /// <summary>
        /// SetUp, asigna valores a las variables companyTest, initialMaterial, publicationTest1., publicationTest2, publicationTest3.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            companyTest = new Company("Prueba1", "Prueba", location, "0922877272");
            initialMaterial = new Material("MaterialTest", 12, 0);

            publicationTest1 = new Publication("Prueba1", companyTest, location, initialMaterial);
            publicationTest2 = new Publication("Prueba1", companyTest, location, initialMaterial);
            publicationTest3 = new Publication("Prueba2", companyTest, location, initialMaterial);
        }

        /// <summary>
        /// Test que se encarga de testear el funcionamiento de la clase PublicationSet de agregar
        /// clases Publicación al sistema.
        /// </summary>
        [Test]
        public void AddPublicationTest()
        {
            bool test1 = PublicationSet.Instance.AddElement(publicationTest1);
            bool test2 = PublicationSet.Instance.AddElement(publicationTest2);
            bool test3 = PublicationSet.Instance.AddElement(publicationTest3);
            string stringPublicationsTest = PublicationSet.Instance.ReturnListElements();

            Assert.IsTrue(test1);
            Assert.IsFalse(test2);
            Assert.IsTrue(test3);
        }

        /// <summary>
        /// Test que se encarga de comprobar que el funcionamiento de ContainsPublicationInListPublications sea el 
        /// correcto y que se puedan identificar las pbulicaciones contenidas.
        /// </summary>
        [Test]
        public void ContainsPublicationInListCompaniesTest()
        {
            bool conditionTest1 = PublicationSet.Instance.ContainsElementInListElements(publicationTest1);
            bool conditionTest2 = PublicationSet.Instance.ContainsElementInListElements(publicationTest2);
            bool conditionTest3 = PublicationSet.Instance.ContainsElementInListElements(publicationTest3);

            Assert.IsTrue(conditionTest1);
            Assert.IsTrue(conditionTest2);
            Assert.IsTrue(conditionTest3);
        }

        /// <summary>
        /// Test que se encarga de testear el funcionamiento de la clase PublicationSet de eliminar 
        /// clases Publicación del sistema.
        /// </summary>
        [Test]
        public void DeletePublicationTest()
        {
            bool test4 = PublicationSet.Instance.DeleteElement(publicationTest1);
            bool test5 = PublicationSet.Instance.DeleteElement(publicationTest3);

            Assert.IsTrue(test4);
            Assert.IsTrue(test5);
            Assert.IsEmpty(PublicationSet.Instance.ListPublications);
            Assert.IsFalse(PublicationSet.Instance.ContainsElementInListElements(publicationTest1));
            Assert.IsFalse(PublicationSet.Instance.ContainsElementInListElements(publicationTest3));
        }

        /// <summary>
        /// ReturnListElementsTest testea que el string de la lista de Publicaciones funcione bien.
        /// </summary>
        [Test]
        public void ReturnListElementsTest()
        {
            PublicationSet.Instance.AddElement(publicationTest1);
            PublicationSet.Instance.AddElement(publicationTest3);

            string stringTest = PublicationSet.Instance.ReturnListElements();
            System.Console.WriteLine(stringTest);

            Assert.That(stringTest is string);
            Assert.That(stringTest.Contains("Publicaciones:"));
            Assert.That(stringTest.Contains("Prueba1"));
            Assert.That(stringTest.Contains("Prueba2"));
        }

        /// <summary>
        /// ContainsPublicationInListCompaniesTest comprueba que el funcionamiento de encontrar
        /// si las clases Publicación se encuentran en el sistema con la condición del nombre de la Publicación.
        /// </summary>
        [Test]
        public void ContainsCompanyInListPublicationsTest()
        {
            PublicationSet.Instance.AddElement(publicationTest1);
            PublicationSet.Instance.AddElement(publicationTest3);

            string namePublicationTest1 = publicationTest1.Title;
            string namePublicationTest2 = publicationTest2.Title;

            bool test6 = PublicationSet.Instance.ContainsElementInListElements(namePublicationTest1);
            bool test7 = PublicationSet.Instance.ContainsElementInListElements(namePublicationTest2);

            Assert.IsTrue(PublicationSet.Instance.ContainsElementInListElements(publicationTest1));
            Assert.IsTrue(PublicationSet.Instance.ContainsElementInListElements(publicationTest2));
            Assert.IsTrue(PublicationSet.Instance.ContainsElementInListElements(publicationTest3));

            Assert.IsTrue(PublicationSet.Instance.ContainsElementInListElements(publicationTest1));
            Assert.IsTrue(PublicationSet.Instance.ContainsElementInListElements(publicationTest2));
        }
    }
}