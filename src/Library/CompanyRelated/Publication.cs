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

        public Company Company
        {
            get
            {
                return this.company;
            }
        }

        public DateTime Date
        {
            get
            {
                return this.date;
            }
        }
        public DateTime ClosedDate
        {
            get
            {
                return this.closedDate;
            }
            set
            {
                this.ClosedDate = value;
            }
        }

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
        public string ReturnListMaterials()
        {
            StringBuilder resultado = new StringBuilder("Materiales: \n");
            int contador = 0;

            foreach (Material material in this.listMaterials)
            {
                resultado.Append($"{++contador}- {material.Name} \n");
            }
            return resultado.ToString();
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
            PublicationSet.DeletePublications(this);
            this.company.AddListHistorialPublications(this);
            // this.interestedPerson.SaveHistorialPublication(this);
            return interestedPerson;
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
    }
}