using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class SearchMaterial: ISearch
    {
        public List<Publication> Search(String wordToSearch)
        {
            List<Publication> result = new List<Publication>();

            List<Publication> listaPublicaciones = PublicationSet.ListPublications;
            bool exit = false;
                /// <summary>
                /// Variable para salir de la publicación cuando en ella ya se encontró el material buscado
                /// </summary>
                bool exitPublication = false;
                while (exitPublication)
                {
                    foreach (Publication publication in listaPublicaciones)
                    {
                        /// <summary>
                        /// Recorre cada material que hay la lista de materiales de una publicacion
                        /// </summary>
                        /// <param name="publication.ReturnListMaterials()"></param>
                        /// <returns></returns>       
                        foreach (Material material in publication.ReturnListMaterials())
                        {
                            if (material.ReturnKeyWords().Contains(wordToSearch))
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
//como usar el IFiltro para que Search no tenga q recibir una opcion
//ni agregarle un if mayor que vea qué opcione es
//buscar por material: opcion 1
//buscar por ubicacion: opcion 2

//hacer excepcion para cuando no se encuentra lo que se busca(?)