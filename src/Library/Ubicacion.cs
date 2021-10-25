using LocationApi;
using System;

namespace Bot
{
    public class Ubicacion
    {
        private string city;
        private string departament;
        private string address;
        private Location location;  

        public Ubicacion(string address, string city, string departament)
        {
            EstablishLocation(this.address, this.city, this.departament);
            if(location.Found)
            {
                this.city = city;
                this.departament = departament;
                this.address = address;
            }
            else Console.WriteLine("Error con Ubicacion");
        }

        private async void EstablishLocation(string address, string city, string departament)
        {
            LocationApiClient client = new LocationApiClient();
            location = await client.GetLocation(address);
        }

    }
}