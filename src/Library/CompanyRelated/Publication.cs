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
        private List<Material> listMaterials = new List<Material>();
        private List<string> listRatings = new List<string>(); // Lista Habilitaciones

        private DateTime date;
        private DateTime closedDate;
        private GeoLocation location;
        private Company company;

        /// <summary>
        /// Atributo público de la clase Publicación con set privado, quedando el get público.
        /// Este atributo se setea cuando una clase Emprendedor ejecuta el método AskContactToPublication
        /// sobre una publicación lo que acciona el método interno SetInterestedPerson seteando a la persona
        /// interesada.
        /// </summary>
        /// <value>RoleEntrepreneur.</value>
        public RoleEntrepreneur interestedPerson { get; private set; } // Hay que ver como guardar la persona interesada
        private bool isClosed = false;

        /// <summary>
        /// Titulo que representa la publicación. Más que nada para poder retornar una lista
        /// identificando por título.
        /// </summary>
        /// <value>string</value>
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
        /// Constructor de Publicación, instancia la hora del sistema actual en donde se crea y setea nombreEmpresa, ubicacion, material y titulo de la publicacion.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="Company"></param>
        /// <param name="location"></param>
        /// <param name="material"></param>
        public Publication(String title, Company Company, GeoLocation location, Material material)
        {
            this.title = title;
            this.company = Company;
            this.date = DateTime.Now;
            this.closedDate = DateTime.MinValue;
            this.location = location;
            AddMaterial(material);
            this.interestedPerson = null;
        }

        /// <summary>
        /// Método que agrega a material a la publicación.
        /// </summary>
        /// <param name="material">Objeto Material</param>
        public void AddMaterial(Material material)
        {
            listMaterials.Add(material);
        }

        /// <summary>
        /// El método busca si hay un valor en el indice ingresado como parámetro, en caso de que exista un elemento:
        /// lo elimina y retorna True. De lo contrario solamente retorna False.
        /// </summary>
        /// <param name="indiceMaterial">Indice del Material que se quiera eliminar.
        /// Se obtiene con la función DevolverListaMateriales.</param>
        /// <returns></returns>
        public bool DeleteMaterial(int indiceMaterial)
        {
            return listMaterials.Remove(listMaterials[indiceMaterial]);
        }

        /// <summary>
        /// Devuelve un string con todos los materiales enumerados, necesario para poder eliminar un objeto Material.
        /// </summary>
        /// <returns>String con todo los materiales enumerados</returns>
        public List<Material> ReturnListMaterials()
        {
            return listMaterials;
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
            if(interestedPerson != null)
            {
                this.company.AddListHistorialPublications(this);
                this.interestedPerson.AddHistorialPublication(this);
                return interestedPerson;
            }
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// Agrega una habilitación a la lista de Habilitaciones de la clase Material.
        /// </summary>
        /// <param name="habilitacion">String</param>
        public void AddRating(string habilitacion)
        {

            if (RoleAdmin.globalRatingsList.Contains(habilitacion))

            {
                listRatings.Add(habilitacion);
            }
            else
            {
                System.Console.WriteLine("No se encuentra en la lista global de habilitaciones.");
            }
        }

        /// <summary>
        /// Elimina una habilitación de la lista de Habilitaciones de la clase Material.
        /// </summary>
        /// <param name="indiceHabilitacion">Índice de la Habilitación</param>
        /// <returns><c>True</c> en caso de que se pueda eliminar, <c>False</c> en caso contrario.</returns>
        public bool DeleteRating(int indiceHabilitacion)
        {
            return listRatings.Remove(listRatings[indiceHabilitacion]);
        }

        /// <summary>
        /// Retorna la lista de Habilitaciones que tiene el material.s
        /// </summary>
        /// <returns>String</returns>
        public string ReturnListRatings()
        {
            StringBuilder resultado = new StringBuilder("Habilitaciones: \n");
            int contador = 0;

            foreach (string palabra in this.listRatings)
            {
                resultado.Append($"{++contador}- {palabra} \n");
            }
            return resultado.ToString();
        }

        /// <summary>
        /// Método que setea a la persona interesada (RolEmprendedor) en el atributo InterestedPerson. 
        /// Debe ser llamado por el método ContactCompany de la clase RolEmprendedor.
        /// </summary>
        /// <param name="interestedPerson">InterestedPerson</param>
        public void SetInterestedPerson(RoleEntrepreneur interestedPerson)
        {
            this.interestedPerson = interestedPerson;
        }
    }
}