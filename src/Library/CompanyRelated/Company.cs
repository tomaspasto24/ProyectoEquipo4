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
        //Hacer metodo para publicar Publicación. y guardarlo en lista 
        private static List<Company> registeredCompanies = new List<Company>(); 
        private static int counterCompanies = 0; // contadorEmpresas
        private string name;
        private string item; //rubro (español)
        private GeoLocation location;
        private string contact;
        private List<User> setUsers = new List<User>(); //conjunto usuarios

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
            setUsers.Add(user);
        }

        /// <summary>
        /// Método que se encarga de eliminar un uusario del conjunto usuarios de la clase Empresa.
        /// </summary>
        /// <param name="user">Clase Usuario.</param>
        /// <returns></returns>
        public bool DeleteUser(User user)
        {
            return setUsers.Remove(user);
        }
    }
}