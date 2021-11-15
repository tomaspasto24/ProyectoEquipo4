using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    /// <summary>
    /// Conjunto de Publicaciones, clase que se encarga de administrar la lista de Publicaciones en general. Su constructor se encuentra 
    /// privado para que no sea posible crear más de una instancia de la clase, para obtener la instancia se necesita llamar al método
    /// GetInstance que devuelve la única instancia que puede ser usada, cumpliendo así con el patrón de diseño Singleton.
    /// </summary>
    public class PublicationSet : ISet<Publication>
    {
        private static PublicationSet _instance;
        private PublicationSet() { }

        /// <summary>
        /// Método estático que controla el acceso a la propia instancia de la clase PublicationSet,
        /// en caso de que la variable _instance no este creada, la crea y la retorna. En caso 
        /// contrario de que anteriormente este creada simplemente la retorna, asi se asegura de que
        /// siempre se use la misma variable instancia y se cumpla con Singleton.
        /// </summary>
        /// <returns>Instancia PublicationSet.</returns>
        public static PublicationSet GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PublicationSet();
            }
            return _instance;
        }

        private List<Publication> listPublications = new List<Publication>();
    
        /// <summary>
        /// Obtiene la lista de Publicaciones.
        /// </summary>
        /// <value>Lista de solo lectura de clase Publicación.</value>
        public IReadOnlyCollection<Publication> ListPublications
        {
            get
            {
                return this.listPublications.AsReadOnly();
            }
        }

        /// <summary>
        /// Método que se encarga de agregar una Publicación a la lista de Publicaciones del sistema.
        /// </summary>
        /// <returns><c>True</c> en caso de que se pueda agregar y <c>False</c> en caso
        /// contrario.</returns>
        public bool AddElement(Publication publication)
        {
            if (!ContainsElementInListElements(publication))
            {
                this.listPublications.Add(publication);
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
        /// <param name="publication">Publicación.</param>
        /// <returns><c>True</c> en caso de que se haya eliminado correctamente y <c>False</c> en caso
        /// contrario.</returns>
        public bool DeleteElement(Publication publication)
        {
            if(ContainsElementInListElements(publication))
            {
                this.listPublications.Remove(publication);
                return true;
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
        /// <param name="publication">Publicación.</param>
        /// <returns><c>True</c> en caso de encontrarse en el sistema y <c>False</c> en caso
        /// contrario.</returns>
        public bool ContainsElementInListElements(Publication publication)
        {
            if (publication != null)
            {
                foreach (Publication item in this.listPublications)
                {
                    if (item.Title == publication.Title)
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                throw new ArgumentNullException(nameof(publication));
            }
        }
        
        /// <summary>
        /// Sobrecarga de ContainsElementInListElements, se encarga de comprobar si el nombre de una clase Publicación se encuentra
        /// en la lista de Publicaciones.
        /// </summary>
        /// <param name="publicationName">Nombre de Publicación.</param>
        /// <returns><c>True</c> en caso de encontrarse en el sistema y <c>False</c> en caso
        /// contrario.</returns>
        public bool ContainsElementInListElements(string publicationName)
        {
            if (publicationName != null)
            {
                foreach (Publication item in this.listPublications)
                {
                    if (item.Title == publicationName)
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                throw new ArgumentNullException(nameof(publicationName));
            }
        }
    }
}