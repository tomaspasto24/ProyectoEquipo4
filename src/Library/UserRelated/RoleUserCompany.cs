using System;

namespace Bot
{
    /// <summary>
    /// Clase RoleUserCompany se encarga de servir como representación de un usuario
    /// que forma parte de una empresa. 
    /// </summary>
    public class RoleUserCompany : Role
    {
        /// <summary>
        /// Representa la clase Empresa a la cual es añadido.
        /// </summary>
        /// <value></value>
        public Company company { private set; get; }

        /// <summary>
        /// Constructor que hereda, asi como toda la clase, de la clase ancestro Role.
        /// </summary>
        /// <param name="company">Empresa</param>
        /// <param name="name">Nombre</param>
        /// <param name="id">Id</param>
        public RoleUserCompany(Company company)
        {
            this.company = company;
        }
    }
}