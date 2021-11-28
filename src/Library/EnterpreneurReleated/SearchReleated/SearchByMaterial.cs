using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    /// <summary>
    /// Clase que implementa la búsqueda de una publicación, en este caso la búsqueda por material.
    /// Patrones y principios:
    /// Esta clase cumple con en patrón Expert porque es experta en cómo hacer una búsqueda por material. Además, 
    /// cumple con el principio SRP dado que su única razón de cambio es cómo buscar una publicación 
    /// que contenga al material que se le indica.
    /// </summary>
    public class SearchByMaterial : ISearch<Publication>
    {
        /// <summary>
        /// Método que búsca todas las publicaciones que contienen el material pasado por parámetro. Recorre todas las
        /// publicaciones y se fija si alguno de sus materiales, tiene a la palabra que recibió por parámetro,
        /// dentro de la lista de palabras claves. Si la encuentra, se agrega la publicación a la lista va a devolver 
        /// y se va a fijar a la siguiente.
        /// </summary>
        /// <param name="wordToSearch"></param>
        /// <returns>Lista de publicaciones.</returns>
        public string Search(string wordToSearch)
        {
            string publications = string.Empty;
            foreach (Publication publication in (PublicationSet.Instance.ListPublications))
            {
                foreach (Material mat in (publication.ListMaterials as List<Material>))
                {
                    if (mat.Name == wordToSearch)
                    {
                        publications = publications + publication.ReturnPublication();
                    }
                }
            }
            return publications;
        }

        private static SearchByMaterial instance;
        /// <summary>
        /// Obtiene una única instancia de esta clase
        /// </summary>
        /// <value>La única instancia de esta clase.</value>
        public static SearchByMaterial Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SearchByMaterial();
                }
                return instance;
            }
        }
    }
}
