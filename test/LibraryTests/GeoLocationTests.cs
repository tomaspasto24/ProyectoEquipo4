using System;
using NUnit.Framework;
using Bot;
using System.Threading.Tasks;


namespace BotTests
{
    public class GeoLocationTests
    {
        GeoLocation location;
        string address;
        string city;
        string departament;   
        [SetUp]
        public void SetUp()
        {
            address = "8 de octubre";
            city = "Montevideo";
            departament = "Montevideo";

            location = new GeoLocation(address, city, departament);
        }
        [Test]
        public void TestLocationInstance()
        {
            // Assert.IsNotNull(location);
            Assert.IsNotNull(location.GetLocation());
        }


        [Test]
        public void TestDuration()
        {
            GeoLocation secondLocation = new GeoLocation("8 de octubre y comercio", "Montevideo", "Montevideo");
            Task<double> duration = location.CalculateDuration(secondLocation);

            Assert.That(duration is Task<double>);
            Assert.IsNotNull(duration);
            System.Console.WriteLine(duration + "minutos");
        }

        [Test]
        public void TestDistance()
        {
            GeoLocation secondLocation = new GeoLocation("8 de octubre y comercio", "Montevideo", "Montevideo");
            Task<double> distance = location.CalculateDuration(secondLocation);

            Assert.That(distance is Task<double>);
            Assert.IsNotNull(distance);
            System.Console.WriteLine(distance + "metros");
        }
    }
}