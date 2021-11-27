using Bot;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace BotTests
{
    /// <summary>
    /// Tests del método Search de las clases SearchByMaterial y SearchByLocation
    /// </summary>
    public class SearchTests
    {
        RoleEntrepreneur emprendedor;
        Material material;
        Material material2;
        Publication publicacion;
        GeoLocation location;
        /// <summary>
        /// Se crean dos instancias de Material y una de Company para poder crear una de Publication y usarlas en los dos test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            location = new GeoLocation("Av. Italia", "Montevideo");
            emprendedor = new RoleEntrepreneur("herrería", location);

            material = new Material("Alambre", 2000, 200);
            material2 = new Material("Alambre2", 1000, 300);
            Company empresa = new Company("Ferretería Mdeo", "herramientas", location, "091234567");
            publicacion = new Publication("Publicacion especial", empresa, location, material);
            publicacion.AddMaterial(material2);
        }

        /// <summary>
        /// El resultado de la búsqueda es una lista con la publicación y esta contiene al material buscado,
        /// cada material tiene una lista de palabras claves
        /// </summary>
        [Test]
        public void SearchByMaterialTest()
        {        
            bool test1 = PublicationSet.Instance.AddElement(publicacion);     
            List<Publication> resultadoBusqueda = new List<Publication>();
            string keyWord = "alambre";
            material2.AddKeyWord(keyWord);
            SearchByMaterial s = new SearchByMaterial();
            resultadoBusqueda = (List<Publication>) s.Search("alambre"); 
 
            Assert.IsTrue(resultadoBusqueda.Contains(publicacion));
            // Assert.IsTrue(publicacion.ReturnListMaterials().Contains(material2));
        }

        /// <summary>
        /// El resultado de la búsqueda es una lista con la publicación de igual ubicación a la buscada
        /// </summary>
        public void SearchByLocationTest()
        {
            List<Publication> resultadoBusqueda = new List<Publication>();
            // resultadoBusqueda = emprendedor.SearchingByLocation("Av.Italia");

            Assert.IsTrue(resultadoBusqueda.Contains(publicacion));
        }
    }
}