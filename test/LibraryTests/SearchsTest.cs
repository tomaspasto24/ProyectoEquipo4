using Bot;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BotTests
{
    public class SearchTests
    {
        RoleEntrepreneur emprendedor;
        Material material;
        Material material2;
        Publication publicacion;
        GeoLocation location;
        [SetUp]
        public void Setup()
        {
            location = new GeoLocation("Av. Italia", "Montevideo", "Montevideo");
            
            emprendedor = new RoleEntrepreneur("Pedrito", 4, "herrería", location, "", "");

            material = new Material("Alambre", 800, 200);
            material2 = new Material("Alambre2", 1000, 300);
            Company empresa = new Company("Ferretería Mdeo", "herramientas", location, "091234567");
            publicacion = new Publication("Publicacion especial", empresa, location, material);
            publicacion.AddMaterial(material2);
        }

        /// <summary>
        /// El resultado de la búsqueda contiene a la publicación y esta contiene al material buscado,
        /// cada material tiene una lista de palabras claves
        /// </summary>
        
        /*[Test]
        public void SearchByMaterialTest()
        {             
            List<Publication> resultadoBusqueda = new List<Publication>();
            string keyWord = "alambre";
            material2.AddKeyWord(keyWord);
            resultadoBusqueda = emprendedor.SearchingByMaterials("alambre"); 
            
            Assert.IsTrue(resultadoBusqueda.Contains(publicacion));
            Assert.IsTrue(publicacion.ReturnListMaterials().Contains(material2));
        }*/

        /// <summary>
        /// El resultado de la búsqueda contiene a la publicación y esta al material con la ubicación buscada
        /// </summary>
        public void SearchByLocationTest()
        {             
            List<Publication> resultadoBusqueda = new List<Publication>();
            resultadoBusqueda = emprendedor.SearchingByLocation("Av.Italia");
            
            Assert.IsTrue(resultadoBusqueda.Contains(publicacion));
            Assert.IsTrue(publicacion.ReturnListMaterials().Contains(material2));
        }
    }
}