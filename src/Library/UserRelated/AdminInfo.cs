using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Clase RoleAdmin que implementa la interfaz IRole
    /// </summary>
    public class AdminInfo
    {
        /// <summary>
        /// Se inicializa la lista globalQualificationList
        /// </summary>
        /// <returns></returns>
        [JsonInclude]
        public static IList<string> globalQualificationList = new List<string>();

        /// <summary>
        /// Constructor AdminInfo sin implementación para poder ser utilizado por la etiqueta JsonConstructor.
        /// </summary>
        [JsonConstructor]
        public AdminInfo() { }
    }
}