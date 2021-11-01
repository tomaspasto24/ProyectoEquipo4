using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class Search
    {

        public List<Publication> SearchingMaterial(int option, String wordTosearch)
        {
            List<Publication> resultado = new List<Publication>();

            //LLAMAR A LA LISTA DE PUBLICACIONES SIN CREAR UNA NUEVA

            foreach (Publication publication in ReturnListPublication)
            {
                foreach (Material material in ReturnListMaterials)
                {
                    List<String> listKeyWords = new List<String>();
                    listKeyWords = ReturnKeyWord().Split(' ').ToList();
                    foreach (String keyword in listKeyWords())
                    {
                        if (keyword == wordToSearch)
                        {
                            resultado.Add(publication);
                            //acá terminar el primer foreach
                        }
                    }

                }
            }
            return resultado;

        }
    }
}
//como usar el IFiltro para que Search no tenga q recibir una opcion
//ni agregarle un if mayor que vea qué opcione es
//buscar por material: opcion 1
//buscar por ubicacion: opcion 2

//hacer excepcion para cuando no se encuentra lo que se busca(?)

//los atributos de publications tienen q ser públicos(?)