using Bot;
using NUnit.Framework;
using System;

namespace BotTests
{
    /// <summary>
    /// Test de la clase Material que se encarga de testear todas las funcionalidades
    /// y opciones que otorga la clase a los demás objetos.
    /// </summary>
    public class MaterialTests
    {
        /// <summary>
        /// Test que se encarga de comprobar los atributos
        /// básicos de la clase Material mediante la creación de un simple Material.
        /// </summary>
        [Test]
        public void CreateSimpleMaterial()
        {
            Material Materialtest = new Material("Wood", 10, 0);

            Assert.IsNotNull(Materialtest.Name);
            Assert.That(Materialtest.Name is string);
            Assert.That(Materialtest.Name.Equals("Wood"));

            Assert.IsNotNull(Materialtest.Price);
            Assert.That(Materialtest.Price == 0);

            Assert.IsNotNull(Materialtest.Quantity);
            Assert.That(Materialtest.Quantity == 10);

            Assert.IsNotNull(Materialtest.KeyWords);
        }

        /// <summary>
        /// Test que se encarga de testear la funcionalidad de obtener, agregar y quitar 
        /// palabras clave anidadas a la clase Material.
        /// </summary>
        [Test]
        public void KeyWordsTest()
        {
            Material Materialtest = new Material("Wood", 10, 0);

            Materialtest.AddKeyWord("TestKeyWord");
            Materialtest.AddKeyWord("TestKeyWord1");
            Materialtest.AddKeyWord("TestKeyWord2");

            Assert.IsNotEmpty(Materialtest.KeyWords);
            Assert.That(Materialtest.KeyWords.Count == 3);

            Assert.IsTrue(Materialtest.DeleteKeyWord("TestKeyWord"));
            Assert.That(Materialtest.KeyWords.Count == 2);
        }
    }
}