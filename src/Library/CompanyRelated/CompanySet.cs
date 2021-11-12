using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    /// <summary>
    /// Conjunto de Empresas, clase estática que administra la lista de Empresas en general.
    /// </summary>
    public static class CompanySet
    {

        private static List<Company> listCompany = new List<Company>();

        /// <summary>
        /// Obtiene la lista de Empresas, esto para que la clase Búsqueda pueda 
        /// manipular eficientemente las Publicaciones.
        /// </summary>
        /// <value></value>
        public static IReadOnlyCollection<Company> ListCompany
        {
            get
            {
                return listCompany.AsReadOnly();
            }
        }

        /// <summary>
        /// Método que se encarga de agregar una Empresa a la lista de Empresas del sistema.
        /// </summary>
        /// <param name="name">Nombre de Empresa.</param>
        /// <param name="item">Rubro de Empresa.</param>
        /// <param name="location">Ubicación de Empresa.</param>
        /// <param name="contact">Contacto de Empresa.</param>
        /// <returns></returns>
        public static Company AddCompany(string name, string item, GeoLocation location, string contact)
        {
            Company company = new Company(name, item, location, contact);
            return company;
        }

        /// <summary>
        /// Método que se encarga de eliminar una Empresa de la lista de Empresas del sistema.
        /// </summary>
        /// <param name="company">Empresa.</param>
        /// <returns><c>True</c> en caso de que se haya eliminado correctamente y <c>False</c> en caso 
        /// contrario.</returns>
        public static bool DeleteCompany(Company company)
        {
            return listCompany.Remove(company);
        }

        /// <summary>
        /// Método que retorna la lista completa de Empresas en un string con sus respectivos
        /// índices.
        /// </summary>
        /// <returns>String con el nombre de la Empresa y sus indices.</returns>
        public static string ReturnListCompanies()
        {
            StringBuilder resultado = new StringBuilder("Empresas: \n");
            int contador = 1;

            foreach (Company company in listCompany)
            {
                resultado.Append($"{++contador}- {company.Name} \n");
            }
            return resultado.ToString();
        }
    }
}