using System;
using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Clase RoleUserCompany se encarga de servir como representación de un usuario
    /// que forma parte de una empresa. 
    /// /// </summary>
    public class UserCompanyInfo
    {
        // TODO Resolver la relacion userCompany - Company

        /// <summary>
        /// Representa la clase Empresa a la cual es añadido.
        /// </summary>
        /// <value></value>
        public Company company { private set; get; }

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