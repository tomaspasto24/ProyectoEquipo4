using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Bot
{
    /// <summary>
    /// Clase contenedora de la información manejada por el Admin
    /// Patrones y principios:
    /// Esta cumple con el patron SRP, ya que la unica razon de cambio que podría tener la clase, sería cambiar la forma
    /// de almacenar la lista de certificaciones globales.
    /// Cumple con Expert ya que, es experta en el manejo de la responsabilidad otorgada que es, manejar la lista de certificaiones globales.
    /// </summary>
    public class AdminInfo
    {
        //TODO Arreglar el tema de qualifications -> certifications :)
        /// <summary>
        /// Lista de certifaciones para los usuarios y las publicaciones.
        /// </summary>
        [JsonInclude]
        public static IList<string> globalQualificationList = new List<string>();

        /// <summary>
        /// Constructor de la clase AdminInfo sin implementación y de acceso publico para poder ser usado por 
        /// la etiqueta JsonConstructor.
        /// </summary>
        [JsonConstructor]
        public AdminInfo() { }
    }
}