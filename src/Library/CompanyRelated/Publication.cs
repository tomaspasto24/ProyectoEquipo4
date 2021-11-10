using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    /// <summary>
    /// Cada objeto de la clase Publicación, administrado por un objeto Empresa, es el conjunto de items
    /// que la aplicación muestra a los emprendedores.
    /// </summary>
    public class Publication
    {
        private string title;
        private DateTime date;
        private DateTime closedDate;
        private GeoLocation location;
        private Company company;
        private bool isClosed;
        private List<Material> listMaterials = new List<Material>();
        private List<string> listQualifications = new List<string>(); // Lista Habilitaciones

        /// <summary>
        /// Obtiene una instancia de RoleEntrepreneur que referencia al emprendedor interesado.
        /// </summary>
        /// <value>Rol Emprendedor.</value>
        public RoleEntrepreneur InterestedPerson { get; private set; } // Hay que ver como guardar la persona interesada


        /// <summary>
        /// Constructor de Publicación, instancia la hora del sistema actual en donde se crea y setea nombreEmpresa, ubicacion, material y titulo de la publicacion.
        /// </summary>
        /// <param name="title">Titulo.</param>
        /// <param name="company">Empresa.</param>
        /// <param name="location">Ubicación.</param>
        /// <param name="material">Material</param>
        public Publication(String title, Company company, GeoLocation location, Material material)
        {
            this.title = title;
            this.company = company;
            this.date = DateTime.Now;
            this.closedDate = DateTime.MinValue;
            this.location = location;
            this.AddMaterial(material);
            this.isClosed = false;
            this.InterestedPerson = null;
        }

        /// <summary>
        /// Obtiene titulo que representa la publicación. Más que nada para poder retornar una lista
        /// identificando por título.
        /// </summary>
        /// <value>Cadena de caracteres.</value>
        public string Title
        {
            get
            {
                return this.title;
            }
        }

        /// <summary>
        /// Empresa dueña de la clase Publicación. Get público.
        /// </summary>
        /// <value>Empresa</value>
        public Company Company
        {
            get
            {
                return this.company;
            }
        }

        /// <summary>
        /// Get público que retorna la ubicación de la publicación.
        /// </summary>
        /// <value>GeoLocation</value>
        public GeoLocation Location
        {
            get
            {
                return this.location;
            }
        }

        /// <summary>
        /// Get público del atributo Date que devuelve la hora en la que se crea la clase Publicacación.
        /// Es decir, cuando el constructor de la clase se ejecuta.
        /// </summary>
        /// <value>DateTime</value>
        public DateTime Date
        {
            get
            {
                return this.date;
            }
        }

        /// <summary>
        /// Get público del atributo Date que devuelve la hora en la que se cierra la clase Publicacación.
        /// Es decir, cuando el método ClosePublication es ejecutado.
        /// </summary>
        /// <value></value>
        public DateTime ClosedDate
        {
            get
            {
                return this.closedDate;
            }
        }

        /// <summary>
        /// Devuelve un string con todos los materiales enumerados, necesario para poder eliminar un objeto Material.
        /// </summary>
        /// <returns>String con todo los materiales enumerados.</returns>
        public IReadOnlyCollection<Material> ListMaterials
        {
            get
            {
                return this.listMaterials.AsReadOnly();
            }
        }

        /// <summary>
        /// Get público del atributo booleano IsClosed que representa el estado Abierto/Cerrado
        /// de una Publicación.
        /// </summary>
        /// <value>Bool</value>
        public Boolean IsClosed
        {
            get
            {
                return this.isClosed;
            }
        }

        /// <summary>
        /// Método que agrega a material a la publicación.
        /// </summary>
        /// <param name="material">Objeto Material</param>
        public void AddMaterial(Material material)
        {
            this.listMaterials.Add(material);
        }

        /// <summary>
        /// El método busca si hay un valor en el indice ingresado como parámetro, en caso de que exista un elemento:
        /// lo elimina y retorna True. De lo contrario solamente retorna False.
        /// </summary>
        /// <param name="material">Indice del Material que se quiera eliminar.
        /// Se obtiene con la función DevolverListaMateriales.</param>
        /// <returns></returns>
        public bool DeleteMaterial(Material material)
        {
            return this.listMaterials.Remove(material);
        }

        /// <summary>
        /// Cierra la clase Publicación por completo, asigna <c>True</c> a la variable IsClosed y
        /// llama al método DeletePublications para eliminarse a si misma de la lista estática de publicaciones
        /// de la clase conjunto publicaciones, además de esto retorna la persona que estuvo interesada.
        /// </summary>
        /// <returns>Usuario que estuvo interesado en adquirir el producto.</returns>
        public RoleEntrepreneur ClosePublication()
        {
            this.isClosed = true;
            this.closedDate = DateTime.Now;
            PublicationSet.DeletePublication(this);
            if (InterestedPerson != null)
            {
                this.company.AddListHistorialPublication(this);
                this.InterestedPerson.AddHistorialPublication(this);
                return this.InterestedPerson;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Agrega una habilitación a la lista de Habilitaciones de la clase Material.
        /// </summary>
        /// <param name="qualification">String</param>
        public void AddQualification(string qualification)
        {

            if (RoleAdmin.globalQualificationList.Contains(qualification))

            {
                this.listQualifications.Add(qualification);
            }
            else
            {
                System.Console.WriteLine("No se encuentra en la lista global de habilitaciones.");
            }
        }

        /// <summary>
        /// Elimina una habilitación de la lista de Habilitaciones de la clase Material.
        /// </summary>
        /// <param name="qualification">Índice de la Habilitación</param>
        /// <returns><c>True</c> en caso de que se pueda eliminar, <c>False</c> en caso contrario.</returns>
        public bool DeleteQualification(string qualification)
        {
            return this.listQualifications.Remove(qualification);
        }

        /// <summary>
        /// Método que setea a la persona interesada (RolEmprendedor) en el atributo InterestedPerson. 
        /// Debe ser llamado por el método ContactCompany de la clase RolEmprendedor.
        /// </summary>
        /// <param name="interestedPerson">InterestedPerson</param>
        public void SetInterestedPerson(RoleEntrepreneur interestedPerson)
        {
            this.InterestedPerson = interestedPerson;
        }
    }
}