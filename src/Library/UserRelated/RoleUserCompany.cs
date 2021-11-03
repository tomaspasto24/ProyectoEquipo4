using System;

namespace Bot
{
    /// <summary>
    /// Clase RoleUserCompany que hereda de la clase Role.
    /// </summary>
    public class RoleUserCompany : Role
    {
        /// <summary>
        /// Se instancia company con get y set.
        /// </summary>
        /// <value></value>
        public Company company { private set; get; }

        /// <summary>
        /// Constructor de la clase RoleUserCompany.
        /// </summary>
        /// <param name="company"></param>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleUserCompany(Company company, string name, int id) : base(name, id)
        {
            this.company = company;
        }
    }
}