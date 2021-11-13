using System;
using NUnit.Framework;
using Bot;
using System.Collections.Generic;

namespace BotTests
{
    /// <summary>
    /// Clase para testear la clase Company.
    /// </summary>
    public class CompanyTest
    {
        GeoLocation location;
        Company companyTest;
        Material initialMaterial;

        /// <summary>
        /// SetUp, asigna valores a las variables location, companyTest, initialMaterial.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            location = new GeoLocation("Universidad Católica", "Montevideo");
            companyTest = new Company("Test", "TestItem", location, "TestContact");
            initialMaterial = new Material("MaterialTest", 12, 0);
        }

        /// <summary>
        /// Test que se encarga de verificar que la instancia de la clase Empresa y sus atributos
        /// internos esten correctamente definidos.
        /// </summary>
        [Test]
        public void InstanceTest()
        {
            Assert.IsNotNull(companyTest);
            Assert.That(companyTest.Name == "Test");
            Assert.That(companyTest.Item == "TestItem");
            Assert.That(companyTest.Location == location);
            Assert.That(companyTest.Contact == "TestContact");
        }
        
        /// <summary>
        /// Test que se encarga de comprobar el correcto funcionamiento de la lista historial de publicaciones.
        /// </summary>
        [Test]
        public void HistorialPublicationsTest()
        {
            Publication publicationTest = new Publication("Test1", companyTest, location, initialMaterial);
            List<Publication> listPublicationTest = new List<Publication>();
            listPublicationTest.Add(publicationTest);
            listPublicationTest.Add(new Publication("Test2", companyTest, location, initialMaterial));
            listPublicationTest.Add(new Publication("Test3", companyTest, location, initialMaterial));
            listPublicationTest.Add(new Publication("Test4", companyTest, location, initialMaterial));

            Assert.IsNotNull(companyTest.ListHistorialPublications);            
            companyTest.AddListHistorialPublication(publicationTest);
            Assert.That(companyTest.ListHistorialPublications.Count == 1);
            companyTest.AddListHistorialPublication(listPublicationTest);
            Assert.That(companyTest.ListHistorialPublications.Count == 5);
        }
    
        /// <summary>
        /// Test que se encarga de verificar que funcionen bien los métodos que involucran la lista
        /// de publicaciones propias de la empresa.
        /// </summary>
        [Test]
        public void OwnPublicationsTest()
        {
            Publication publicationTest = new Publication("Test", companyTest, location, initialMaterial);
            List<Publication> listPublicationTest = new List<Publication>();
            listPublicationTest.Add(publicationTest);
            listPublicationTest.Add(new Publication("Test2", companyTest, location, initialMaterial));
            listPublicationTest.Add(new Publication("Test3", companyTest, location, initialMaterial));
            listPublicationTest.Add(new Publication("Test4", companyTest, location, initialMaterial));

            Assert.IsNotNull(companyTest.ListOwnPublications);
            
            companyTest.AddOwnPublication(publicationTest);
            Assert.That(companyTest.ListOwnPublications.Count == 1);
            companyTest.DeleteOwnPublication(publicationTest);
            Assert.IsEmpty(companyTest.ListOwnPublications);

            companyTest.AddOwnPublication(listPublicationTest);
            Assert.That(companyTest.ListOwnPublications.Count == 4);
            companyTest.DeleteOwnPublication(publicationTest);
            Assert.That(companyTest.ListOwnPublications.Count == 3);
        }

        /// <summary>
        /// Test que se encarga de validar que los usuarios puedan añadirse a una clase Empresa y también
        /// puedan ser eliminados.
        /// </summary>
        [Test]
        public void UsersCompanyTest()
        {
            User userTest1 = new User("Test1", 12, new RoleUserCompany(companyTest, "Test1", 12));
            User userTest2 = new User("Test2", 22, new RoleUserCompany(companyTest, "Test2", 22));
            List<User> listUsersTest = new List<User>();
            listUsersTest.Add(userTest1);
            listUsersTest.Add(userTest2);

            Assert.IsNotNull(companyTest.ListUsers);

            companyTest.AddUser(userTest1);
            companyTest.AddUser(userTest2);
            Assert.That(companyTest.ListUsers.Count == 2);
            companyTest.DeleteUser(userTest1);
            Assert.That(companyTest.ListUsers.Count == 1);

            companyTest.AddUser(listUsersTest);
            Assert.That(companyTest.ListUsers.Count == 3);

            companyTest.DeleteUser(userTest1);
            Assert.That(companyTest.ListUsers.Count == 2);
        }
    }
}