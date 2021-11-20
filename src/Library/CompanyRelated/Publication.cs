using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Bot
{
    /// <summary>
    /// Cada objeto de la clase Publicación, administrado por un objeto Empresa, es el conjunto de items
    /// que la aplicación muestra a los emprendedores. Cumple con el patrón de diseño Creator porque la clase
    /// Publication tiene la responsabilidad de crear instancias de la clase Material porque guarda instancias
    /// de Material y lo usa de forma cercana.
    /// </summary>
    public class Publication
    {
        private string title;
        private DateTime date;
        private DateTime closedDate;
        private GeoLocation location;
        private Company company;
        private bool isClosed;
        private IList<Material> listMaterials = new List<Material>();
        private IList<string> listQualifications = new List<string>(); // Lista Habilitaciones

        /// <summary>
        /// Constructor ingresado en blanco para la implementación de la Serialización.
        /// </summary>
        [JsonConstructor]
        public Publication() { }

        /// <summary>
        /// Constructor de Publicación, instancia la hora del sistema actual en donde se crea y setea nombreEmpresa, ubicacion, material y titulo de la publicacion.
        /// </summary>
        /// <param name="title">Titulo.</param>
        /// <param name="company">Empresa.</param>
        /// <param name="location">Ubicación.</param>
        /// <param name="material">Material.</param>
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
        /// Obtiene una instancia de RoleEntrepreneur que referencia al emprendedor interesado.
        /// </summary>
        /// <value>Rol Emprendedor.</value>
        public RoleEntrepreneur InterestedPerson { get; private set; }

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
        /// Obtiene clase Empresa dueña de la clase Publicación. Get público.
        /// </summary>
        /// <value>Empresa.</value>
        public Company Company
        {
            get
            {
                return this.company;
            }
        }

        /// <summary>
        /// Obtiene la ubicación de la publicación.
        /// </summary>
        /// <value>GeoLocation.</value>
        public GeoLocation Location
        {
            get
            {
                return this.location;
            }
        }

        /// <summary>
        /// Obtiene el atributo Date que devuelve la hora en la que se crea la clase Publicacación.
        /// Es decir, cuando el constructor de la clase se ejecuta.
        /// </summary>
        /// <value>DateTime.</value>
        public DateTime Date
        {
            get
            {
                return this.date;
            }
        }

        /// <summary>
        /// Obtiene el atributo Date que devuelve la hora en la que se cierra la clase Publicacación.
        /// Es decir, cuando el método ClosePublication es ejecutado.
        /// </summary>
        /// <value>DateTime.</value>
        public DateTime ClosedDate
        {
            get
            {
                return this.closedDate;
            }
        }

        /// <summary>
        /// Obtiene una lista de solo lectura con todos los materiales.
        /// </summary>
        /// <returns>Lista de solo lectura de Material.</returns>
        [JsonInclude]
        public IReadOnlyList<Material> ListMaterials
        {
            get
            {
                return new ReadOnlyCollection<Material>(this.listMaterials);
            }
        }

        /// <summary>
        /// Obtiene una lista de solo lectura de los string Habilitaciones.
        /// </summary>
        /// <value>Lista de solo lectura de cadena de caracteres.</value>
        [JsonInclude]
        public IReadOnlyList<string> ListQualifications
        {
            get
            {
                return new ReadOnlyCollection<string>(this.listQualifications);
            }
        }

        /// <summary>
        /// Obtiene un valor que indica si el estado de la publicación es abierto o cerrado.
        /// </summary>
        /// <value>Booleano.</value>
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
        /// <param name="material">Objeto Material.</param>
        public void AddMaterial(Material material)
        {
            this.listMaterials.Add(material);
        }

        /// <summary>
        /// Sobrecarga de AddMaterial que agrega una lista de materiales a listMaterials.
        /// </summary>
        /// <param name="listMaterials"></param>
        public void AddMaterial(IReadOnlyList<Material> listMaterials)
        {
            (this.listMaterials as List<Material>).AddRange(listMaterials);
        }

        /// <summary>
        /// El método busca si hay un valor igual a la instancia Material ingresada, en caso de que exista un elemento:
        /// lo elimina y retorna True. De lo contrario solamente retorna False.
        /// </summary>
        /// <param name="material">Indice del Material que se quiera eliminar.
        /// Se obtiene con la función DevolverListaMateriales.</param>
        /// <returns><c>True</c> en caso de que se elimine correctamente, <c>False</c> en caso contrario.</returns>
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
            PublicationSet.Instance.DeleteElement(this);
            if (this.InterestedPerson != null)
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
        /// <param name="qualification">String.</param>
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
        /// Sobrecarga del método AddQualification que agrega una lista de Habilitaciones a listQualifications.
        /// </summary>
        /// <param name="listQualifications"></param>
        public void AddQualification(IReadOnlyList<string> listQualifications)
        {
            if (listQualifications != null)
            {
                bool adminCondition = true;
                foreach (string qualification in listQualifications)
                {
                    if (!RoleAdmin.globalQualificationList.Contains(qualification))
                    {
                        adminCondition = false;
                    }
                }

                if (adminCondition)
                {
                    (this.listQualifications as List<string>).AddRange(listQualifications);
                }
                else
                {
                    System.Console.WriteLine("No se encuentra en la lista global de habilitaciones.");
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(listQualifications));
            }
        }

        /// <summary>
        /// Elimina una habilitación de la lista de Habilitaciones de la clase Material.
        /// </summary>
        /// <param name="qualification">Índice de la Habilitación.</param>
        /// <returns><c>True</c> en caso de que se pueda eliminar, <c>False</c> en caso contrario.</returns>
        public bool DeleteQualification(string qualification)
        {
            return this.listQualifications.Remove(qualification);
        }

        /// <summary>
        /// Método que setea a la persona interesada (RolEmprendedor) en el atributo InterestedPerson.
        /// Debe ser llamado por el método ContactCompany de la clase RolEmprendedor.
        /// </summary>
        /// <param name="interestedPerson">InterestedPerson.</param>
        public void SetInterestedPerson(RoleEntrepreneur interestedPerson)
        {
            this.InterestedPerson = interestedPerson;
        }
    }
}