using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json.Serialization;

namespace Bot
{
    /// <summary>
    /// Clase que se encarga de representar una Empresa. Cumple con el patrón de diseño Creator porque la clase
    /// Empresa tiene la responsabilidad de crear instancias de la clase Usuario y Publicación ya que tiene
    /// un contenedor capaz de almacenar instancias de ambas y las usa de forma muy cercana.
    /// </summary>
    public class Company
    {
        private string name;
        private string item;
        /// <summary>
        /// Atributo location el cual es público por motivos de incompatibilidad con la implementación de la Serialización.
        /// </summary>
        /// <value>GeoLocation.</value>
        public GeoLocation location {get; set;}
        private string contact;
        private List<UserInfo> listUsers = new List<UserInfo>();
        private List<Publication> listOwnPublications = new List<Publication>();
        private List<Publication> listHistorialPublications = new List<Publication>();

        /// <summary>
        /// Constructor ingresado en blanco para la implementación de la Serialización.
        /// </summary>
        [JsonConstructor]
        public Company() { }

        /// <summary>
        /// Constructor de la clase Empresa, inicializa los valores de los parámetros.
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
            set
            {
                this.name = value;
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
            set
            {
                this.item = value;
            }
        }

        /// <summary>
        /// Obtiene el contacto de la clase Empresa.
        /// </summary>
        /// <value>Cadena de caracteres.</value>
        public String Contact
        {
            get
            {
                return this.contact;
            }
            set
            {
                this.contact = value;
            }
        }

        /// <summary>
        /// Obtiene el historial de publicaciones como una lista de solo lectura para que no se
        /// pueda agregar o quitar objetos Publication de la instancia obtenida.
        /// </summary>
        /// <returns>Lista Publications de solo lectura.</returns>
        [JsonInclude]
        public List<Publication> ListHistorialPublications
        {
            get
            {
                return this.listHistorialPublications;
            }
            set
            {
                if(!(value.Count == 0))
                {
                    this.ListHistorialPublications.Clear();
                    this.ListHistorialPublications.AddRange(value);
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de las publicaciones actuales de la empresa como una lista de solo lectura.
        /// </summary>
        /// <returns>Lista Publitacions de solo lectura.</returns>
        [JsonInclude]
        public List<Publication> ListOwnPublications
        {
            get
            {
                return this.listOwnPublications;
            }
            set
            {
                if(!(value.Count == 0))
                {
                    this.listOwnPublications.Clear();
                    this.listOwnPublications.AddRange(value);
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de los usuarios actuales de la Empresa como una lista de solo lectura.
        /// </summary>
        /// <returns>Lista User de solo lectura.</returns>
        [JsonInclude]
        public List<UserInfo> ListUsers
        {
            get
            {
                return this.listUsers;
            }
            set
            {
                if(!(value.Count == 0))
                {
                    this.listUsers.Clear();
                    this.listUsers.AddRange(value);
                }
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
            (this.listUsers as List<UserInfo>).Add(user);
        }

        /// <summary>
        /// Sobrecarga del método AddUser, se encarga de agregar una lista de Usuarios al conjunto de usuarios de la clase Empresa.
        /// </summary>
        /// <param name="listUsers"></param>
        public void AddUser(IReadOnlyList<UserInfo> listUsers)
        {
            (this.listUsers as List<UserInfo>).AddRange(listUsers);
        }

        /// <summary>
        /// Método que se encarga de eliminar un usario del conjunto usuarios de la clase Empresa.
        /// </summary>
        /// <param name="user">Clase Usuario.</param>
        /// <returns>Retorna <c>True</c> en caso de que pueda eliminarse, <c>False</c> en caso contrario.</returns>
        public bool DeleteUser(UserInfo user)
        {
            return (this.listUsers as List<UserInfo>).Remove(user);
        }

        /// <summary>
        /// Método que se encarga de agregar una publicación propia de la empresa.
        /// </summary>
        /// <param name="publication">Clase Publicación.</param>
        public void AddOwnPublication(Publication publication)
        {
            (this.listOwnPublications as List<Publication>).Add(publication);
        }

        /// <summary>
        /// Sobrecarga del método AddOwnPublication, se encarga de agregar una lista de Publicaciones a listOwnPublications.
        /// </summary>
        /// <param name="listPublications">Lista de Publicaciones.</param>
        public void AddOwnPublication(IReadOnlyList<Publication> listPublications)
        {
            (this.listOwnPublications as List<Publication>).AddRange(listPublications);
        }

        /// <summary>
        /// Método que se encarga de eliminar una publicación propia de la empresa.
        /// </summary>
        /// <param name="publication">Publicación.</param>
        /// <returns>Retorna <c>True</c> en caso de que pueda eliminarse, <c>False</c> en caso contrario.</returns>
        public bool DeleteOwnPublication(Publication publication)
        {
            return (this.listOwnPublications as List<Publication>).Remove(publication);
        }

        /// <summary>
        /// Método que se encarga de añadir una clase Publicación a ListHistorialPublications.
        /// </summary>
        /// <param name="publication">Publicación a añadir.</param>
        public void AddListHistorialPublication(Publication publication)
        {
            (this.listHistorialPublications as List<Publication>).Add(publication);
        }

        /// <summary>
        /// Sobrecarga del método AddListHistorialPublication, se encarga de agregar una lista de Publicaciones a listHistorialPublications.
        /// </summary>
        /// <param name="listPublications"></param>
        public void AddListHistorialPublication(IReadOnlyList<Publication> listPublications)
        {
            ((List<Publication>)this.listHistorialPublications).AddRange(listPublications);
        }
    }
}