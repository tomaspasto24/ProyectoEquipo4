using System;
using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Clase RoleUserCompany se encarga de servir como representación de un usuario
    /// que forma parte de una empresa. 
    /// /// </summary>
    public class RoleUserCompany : IRole
    {
        /// <summary>
        /// Representa la clase Empresa a la cual es añadido.
        /// </summary>
        /// <value></value>
        public Company company { private set; get; }

        private List<Permission> permissions = new List<Permission>(){
            Permission.None,
            Permission.SalesReport,
            Permission.Publish,
            Permission.AddMaterial,
        };

        /// <summary>
        /// Constructor que hereda, asi como toda la clase, de la clase ancestro Role.
        /// </summary>
        /// <param name="company">Empresa</param>
        public RoleUserCompany(Company company)
        {
            this.company = company;
        }

        public bool HasPermission(Permission perm)
        {
            return this.permissions.Contains(perm);
        }

        public override string ToString()
        {
            return "Usuario de compañia";
        }
    }
}