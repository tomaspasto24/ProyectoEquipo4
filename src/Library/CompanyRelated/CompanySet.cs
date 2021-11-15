using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    /// <summary>
    /// Conjunto de Empresas, clase que se encarga de administrar la lista de Empresas en general. Su constructor se encuentra 
    /// privado para que no sea posible crear más de una instancia de la clase, para obtener la instancia se necesita llamar al método
    /// GetInstance que devuelve la única instancia que puede ser usada, cumpliendo así con el patrón de diseño Singleton.
    /// </summary>
    public class CompanySet : ISet<Company>
    {
        private static CompanySet _instance;
        private CompanySet() { }

        /// <summary>
        /// Método estático que controla el acceso a la propia instancia de la clase CompanySet,
        /// en caso de que la variable _instance no este creada, la crea y la retorna. En caso 
        /// contrario de que anteriormente este creada simplemente la retorna, asi se asegura de que
        /// siempre se use la misma variable instancia y se cumpla con Singleton.
        /// </summary>
        /// <returns>Instancia CompanySet.</returns>
        public static CompanySet GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CompanySet();
            }
            return _instance;
        }

        private List<Company> listCompanies = new List<Company>();
    
        /// <summary>
        /// Obtiene la lista de Empresas.
        /// </summary>
        /// <value>Lista de solo lectura de clase Empresa.</value>
        public IReadOnlyCollection<Company> ListCompanies
        {
            get
            {
                return this.listCompanies.AsReadOnly();
            }
        }

        /// <summary>
        /// Método que se encarga de agregar una Empresa a la lista de Empresas del sistema.
        /// </summary>
        /// <param name="name">Nombre de Empresa.</param>
        /// <param name="item">Rubro de Empresa.</param>
        /// <param name="location">Ubicación de Empresa.</param>
        /// <param name="contact">Contacto de Empresa.</param>
        /// <returns><c>True</c> en caso de que se pueda agregar y <c>False</c> en caso
        /// contrario.</returns>
        public bool AddElement(string name, string item, GeoLocation location, string contact)
        {
            if (!ContainsElementInListElements(name))
            {
                Company company = new Company(name, item, location, contact);
                this.listCompanies.Add(company);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método que se encarga de agregar una Empresa a la lista de Empresas del sistema.
        /// </summary>
        /// <returns><c>True</c> en caso de que se pueda agregar y <c>False</c> en caso
        /// contrario.</returns>
        public bool AddElement(Company company)
        {
            if (!ContainsElementInListElements(company))
            {
                listCompanies.Add(company);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método que se encarga de eliminar una Empresa de la lista de Empresas del sistema.
        /// </summary>
        /// <param name="company">Empresa.</param>
        /// <returns><c>True</c> en caso de que se haya eliminado correctamente y <c>False</c> en caso
        /// contrario.</returns>
        public bool DeleteElement(Company company)
        {
            if(ContainsElementInListElements(company))
            {
                listCompanies.Remove(company);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método que retorna la lista completa de Empresas en un string con sus respectivos
        /// índices.
        /// </summary>
        /// <returns>String con el nombre de la Empresa y sus indices.</returns>
        public string ReturnListElements()
        {
            StringBuilder result = new StringBuilder("Empresas: \n");

            foreach (Company company in listCompanies)
            {
                result.Append($"{company.Name} \n");
            }

            return result.ToString();
        }

        /// <summary>
        /// Método simple que se encarga de comprobar si una clase Empresa se encuentra
        /// en el sistema de Empresas.
        /// </summary>
        /// <param name="company">Empresa.</param>
        /// <returns><c>True</c> en caso de encontrarse en el sistema y <c>False</c> en caso
        /// contrario.</returns>
        public bool ContainsElementInListElements(Company company)
        {
            if (company != null)
            {
                foreach (Company item in listCompanies)
                {
                    if (item.Name == company.Name)
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                throw new ArgumentNullException(nameof(company));
            }
        }
        
        /// <summary>
        /// Sobrecarga de ContainsCompanyInListCompanies, se encarga de comprobar si el nombre de una clase Empresa se encuentra
        /// en la lista de Empresas.
        /// </summary>
        /// <param name="companyName">Nombre de Empresa.</param>
        /// <returns><c>True</c> en caso de encontrarse en el sistema y <c>False</c> en caso
        /// contrario.</returns>
        public bool ContainsElementInListElements(string companyName)
        {
            if (companyName != null)
            {
                foreach (Company item in listCompanies)
                {
                    if (item.Name == companyName)
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                throw new ArgumentNullException(nameof(companyName));
            }
        }
    }
}