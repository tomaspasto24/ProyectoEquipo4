using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    /// <summary>
    /// Conjunto de Publicaciones, clase estática que administra la lista de publicaciones en general.
    /// </summary>
    public static class PublicationSet
    {

        private static List<Publication> listPublications = new List<Publication>();

        /// <summary>
        /// Get público que retorna la lista de publicaciones, esto para que la clase Búsqueda pueda 
        /// manipular eficientemente las Publicaciones.
        /// </summary>
        /// <value></value>
        public static IReadOnlyCollection<Publication> ListPublications
        {
            get
            {
                return listPublications.AsReadOnly();
            }
        }

        /// <summary>
        /// Método que agrega una publicación a la lista publicaciones, toma como parámetro 
        /// todos los datos para poder crear una instancia de Publicación dentro del método,
        /// cumpliendo así con el Creator Pattern.
        /// </summary>
        /// <param name="title">String Título.</param>
        /// <param name="company">Clase Empresa.</param>
        /// <param name="location">Clase Ubicación.</param>
        /// <param name="material">Clase Material que es tomado como el primero de la Publicación.</param>
        public static Publication AddPublication(string title, Company company, GeoLocation location, Material material)
        {
            Publication publication = null;
            if (company != null)
            {
                publication = new Publication(title, company, location, material);
                listPublications.Add(publication);
                company.AddOwnPublication(publication);
            }
            return publication;

        }

        /// <summary>
        /// Elimina una Publicación de la lista publicaciones.
        /// </summary>
        /// <param name="publication">Publicación.</param>
        /// <returns><c>True</c> en caso de que se haya eliminado correctamente y <c>False</c> en caso 
        /// contrario.</returns>
        public static bool DeletePublication(Publication publication)
        {
            return listPublications.Remove(publication);
        }

        /// <summary>
        /// Método que retorna la lista completa de Publicaciones en un string con sus respectivos
        /// índices.
        /// </summary>
        /// <returns>String con el nombre de la Publicación y sus indices.</returns>
        public static string ReturnListPublications()
        {
            StringBuilder resultado = new StringBuilder("Publicaciones: \n");
            int contador = 1;

            foreach (Publication publication in listPublications)
            {
                resultado.Append($"{++contador}- {publication.Title} \n");
            }
            return resultado.ToString();
        }
    }
}