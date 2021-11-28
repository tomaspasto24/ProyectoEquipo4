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
        EntrepreneurInfo entrepreneur;    
        Material material;
        Material material2;
        Publication publication;
        GeoLocation location;
        /// <summary>
        /// Se crean dos instancias de Material y una de Company para poder crear una de Publication y usarlas en los dos test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            location = new GeoLocation("Av. Italia", "Montevideo");
            entrepreneur = new EntrepreneurInfo("Smithy", location);

            material = new Material("Wire", 2000, 200);
            material2 = new Material("Wire2", 1000, 300);
            Company company1 = new Company("Ironmongery Mdeo", "tools", location, "091234567");
            publication = new Publication("Special publication", company1, location, material);
            publication.AddMaterial(material2);
        }

        /// <summary>
        /// El resultado de la búsqueda es una publicación como string y esta contiene al material con la palabra clave buscada,
        /// cada material tiene una lista de palabras claves.
        /// </summary>
        [Test]
        public void SearchByMaterialTest()
        {
            string expectedResult;
            string keyWord = "wire";
            material2.AddKeyWord(keyWord);
            SearchByMaterial sM = new SearchByMaterial();
            expectedResult = publication.Title + "\n"+  publication.Company.Name+ "\n" + "Materiales:\n" + material2.Name +" - "+ material2.Quantity +" - " + material2.Price;
            Assert.AreEqual(expectedResult, sM.Search("wire"));
            Assert.AreEqual(publication.ReturnPublication(), sM.Search("wire"));
            List<Material> materialList = (List<Bot.Material>) publication.ListMaterials;
            Assert.IsTrue(materialList.Contains(material2));
        }

        /// <summary>
        /// El resultado de la búsqueda es la publicación que se encuentra a partir de la ubicación ingresada.
        /// </summary>
        [Test]
        public void SearchByLocationTest()
        {
            string expectedResult;
            SearchByLocation sL = new SearchByLocation();
            expectedResult = publication.ReturnPublication();
            Assert.AreEqual(expectedResult, sL.Search("Av.Italia"));
            Assert.AreEqual(publication.ReturnPublication(), sL.Search("Av.Italia"));
        }
    }
}