using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Clase RoleAdmin que implementa la interfaz IRole
    /// </summary>
    public class AdminInfo
    {
        // TODO Resolver donde guardar estas qualifications

        /// <summary>
        /// Se inicializa la lista globalQualificationList
        /// </summary>
        /// <returns></returns>
        [JsonInclude]
        public static IList<string> globalQualificationList = new List<string>();

        [JsonConstructor]
        public AdminInfo() { }
    }
}