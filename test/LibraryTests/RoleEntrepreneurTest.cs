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
        EntrepreneurInfo emprendedor;
        GeoLocation ubicacionParaPruebas;
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
            emprendedor = new EntrepreneurInfo(heading, ubicacionParaPruebas);
        }

        /// <summary>
        /// Test del método que le agrega una certificación al emprendedor.
        /// </summary>
        [Test]
        public void AddCertificationTest()
        {
            certification = "soldadora";
            emprendedor.AddCertification(certification);
            Assert.AreEqual("soldadora", emprendedor.GetCertifications());
        }


        /// <summary>
        /// Test del método que le agrega una especialización al emprendedor
        /// </summary>
        [Test]
        public void AddSpecializationTest()
        {
            specialization = "Quimica";
            emprendedor.AddSpecialization(specialization);
            // Assert.AreEqual("Quimica", emprendedor.ReturnSpecialization()[1]);
        }

        /// <summary>
        /// Test del método que guarda las publicaciónes adquiridas por el emprendedor, se le agrega la publicación 
        /// y se fija que la lista no esté vacía
        /// </summary>
        [Test]
        public void AddListHistorialPublicationsTest1()
        {
            EntrepreneurInfo emprendedor2 = new EntrepreneurInfo("Industria", ubicacionParaPruebas);
            Company empresaDeVidrios = new Company("vidrioglass", "vidrio", ubicacionParaPruebas, "vidrioglas@correo.com");
            Material vidrio = new Material("Vidrio", 100, 850);
            Publication publicacion = new Publication("Publicación de vidrio", empresaDeVidrios, ubicacionParaPruebas, vidrio);
            emprendedor.AddHistorialPublication(publicacion);
            Assert.IsNotEmpty(emprendedor2.ListHistorialPublications);
        }

        /// <summary>
        /// Test del método que guarda las publicaciónes adquiridas por el emprendedor, se le agrega la publicación 
        /// y se fija la lista sea la correcta
        /// </summary>
        [Test]
        public void AddHistorialPublicationTest2()
        {
            EntrepreneurInfo emprendedor3 = new EntrepreneurInfo("Construcción", ubicacionParaPruebas);
            Company pvcCompany = new Company("pvCompany", "plasticos", ubicacionParaPruebas, "pvcventas@correo.com");
            Material pvc = new Material("PVC", 200, 500);
            Publication publicacion = new Publication("Publicación de plástico", pvcCompany, ubicacionParaPruebas, pvc);
            emprendedor.AddHistorialPublication(publicacion);
            List<Publication> listaPublicacionesEsperada = new List<Publication>();
            listaPublicacionesEsperada.Add(publicacion);
            Assert.AreEqual(listaPublicacionesEsperada, emprendedor.ListHistorialPublications);
        }
        /// <summary>
        /// Verifica que la ubicación que se le devuelve al emprendedor sea efectivamente la correcta
        /// </summary>
        [Test]
        public void ContactCompanyTest()
        {
            Company pvcCompany = new Company("pvCompany", "plasticos", ubicacionParaPruebas, "pvcventas@correo.com");
            Material pvc = new Material("PVC", 200, 500);
            Publication publicacion = new Publication("Publicación de plástico", pvcCompany, ubicacionParaPruebas, pvc);
            Assert.AreEqual(pvcCompany.ReturnContact(), emprendedor.ContactCompany(publicacion));
        }
    }
}
