using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    /// <summary>
    /// Clase que se encarga de representar una Empresa.
    /// </summary>
    public class Company
    {
        private static List<Company> registeredCompanies = new List<Company>();
        private string name;
        private string item;  
        private GeoLocation location;
        private string contact;
        private List<User> setUsers = new List<User>(); // Conjunto usuarios
        private List<Publication> listOwnPublications = new List<Publication>(); // Conjunto publicaciones propias de la empresa
        private List<Publication> listHistorialPublications = new List<Publication>(); // Historial de publicaciones

        /// <summary>
        /// Obtiene nombre de la clase Empresa.
        /// </summary>
        /// <value>Cadena de caracteres.</value>
        public String Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Obtiene objeto ListHistorialPublications de la clase Empresa.
        /// </summary>
        /// <returns>Lista Publicación.</returns>
        public IReadOnlyList<Publication> ListHistorialPublications
        {
            get
            {
                return this.listHistorialPublications.AsReadOnly();
            }
        }

        /// <summary>
        /// Constructor de la clase Empresa, setea los valores de los parámetros y suma un valor al
        /// contador de empresas estático.
        /// </summary>
        /// <param name="name">Nombre de la Empresa.</param>
        /// <param name="item">Rubro de la Empresa.</param>
        /// <param name="location">Ubicación establecida de la Empresa.</param>
        /// <param name="contact">Contacto (Teléfono) de la Empresa.</param>
        public Company(string name, string item, GeoLocation location, string contact)
        {
            this.name = name;
            this.item = item;
            this.location = location;
            this.contact = contact;
            this.RegisterCompany();
        }

        /// <summary>
        /// Devuelve los datos básicos de la empresa (nombre, rubro y contacto).
        /// </summary>
        /// <returns>String conteniendo los datos de la Empresa.</returns>
        public string ReturnContact()
        {
            StringBuilder resultado = new StringBuilder("Contacto: \n");

            resultado.Append($"Empresa: {this.name} \n");
            resultado.Append($"Rubro: {this.item} \n");
            resultado.Append($"Contacto: {this.contact} \n");

            return resultado.ToString();
        }

        /// <summary>
        /// Método que registra (agrega) una Empresa a la lista estática de Empresas.
        /// </summary>
        public void RegisterCompany()
        {
            registeredCompanies.Add(this);
        }

        /// <summary>
        /// Método que elimina la Empresa de la lista estática de Empresas.
        /// </summary>
        public void DeleteCompany()
        {
            registeredCompanies.Remove(this);
        }

        /// <summary>
        /// Método que se encarga de agregar usuario al conjunto usuarios de la clase Empresa.
        /// </summary>
        /// <param name="user">Clase Usuario.</param>
        public void AddUser(User user)
        {
            this.setUsers.Add(user);
        }

        /// <summary>
        /// Método que se encarga de eliminar un usario del conjunto usuarios de la clase Empresa.
        /// </summary>
        /// <param name="user">Clase Usuario.</param>
        /// <returns>Retorna <c>True</c> en caso de que pueda eliminarse, <c>False</c> en caso contrario.</returns>
        public bool DeleteUser(User user)
        {
            return this.setUsers.Remove(user);
        }

        /// <summary>
        /// Método que se encarga de agregar una publicación propia de la empresa.
        /// </summary>
        /// <param name="publication">Publicación.</param>
        public void AddOwnPublication(Publication publication)
        {
            this.listOwnPublications.Add(publication);
        }

        /// <summary>
        /// Método que se encarga de retornar la lista de publicaciones propia de la clase Empresa.
        /// </summary>
        /// <returns>Lista Publicación.</returns>
        public IReadOnlyList<Publication> ListOwnPublications
        {
            get
            {
                return this.listOwnPublications.AsReadOnly();
            }
        }

        /// <summary>
        /// Método que se encarga de añadir una clase Publicación a ListHistorialPublications.
        /// </summary>
        /// <param name="publication">Publicación a añadir.</param>
        public void AddListHistorialPublications(Publication publication)
        {
            this.listHistorialPublications.Add(publication);
        }
    }
}