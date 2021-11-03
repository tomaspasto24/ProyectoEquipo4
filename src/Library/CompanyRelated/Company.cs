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
        private static int counterCompanies = 0; // contadorEmpresas
        private string name;
        private string item; //rubro 
        private GeoLocation location;
        private string contact;
        private List<User> setUsers = new List<User>(); //conjunto usuarios
        private List<Publication> listOwnPublications = new List<Publication>(); //conjunto publicaciones propias de la empresa
        private List<Publication> listHistorialPublications = new List<Publication>(); //historial de publicaciones

        /// <summary>
        /// Contador estático que representa el número de Empresas creadas.
        /// </summary>
        /// <value>Entero.</value>
        public static int CounterCompanies
        {
            get
            {
                return counterCompanies;
            }
        }

        /// <summary>
        /// Atributo nombre de la clase Empresa.
        /// </summary>
        /// <value>String</value>
        public String Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Constructor de la clase Empresa, setea los valores de los parámetros y suma un valor al
        /// contador de empresas estático.
        /// </summary>
        /// <param name="nombre">Nombre de la Empresa.</param>
        /// <param name="rubro">Rubro de la Empresa.</param>
        /// <param name="location">Ubicación establecida de la Empresa.</param>
        /// <param name="contacto">Contacto (Teléfono) de la Empresa.</param>
        public Company(string nombre, string rubro, GeoLocation location, string contacto)
        {
            this.name = nombre;
            this.item = rubro;
            this.location = location;
            this.contact = contacto;
            this.RegisterCompany();
            counterCompanies++;
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
        /// <returns></returns>
        public bool DeleteUser(User user)
        {
            return this.setUsers.Remove(user);
        }

        /// <summary>
        /// Método que se encarga de agregar una publicación propia de la empresa.
        /// </summary>
        /// <param name="publication">Publication</param>
        public void AddOwnPublication(Publication publication)
        {
            this.listOwnPublications.Add(publication);
        }

        /// <summary>
        /// Método que se encarga de retornar la lista de publicaciones propia de la clase Empresa.
        /// </summary>
        /// <returns>List Publication </returns>
        public List<Publication> GetListOwnPublications()
        {
            return this.listOwnPublications;
        }

        /// <summary>
        /// Método que se encarga de añadir una clase Publicación a ListHistorialPublications.
        /// </summary>
        /// <param name="publication">Publicación a añadir.</param>
        public void AddListHistorialPublications(Publication publication)
        {
            this.listHistorialPublications.Add(publication);
        }

        /// <summary>
        /// Método que devuelve el objeto ListHistorialPublications de la clase Empresa.
        /// </summary>
        /// <returns>List Publication</returns>
        public List<Publication> GetListHistorialPublications()
        {
            return this.listHistorialPublications;
        }
    }
}