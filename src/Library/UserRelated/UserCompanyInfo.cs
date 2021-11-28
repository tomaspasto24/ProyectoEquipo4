using System.Text.Json.Serialization;

namespace Bot
{
    /// <summary>
    /// Clase RoleUserCompany se encarga de servir como representación de un usuario
    /// que forma parte de una empresa. 
    /// /// </summary>
    public class UserCompanyInfo
    {
        /// <summary>
        /// Representa la clase Empresa a la cual es añadido.
        /// </summary>
        /// <value></value>
        public Company company { private set; get; }

        /// <summary>
        /// Constructor UserCompanyInfo sin implementación para poder ser utilizado por la etiqueta JsonConstructor.
        /// </summary>
        [JsonConstructor]
        public UserCompanyInfo() { }

        /// <summary>
        /// Constructor que hereda, asi como toda la clase, de la clase ancestro Role.
        /// </summary>
        /// <param name="company">Empresa</param>
        public UserCompanyInfo(Company company)
        {
            this.company = company;
        }
    }
}