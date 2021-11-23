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
            Assert.That(companyTest.Name.Equals("Test"));
            Assert.That(companyTest.Item.Equals("TestItem"));
            Assert.That(companyTest.location == location);
            Assert.That(companyTest.Contact.Equals("TestContact"));
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
            List<UserInfo> listUser = new List<UserInfo>();
            UserInfo userTest1 = new UserInfo("Test1", 12, new RoleUserCompany(companyTest));
            UserInfo userTest2 = new UserInfo("Test2", 22, new RoleUserCompany(companyTest));
            companyTest.AddUser(userTest1);
            companyTest.AddUser(userTest2);

            Assert.IsNotNull(companyTest.ListUsers);

            listUser.Add(userTest1);
            listUser.Add(userTest2);
            companyTest.AddUser(listUser);

            Assert.That(companyTest.ListUsers.Count == 4);
            companyTest.DeleteUser(userTest1);
            Assert.That(companyTest.ListUsers.Count == 3);
        }

        /// <summary>
        /// Testea que el método ReturnContactTest retorne la cadena de carácteres con la información correcta.
        /// </summary>
        [Test]
        public void ReturnContactTest()
        {
            string stringTest = companyTest.ReturnContact();
            System.Console.WriteLine(stringTest);

            Assert.That(stringTest is string);
            Assert.That(stringTest.Contains("Empresa:"));
            Assert.That(stringTest.Contains("Rubro:"));
            Assert.That(stringTest.Contains("Contacto:"));
            Assert.That(stringTest.Contains("Test"));
            Assert.That(stringTest.Contains("TestItem"));
            Assert.That(stringTest.Contains("TestContact"));
        }
    }
}