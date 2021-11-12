using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    /// <summary>
    /// Esta clase cumple con en patrón Expert porque es experta en cómo hacer una búsqueda por material. Además, 
    /// cumple con el principio SRP dado que su única razón de cambio es cómo buscar una publicación 
    /// que contenga al material que se le indica.
    /// </summary>
    public class SearchByMaterial: ISearch
    {
        /// <summary>
        /// Método que búsca todas las publicaciones que contienen el material pasado por parámetro. Recorre todas las
        /// publicaciones y se fija si alguno de sus materiales, tiene a la palabra que recibió por parámetro,
        /// dentro de la lista de palabras claves. Si la encuentra, se agrega la publicación a la lista va a devolver 
        /// y se va a fijar a la siguiente.
        /// </summary>
        /// <param name="wordToSearch"></param>
        /// <returns></returns>
        public List<Publication> Search(String wordToSearch)
        {
            List<Publication> result = new List<Publication>();

            List<Publication> listPublications = PublicationSet.ListPublications;
            bool exitPublication = false;   // Variable para salir de la publicación cuando se encontró el material buscado
            foreach (Publication publication in listPublications)
            {
                while (!exitPublication)
                {       
                    foreach (Material material in publication.ReturnListMaterials())
                    {
                        if (material.KeyWords.Contains(wordToSearch))
                        {
                            result.Add(publication);
                            exitPublication = true; 
                        } 
                    }
                }                    
            }      
            return result;
        }
    }
}
