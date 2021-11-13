using System;
using NUnit.Framework;
using Bot;

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
            Assert.IsNotNull(companyTest.ListHistorialPublications);

            Publication publicationTest = new Publication("Test", companyTest, location, initialMaterial);
            companyTest.AddListHistorialPublication(publicationTest);

            Assert.That(companyTest.ListHistorialPublications.Count == 1);
        }
    
        /// <summary>
        /// Test que se encarga de verificar que funcionen bien los métodos que involucran la lista
        /// de publicaciones propias de la empresa.
        /// </summary>
        [Test]
        public void OwnPublicationsTest()
        {
            Assert.IsNotNull(companyTest.ListOwnPublications);

            Publication publicationTest = new Publication("Test", companyTest, location, initialMaterial);
            companyTest.AddOwnPublication(publicationTest);

            Assert.That(companyTest.ListOwnPublications.Count == 1);

            companyTest.DeleteOwnPublication(publicationTest);

            Assert.IsEmpty(companyTest.ListOwnPublications);
        }

        /// <summary>
        /// Test que se encarga de validar que los usuarios puedan añadirse a una clase Empresa y también
        /// puedan ser eliminados.
        /// </summary>
        [Test]
        public void UsersCompanyTest()
        {
            User usuarioTest1 = new User("Test1", 12, new RoleUserCompany(companyTest, "Test1", 12));
            User usuarioTest2 = new User("Test2", 22, new RoleUserCompany(companyTest, "Test2", 22));
            companyTest.AddUser(usuarioTest1);
            companyTest.AddUser(usuarioTest2);

            Assert.That(companyTest.ListUsers.Count == 2);

            companyTest.DeleteUser(usuarioTest1);

            Assert.That(companyTest.ListUsers.Count == 1);
        }
    }
}