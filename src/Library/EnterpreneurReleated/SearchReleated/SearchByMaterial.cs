using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class SearchByMaterial: ISearch
    {
        public List<Publication> Search(String wordToSearch)
        {
            List<Publication> result = new List<Publication>();

            bool exit = false;
            /// <summary>
            /// VAriable para salir de la publicación cuando se encontró el material buscado
            /// </summary>
            bool exitPublication = false;
            foreach (Publication publication in PublicationSet.ListPublication)
            {
                while (!exitPublication)
                {
                    /// <summary>
                    /// Recorre cada material que hay la lista de materiales de una publicacion
                    /// </summary>
                    /// <param name="publication.ReturnListMaterials()"></param>
                    /// <returns></returns>       
                    foreach (Material material in publication.ListMaterials)
                    {
                        if (material.KeyWords.Contains(wordToSearch))
                        {
                            result.Add(publication);
                            exit = true; 
                        } 
                    }
                }                    
            }      
            return result;
        }
    }
}
