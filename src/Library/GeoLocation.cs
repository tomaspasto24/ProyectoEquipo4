using System;
using Ucu.Poo.Locations.Client;

namespace Bot
{
    /// <summary>
    /// Clase que depende de la API Location de la UCU.
    /// </summary>
    public class GeoLocation
    {
        private LocationApiClient client = new LocationApiClient();
        private string city;
        private string address;
        private Location location;

        /// <summary>
        /// Constructor de la clase Geolocation, llama a un método privado asincrono y después se valida la propiedad Found.
        /// <c>True</c> se asignan los parámetros ingresados a los atributos city, departament y address respectivamente.
        /// </summary>
        /// <param name="address">Dirección.</param>
        /// <param name="city">Ciudad.</param>
        public GeoLocation(string address, string city)
        {
            this.location = this.client.GetLocation(address, city);
            if (this.location.Found)
            {
                this.city = city;
                this.address = address;
            }
            else
            {
                throw new ArgumentException("No se ha podido encontrar la ubicación");
            }
        }

        /// <summary>
        /// Obtiene ciudad ingresada como parámetro no obligatorio para crear instancia Location.
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
        /// Obtiene dirección (calle, número de puerta, etc. o ruta, kilómetro, etc) ingresado como parámetro obligatorio para instancia Location.
        /// </summary>
        /// <value>Cadena de caracteres.</value>
        public string Address
        {
            get
            {
                return this.address;
            }
        }

        /// <summary>
        /// Calcula y retorna la distancia en kilometros entre la propia instancia de clase y la ingresada como parámetro.
        /// </summary>
        /// <param name="secondLocation">Segunda clase a calcular distancia.</param>
        /// <returns>Distancia de tipo double.</returns>
        public double CalculateDistance(GeoLocation secondLocation)
        {
            if (secondLocation != null)
            {
                Distance distance = this.client.GetDistance(this.location, secondLocation.location);
                return distance.TravelDistance;
            }
            else
            {
                throw new ArgumentNullException(nameof(secondLocation));
            }
        }

        /// <summary>
        /// Calcula y retorna la duración entre la propia instancia de clase y la ingresada como parámetro.
        /// </summary>
        /// <param name="secondLocation">Segunda clase a calcular duración.</param>
        /// <returns>Duración de tipo double.</returns>
        public double CalculateDuration(GeoLocation secondLocation)
        {
            if (secondLocation != null)
            {
                Distance distance = this.client.GetDistance(this.location, secondLocation.location);
                return distance.TravelDuration;
            }
            else
            {
                throw new ArgumentNullException(nameof(secondLocation));
            }
        }

        /// <summary>
        /// Retorna el propio objeto Location y descarga el mapa con la ubicación correspondiente.
        /// </summary>
        /// <returns>Objeto mismo.</returns>
        public Location GetLocation()
        {
            this.client.DownloadMap(this.location.Latitude, this.location.Longitude, @"Location");
            return this.location;
        }

        /// <summary>
        /// Método que revisa si se puede encontrar la ubicación construida.
        /// </summary>
        /// <returns><c>True</c> en caso de que la ubicación sea válida, <c>False</c> en caso de no ser válida.</returns>
        public bool IsValid()
        {
            return this.location.Found;
        }
    }
}