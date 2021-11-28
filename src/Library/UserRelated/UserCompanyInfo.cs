using System.Text.Json.Serialization;

namespace Bot
{
    /// <summary>
    /// Clase contenedora de la información de un usuario de una compañía
    /// Patrones y principios:
    /// Esta cumple con el patron SRP, ya que la unica razon de cambio que podría tener la clase, sería cambiar la forma
    /// de guardar la información del usuario de la compañía.
    /// Cumple con Expert ya que, es experta en el manejo de la información de un usuario de una compañía.
    /// </summary>
    public class UserCompanyInfo
    {
        /// <summary>
        /// Compañía a la cuál pertenece el usuario
        /// </summary>
        public Company company { private set; get; }

        /// <summary>
        /// Constructor UserCompanyInfo sin implementación para poder ser utilizado por la etiqueta JsonConstructor.
        /// </summary>
        [JsonConstructor]
        public UserCompanyInfo() { }

        /// <summary>
        /// Crea una nueva instancia de la clase UserCompanyInfo, asignando la compañía a la cual pertenece el usuario
        /// </summary>
        /// <param name="company">Compañía en cuestión</param>
        public UserCompanyInfo(Company company)
        {
            this.company = company;
        }
    }
}