using System;
using NUnit.Framework;
using Bot;
using System.Threading.Tasks;
using Nito.AsyncEx;
using Ucu.Poo.Locations.Client;

namespace BotTests
{
    /// <summary>
    /// GeoLocationTests se encarga de testear los atributos y las funcionalidades de la clase
    /// GeoLocation.
    /// </summary>
    public class GeoLocationTests
    {
        GeoLocation location {get; set;}
        string address {get; set;}
        string city {get; set;}
        string departament {get; set;}

        /// <summary>
        /// Este método se encarga de inicializar los atributos address, city y departament.
        /// Además de construir el objeto a testear location.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            address = "8 de octubre";
            city = "Montevideo";

            location = new GeoLocation(address, city);
        }

        /// <summary>
        /// Test que se encarga de comprobar la instancia de la clase GeoLocation.
        /// </summary>
        [Test]
        public void TestLocationInstance()
        {
            Assert.IsNotNull(location.GetLocation());
            Assert.IsNotNull(location.City);
            Assert.IsNotNull(location.Address);
        }

        /// <summary>
        /// Test que se encarga de testear el calculo de duración que realiza 
        /// el método CalculateDuration de la clase GeoLocation.
        /// </summary>
        [Test]
        public void TestDuration()
        {
            GeoLocation secondLocation = new GeoLocation("8 de octubre y comercio", "Montevideo");
            double durationTest1 = location.CalculateDuration(secondLocation);
            double durationTest2 = secondLocation.CalculateDuration(location);

            Assert.IsNotNull(durationTest1);
            Assert.IsNotNull(durationTest2);
            Assert.That(durationTest1 == durationTest2);
        }

        /// <summary>
        /// Test que se encarga de testear el calculo de distancia que realiza 
        /// el método CalculateDistance de la clase GeoLocation.
        /// </summary>        
        [Test]
        public void TestDistance()
        {
            GeoLocation secondLocation = new GeoLocation("8 de octubre y comercio", "Montevideo");
            double distanceTest1 = location.CalculateDistance(secondLocation);
            double distanceTest2 = secondLocation.CalculateDistance(location);

            Assert.IsNotNull(distanceTest1);
            Assert.IsNotNull(distanceTest2);
            Assert.That(distanceTest1 == distanceTest2);
        }
    }
}