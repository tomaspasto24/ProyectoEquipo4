using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Bot
{
    /// <summary>
    /// Clase que se encarga de representar una Empresa.
    /// </summary>
    public class Company
    {
        private string name;
        private string item;
        private GeoLocation location;
        private string contact;
        private List<UserInfo> listUsers = new List<UserInfo>();
        private List<Publication> listOwnPublications = new List<Publication>();
        private List<Publication> listHistorialPublications = new List<Publication>();

        /// <summary>
        /// Constructor de la clase Empresa, setea los valores de los parámetros y suma un valor al
        /// contador de empresas estático.
        /// </summary>
        /// <param name="name">Nombre de la Empresa.</param>
        /// <param name="item">Rubro de la Empresa.</param>
        /// <param name="location">Ubicación establecida de la Empresa.</param>
        /// <param name="contact">Contacto (Teléfono) de la Empresa.</param>
        [JsonConstructor]
        public Company(string name, string item, GeoLocation location, string contact)
        {
            this.name = name;
            this.item = item;
            this.location = location;
            this.contact = contact;
        }

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
        /// Obtiene nombre del rubro de la clase Empresa.
        /// </summary>
        /// <value>Cadena de caracteres.</value>
        public String Item
        {
            get
            {
                return this.item;
            }
        }

        /// <summary>
        /// Obtiene la ubicación de la clase Empresa.
        /// </summary>
        /// <value>Cadena de caracteres.</value>
        public GeoLocation Location
        {
            get
            {
                return this.location;
            }
        }

        /// <summary>
        /// Obtiene nombre del rubro de la clase Empresa.
        /// </summary>
        /// <value>Cadena de caracteres.</value>
        public String Contact
        {
            get
            {
                return this.contact;
            }
        }

        /// <summary>
        /// Obtiene el historial de publicaciones como una lista de solo lectura para que no se
        /// pueda agregar o quitar objetos Publication de la instancia obtenida.
        /// </summary>
        /// <returns>Lista Publications de solo lectura.</returns>
        public IReadOnlyList<Publication> ListHistorialPublications
        {
            get
            {
                return this.listHistorialPublications.AsReadOnly();
            }
        }

        /// <summary>
        /// Obtiene una lista de las publicaciones actuales de la empresa como una lista de solo lectura.
        /// </summary>
        /// <returns>Lista Publitacions de solo lectura.</returns>
        public IReadOnlyList<Publication> ListOwnPublications
        {
            get
            {
                return this.listOwnPublications.AsReadOnly();
            }
        }

        /// <summary>
        /// Obtiene una lista de los usuarios actuales de la empresa como una lista de solo lectura.
        /// </summary>
        /// <returns>Lista User de solo lectura.</returns>
        public IReadOnlyList<UserInfo> ListUsers
        {
            get
            {
                return this.listUsers.AsReadOnly();
            }
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
        /// Método que se encarga de agregar usuario al conjunto usuarios de la clase Empresa.
        /// </summary>
        /// <param name="user">Clase Usuario.</param>
        public void AddUser(UserInfo user)
        {
            this.listUsers.Add(user);
        }

        /// <summary>
        /// Sobrecarga del método AddUser, se encarga de agregar una lista de Usurios a listUsers.
        /// </summary>
        /// <param name="listUsers"></param>
        public void AddUser(IReadOnlyList<UserInfo> listUsers)
        {
            this.listUsers.AddRange(listUsers);
        }

        /// <summary>
        /// Método que se encarga de eliminar un usario del conjunto usuarios de la clase Empresa.
        /// </summary>
        /// <param name="user">Clase Usuario.</param>
        /// <returns>Retorna <c>True</c> en caso de que pueda eliminarse, <c>False</c> en caso contrario.</returns>
        public bool DeleteUser(UserInfo user)
        {
            return this.listUsers.Remove(user);
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
        /// Sobrecarga del método AddOwnPublication, se encarga de agregar una lista de Publicaciones a listOwnPublications.
        /// </summary>
        /// <param name="listPublications">Lista de Publicaciones.</param>
        public void AddOwnPublication(IReadOnlyList<Publication> listPublications)
        {
            this.listOwnPublications.AddRange(listPublications);
        }

        /// <summary>
        /// Método que se encarga de eliminar una publicación propia de la empresa.
        /// </summary>
        /// <param name="publication">Publicación.</param>
        /// <returns>Retorna <c>True</c> en caso de que pueda eliminarse, <c>False</c> en caso contrario.</returns>
        public bool DeleteOwnPublication(Publication publication)
        {
            return this.listOwnPublications.Remove(publication);
        }

        /// <summary>
        /// Método que se encarga de añadir una clase Publicación a ListHistorialPublications.
        /// </summary>
        /// <param name="publication">Publicación a añadir.</param>
        public void AddListHistorialPublication(Publication publication)
        {
            this.listHistorialPublications.Add(publication);
        }

        /// <summary>
        /// Sobrecarga del método AddListHistorialPublication, se encarga de agregar una lista de Publicaciones a listHistorialPublications.
        /// </summary>
        /// <param name="listPublications"></param>
        public void AddListHistorialPublication(IReadOnlyList<Publication> listPublications)
        {
            this.listHistorialPublications.AddRange(listPublications);
        }
    }
}