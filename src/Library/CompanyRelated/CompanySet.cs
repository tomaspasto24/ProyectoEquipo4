using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Bot
{
    /// <summary>
    /// Conjunto de Empresas, clase que se encarga de administrar la lista de Empresas en general. Su constructor se encuentra 
    /// privado para que no sea posible crear más de una instancia de la clase, para obtener la instancia se necesita llamar al método
    /// GetInstance que devuelve la única instancia que puede ser usada, cumpliendo así con el patrón de diseño Singleton.
    /// </summary>
    public class CompanySet : ISetOfElement<Company>
    {
        private static CompanySet instance;
        private IList<Company> listCompanies = new List<Company>();

        private CompanySet() { }

        /// <summary>
        /// Obtiene el acceso a la propia instancia de la clase CompanySet,
        /// en caso de que la variable _instance no este creada, la crea y la retorna. En caso 
        /// contrario de que anteriormente este creada simplemente la retorna, asi se asegura de que
        /// siempre se use la misma variable instancia y se cumpla con Singleton.
        /// </summary>
        /// <returns>Instancia CompanySet.</returns>
        public static CompanySet Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CompanySet();
                }
                
                return instance;
            }
        }

        /// <summary>
        /// Obtiene la lista de Empresas.
        /// </summary>
        /// <value>Lista de solo lectura de clase Empresa.</value>
        public IReadOnlyCollection<Company> ListCompanies
        {
            get
            {
                return new ReadOnlyCollection<Company>(this.listCompanies);
            }
        }

        /// <summary>
        /// Método que se encarga de agregar una Empresa a la lista de Empresas del sistema.
        /// </summary>
        /// <param name="element">Elemento Empresa.</param>
        /// <returns><c>True</c> en caso de que se pueda agregar y <c>False</c> en caso
        /// contrario.</returns>
        public bool AddElement(Company element)
        {
            if (!this.ContainsElementInListElements(element))
            {
                this.listCompanies.Add(element);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método que se encarga de eliminar una Empresa de la lista de Empresas del sistema. El criterio usado es
        /// por el nombre de la clase Empresa. Es decir, el programa no admite 2 clases Empresa con mismo nombre.
        /// </summary>
        /// <param name="element">Empresa.</param>
        /// <returns><c>True</c> en caso de que se haya eliminado correctamente y <c>False</c> en caso
        /// contrario.</returns>
        public bool DeleteElement(Company element)
        {
            if (this.ContainsElementInListElements(element))
            {
                return this.listCompanies.Remove((this.listCompanies as List<Company>).Find(companyInList => companyInList.Name == element.Name));
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

            foreach (Company company in this.listCompanies)
            {
                result.Append($"\t {company.Name} \n");
            }

            return result.ToString();
        }

        /// <summary>
        /// Método simple que se encarga de comprobar si una clase Empresa se encuentra
        /// en el sistema de Empresas.
        /// </summary>
        /// <param name="element">Empresa.</param>
        /// <returns><c>True</c> en caso de encontrarse en el sistema y <c>False</c> en caso
        /// contrario.</returns>
        public bool ContainsElementInListElements(Company element)
        {
            if (element != null)
            {
                foreach (Company item in this.listCompanies)
                {
                    if (item.Name == element.Name)
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                throw new ArgumentNullException(nameof(element));
            }
        }
        
        /// <summary>
        /// Sobrecarga de ContainsCompanyInListCompanies, se encarga de comprobar si el nombre de una clase Empresa se encuentra
        /// en la lista de Empresas.
        /// </summary>
        /// <param name="elementName">Nombre de Empresa.</param>
        /// <returns><c>True</c> en caso de encontrarse en el sistema y <c>False</c> en caso
        /// contrario.</returns>
        public bool ContainsElementInListElements(string elementName)
        {
            if (elementName != null)
            {
                foreach (Company item in this.listCompanies)
                {
                    if (item.Name == elementName)
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                throw new ArgumentNullException(nameof(elementName));
            }
        }
    }
}