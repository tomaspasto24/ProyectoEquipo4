using LocationApi;
using System;
using System.Threading.Tasks;

namespace Bot
{
    /// <summary>
    /// Clase que depende de la API Location de la UCU.
    /// </summary>
    public class GeoLocation
    {
        private LocationApiClient client = new LocationApiClient();
        private string city;
        private string departament;
        private string address;
        private Location location;

        /// <summary>
        /// Ciudad ingresada como parámetro no obligatorio para crear instancia Location.
        /// </summary>
        /// <value>String que representa ciudad.</value>
        public string City
        {
            get
            {
                return this.city;
            }
        }
        /// <summary>
        /// Departamento ingresado como parámetro no obligatorio para instancia Location.
        /// </summary>
        /// <value>String que representa el departamento.</value>  
        public string Departament
        {
            get
            {
                return this.departament;
            }
        }  
        /// <summary>
        /// Dirección (calle, número de puerta, etc. o ruta, kilómetro, etc) ingresado como parámetro obligatorio para instancia Location.
        /// </summary>
        /// <value></value>
        public string Address
        {
            get
            {
                return this.address;
            }
        }  

        /// <summary>
        /// Constructor de la clase Geolocation, llama a un método privado asincrono y después se valida la propiedad Found.
        /// <c>True</c> se asignan los parámetros ingresados a los atributos city, departament y address respectivamente.
        /// </summary>
        /// <param name="address">Dirección.</param>
        /// <param name="city">Ciudad.</param>
        /// <param name="departament">Departamento.</param>
        public GeoLocation(string address, string city, string departament)
        {
            EstablishLocation(address, city, departament);
            this.city = city;
            this.departament = departament;
            this.address = address;
        }

        private async void EstablishLocation(string address, string city, string departament)
        {
            this.location = await client.GetLocation(address);
        }

        /// <summary>
        /// Calcula y retorna la distancia en kilometros entre la propia instancia de clase y la ingresada como parámetro.
        /// </summary>
        /// <param name="secondLocation">Segunda clase a calcular distancia.</param>
        /// <returns>Distancia de tipo Task double.</returns>
        public async Task<double> CalculateDistance(GeoLocation secondLocation)
        {
            Distance distance = await client.GetDistance(this.location, secondLocation.location);
            return distance.TravelDistance;
        }

        /// <summary>
        /// Calcula y retorna la duración entre la propia instancia de clase y la ingresada como parámetro.
        /// </summary>
        /// <param name="secondLocation">Segunda clase a calcular duración.</param>
        /// <returns>Duración de tipo Task double.</returns>
        public async Task<double> CalculateDuration(GeoLocation secondLocation)
        {
            Distance distance = await client.GetDistance(this.location, secondLocation.location);
            return distance.TravelDuration;
        }

        /// <summary>
        /// Retorna el propio objeto Location y descarga el mapa con la ubicación correspondiente.
        /// </summary>
        /// <returns>Objeto mismo.</returns>
        public async Task<Location> GetLocation()
        {
            await client.DownloadMap(this.location.Latitude, this.location.Longitude, @"Location");
            return this.location;
        }
    }
}