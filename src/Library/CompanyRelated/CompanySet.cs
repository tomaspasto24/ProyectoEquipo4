using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;

namespace Bot
{
    /// <summary>
    /// Conjunto de Empresas, clase que se encarga de administrar la lista de Empresas en general.
    /// Cumple con el patrón de creación Singleton (Ver Readme).
    /// </summary>
    public class CompanySet : ISetOfElement<Company>, IJsonConvertible
    {
        private static CompanySet instance;
        private IList<Company> listCompanies;

        private CompanySet()
        {
            this.Initialize();
        }

        /// <summary>
        /// Obtiene el acceso a la propia instancia de la clase CompanySet,
        /// en caso de que el atributo instance no este creado, lo crea y lo retorna. En caso 
        /// contrario de que anteriormente este creado simplemente lo retorna, asi se asegura de que
        /// siempre se use la misma instancia y se cumpla con Singleton.
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
        /// Método que retorna la lista completa de Empresas en una cadena de caracteres.
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
        /// <param name="element">Clase Empresa.</param>
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

        /// <summary>
        /// Método que es llamado por el constructor privado para inicializar la lista de clases Empresa.
        /// </summary>
        public void Initialize()
        {
            this.listCompanies = new List<Company>();
        }

        /// <summary>
        /// Método que convierte el propio objeto en formato JSON.
        /// </summary>
        /// <returns>Lista convertida en JSON mediante una cadena de caracteres.</returns>
        public string ConvertObjectToSave()
        {
            JsonSerializerOptions options = new () 
            {
                ReferenceHandler = MyReferenceHandler.Instance,
                WriteIndented = true,
            };
            return JsonSerializer.Serialize(this.listCompanies, options);
        }
    }
}