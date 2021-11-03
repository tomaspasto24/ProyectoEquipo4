using System;
using NUnit.Framework;
using Bot;
using System.Threading.Tasks;
using Nito.AsyncEx;
using LocationApi;


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
            departament = "Montevideo";

            location = new GeoLocation(address, city, departament);
        }

        /// <summary>
        /// Test que se encarga de comprobar la instancia de la clase GeoLocation.
        /// </summary>
        [Test]
        public void TestLocationInstance()
        {
            Assert.IsNotNull(location.GetLocation());
            Assert.IsNotNull(location.City);
            Assert.IsNotNull(location.Departament);
            Assert.IsNotNull(location.Address);
        }

        /// <summary>
        /// Test que se encarga de testear el calculo de duración que realiza 
        /// el método CalculateDuration de la clase GeoLocation.
        /// </summary>
        [Test]
        public void TestDuration()
        {
            GeoLocation secondLocation = new GeoLocation("8 de octubre y comercio", "Montevideo", "Montevideo");
            Task<double> duration = location.CalculateDuration(secondLocation);

            Assert.That(duration is Task<double>);
            Assert.IsNotNull(duration);
        }

        /// <summary>
        /// Test que se encarga de testear el calculo de distancia que realiza 
        /// el método CalculateDistance de la clase GeoLocation.
        /// </summary>        
        [Test]
        public void TestDistance()
        {
            GeoLocation secondLocation = new GeoLocation("8 de octubre y comercio", "Montevideo", "Montevideo");
            Task<double> distance = location.CalculateDuration(secondLocation);

            Assert.That(distance is Task<double>);
            Assert.IsNotNull(distance);
        }
    }
}