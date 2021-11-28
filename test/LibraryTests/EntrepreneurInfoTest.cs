using System.Collections.Generic;
using Bot;
using NUnit.Framework;

namespace BotTests
{
    /// <summary>
    /// Tests de los métodos de la clase RoleEntrepreneur.
    /// </summary>
    public class RoleEntrepreneurTests
    {
        EntrepreneurInfo entrepreneur;
        GeoLocation locationForTests;
        string name;
        int id;
        string heading;
        string certification;
        string specialization;
        /// <summary>
        /// Se crea una instancia de emprendedor que será usada para los tests de cada método.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            name = "Alejandra";
            id = 1;
            heading = "metalurgica";
            GeoLocation ubicacionParaPruebas = new GeoLocation("8 de OCtubre", "Montevideo");
            entrepreneur = new EntrepreneurInfo(heading, locationForTests);
        }

        /// <summary>
        /// Test del método que le agrega una certificación al emprendedor.
        /// </summary>
        [Test]
        public void AddCertificationTest()
        {
            certification = "soldadora";
            entrepreneur.AddCertification(certification);
            Assert.AreEqual("soldadora", entrepreneur.GetCertifications());
        }


        /// <summary>
        /// Test del método que le agrega una especialización al emprendedor.
        /// </summary>
        [Test]
        public void AddSpecializationTest()
        {
            specialization = "Quimica";
            entrepreneur.AddSpecialization(specialization);
            Assert.AreEqual("Quimica", entrepreneur.GetSpecializations());
        }

        /// <summary>
        /// Test del método que guarda las publicaciónes adquiridas por el emprendedor, se le agrega la publicación 
        /// y se fija que la lista no esté vacía.
        /// </summary>
        [Test]
        public void AddListHistorialPublicationsTest1()
        {
            EntrepreneurInfo entrepreneur1 = new EntrepreneurInfo("Crystals", locationForTests);
            Company glassCompany = new Company("GlassCompany", "glass", locationForTests, "glass@gmail.com");
            Material glass = new Material("Glass", 100, 850);
            Publication publicacion = new Publication("Glass publication", glassCompany, locationForTests, glass);
            entrepreneur1.AddHistorialPublication(publicacion);
            Assert.IsNotEmpty(entrepreneur1.ListHistorialPublications);
        }

        /// <summary>
        /// Verifica que la ubicación que se le devuelve al emprendedor sea la correcta.
        /// </summary>
        [Test]
        public void ContactCompanyTest()
        {
            Company pvcCompany = new Company("pvCompany", "plastic", locationForTests, "pvcvsales@gmail.com");
            Material pvc = new Material("PVC", 200, 500);
            Publication publicacion = new Publication("Plastic publication", pvcCompany, locationForTests, pvc);
            Assert.AreEqual(pvcCompany.ReturnContact(), entrepreneur.ContactCompany(publicacion));
        }
    }
}
