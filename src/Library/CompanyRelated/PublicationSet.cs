using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bot
{
    /// <summary>
    /// Conjunto de Publicaciones, clase que se encarga de administrar la lista de Publicaciones en general.
    /// Cumple con el patrón de creación Singleton (Ver Readme).
    /// </summary>
    public class PublicationSet : ISetOfElement<Publication>, IJsonConvertible
    {
        private static PublicationSet instance;
        [JsonInclude]
        private IList<Publication> listPublications;
        [JsonConstructor]
        private PublicationSet()
        {
            this.Initialize();
        }

        /// <summary>
        /// Obtiene el acceso a la propia instancia de la clase PublicationSet,
        /// en caso de que el atributo instance no este creado, lo crea y lo retorna. En caso 
        /// contrario de que anteriormente este creado simplemente lo retorna, asi se asegura de que
        /// siempre se use la misma instancia y se cumpla con Singleton.
        /// </summary>
        /// <returns>Instancia PublicationSet.</returns>
        public static PublicationSet Instance
        {
            get
            {
            if (instance == null)
            {
                instance = new PublicationSet();
            }

            return instance;
            }
        }
    
        /// <summary>
        /// Obtiene la lista de Publicaciones.
        /// </summary>
        /// <value>Lista de solo lectura de clase Publicación.</value>
        [JsonInclude]
        public IReadOnlyCollection<Publication> ListPublications
        {
            get
            {
                return new ReadOnlyCollection<Publication>(this.listPublications);
            }
        }

        /// <summary>
        /// Método que se encarga de agregar una Publicación a la lista de Publicaciones del sistema.
        /// </summary>
        /// <param name="element">Elemento Publicación.</param>
        /// <returns><c>True</c> en caso de que se pueda agregar y <c>False</c> en caso
        /// contrario.</returns>
        public bool AddElement(Publication element)
        {
            if (!this.ContainsElementInListElements(element))
            {
                this.listPublications.Add(element);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método que se encarga de eliminar una Publicación de la lista de Publicaciones del sistema.
        /// </summary>
        /// <param name="element">Publicación.</param>
        /// <returns><c>True</c> en caso de que se haya eliminado correctamente y <c>False</c> en caso
        /// contrario.</returns>
        public bool DeleteElement(Publication element)
        {
            if (this.ContainsElementInListElements(element))
            {
                return this.listPublications.Remove((this.listPublications as List<Publication>).Find(publicationInList => publicationInList.Title == element.Title));
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método que retorna la lista completa de Publicaciones en un string.
        /// </summary>
        /// <returns>String con los nombres de las Publicaciones.</returns>
        public string ReturnListElements()
        {
            StringBuilder result = new StringBuilder("Publicaciones: \n");

            foreach (Publication publication in this.listPublications)
            {
                result.Append($"{publication.Title} \n");
            }

            return result.ToString();
        }

        /// <summary>
        /// Método simple que se encarga de comprobar si una clase Publicación se encuentra
        /// en el sistema de Publicaciones.
        /// </summary>
        /// <param name="element">Publicación.</param>
        /// <returns><c>True</c> en caso de encontrarse en el sistema y <c>False</c> en caso
        /// contrario.</returns>
        public bool ContainsElementInListElements(Publication element)
        {
            if (element != null)
            {
                foreach (Publication item in this.listPublications)
                {
                    if (item.Title == element.Title)
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
        /// Sobrecarga de ContainsElementInListElements, se encarga de comprobar si el nombre de una clase Publicación se encuentra
        /// en la lista de Publicaciones.
        /// </summary>
        /// <param name="elementName">Nombre de Publicación.</param>
        /// <returns><c>True</c> en caso de encontrarse en el sistema y <c>False</c> en caso
        /// contrario.</returns>
        public bool ContainsElementInListElements(string elementName)
        {
            if (elementName != null)
            {
                foreach (Publication item in this.listPublications)
                {
                    if (item.Title == elementName)
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
        /// Método que es llamado por el constructor privado para inicializar la lista de clases Publicación.
        /// </summary>
        public void Initialize()
        {
            this.listPublications = new List<Publication>();
        }

        /// <summary>
        /// Método que se encarga de convertir en JSON las publicaciones creadas y escribirlas en su
        /// correspondiente archivo JSON.
        /// </summary>
        public void ConvertToJson()
        {
            JsonSerializerOptions options = new ()
            {
                ReferenceHandler = MyReferenceHandler.Instance,
                WriteIndented = true,
            };

            string json = JsonSerializer.Serialize(this.listPublications as List<Publication>, options);

            try
            {
                File.WriteAllText(@"..\..\docs\PublicationDataBase.json", json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Método que se encarga de leer el archivo JSON donde se guardan las publicaciones y deserializarlo.
        /// </summary>
        public void LoadFromJson()
        {
            try
            {
                string jsonData = File.ReadAllText(@"..\..\docs\PublicationDataBase.json");
                JsonSerializerOptions options = new ()
                {
                    ReferenceHandler = MyReferenceHandler.Instance,
                    WriteIndented = true,
                };

                // List<Publication> listPublicationsFromJson = JsonSerializer.Deserialize<List<Publication>>(jsonData, options);
                List<Publication> listPublicationsFromJson = JsonSerializer.Deserialize<List<Publication>>(jsonData, options);

                foreach (Publication item in listPublicationsFromJson)
                {
                    this.AddElement(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}