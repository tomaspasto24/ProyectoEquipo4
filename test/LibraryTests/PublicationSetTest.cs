using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    /// <summary>
    /// Clase para testear la clase CompanySet.
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
        /// Test que se encarga de testear el funcionamiento de la clase CompanySet de agregar
        /// clases Empresa al sistema.
        /// </summary>
        [Test]
        public void AddCompanyTest()
        {
            bool test1 = PublicationSet.Instance.AddElement(publicationTest1);
            bool test2 = PublicationSet.Instance.AddElement(publicationTest2);
            bool test3 = PublicationSet.Instance.AddElement(publicationTest3);
            string stringPublicationsTest = PublicationSet.Instance.ReturnListElements();

            Assert.IsTrue(test1);
            Assert.IsTrue(test2);
            Assert.IsTrue(test3);
        }

        /// <summary>
        /// Test que se encarga de comprobar que el funcionamiento de ContainsPublicationInListPublications sea el 
        /// correcto y que se puedan identificar las pbulicaciones contenidas. (IMPORTANTE CORRER AddPublicationTest PARA QUE ESTE TEST PASE).
        /// </summary>
        [Test]
        public void ContainsCompanyInListCompaniesTest()
        {
            bool conditionTest1 = PublicationSet.Instance.ContainsElementInListElements(publicationTest1);
            bool conditionTest2 = PublicationSet.Instance.ContainsElementInListElements(publicationTest2);
            bool conditionTest3 = PublicationSet.Instance.ContainsElementInListElements(publicationTest3);

            Assert.IsTrue(conditionTest1);
            Assert.IsTrue(conditionTest2);
            Assert.IsTrue(conditionTest3);
        }

        /// <summary>
        /// Test que se encarga de testear el funcionamiento de la clase CompanySet de eliminar 
        /// clases Empresa del sistema.
        /// </summary>
        [Test]
        public void DeleteCompanyTest()
        {
            bool test4 = PublicationSet.Instance.DeleteElement(publicationTest1);
            bool test5 = PublicationSet.Instance.DeleteElement(publicationTest2);

            Assert.IsTrue(test4);
            Assert.IsTrue(test5);
            Assert.IsEmpty(PublicationSet.Instance.ListPublications);
            Assert.IsFalse(PublicationSet.Instance.ContainsElementInListElements(publicationTest1));
            Assert.IsFalse(PublicationSet.Instance.ContainsElementInListElements(publicationTest2));
        }
    }
}