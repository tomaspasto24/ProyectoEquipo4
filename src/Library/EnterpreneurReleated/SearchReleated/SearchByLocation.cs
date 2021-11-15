using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Bot
{
    /// <summary>
    /// Esta clase cumple con en patrón Expert porque es experta en cómo hacer una búsqueda por ubicación. Además,
    /// cumple con el principio SRP dado que su única razón de cambio es cómo buscar una publicación con la ubicación que se le indica.
    /// </summary>
    public class SearchByLocation : ISearch
    {
        GeoLocation location;
        
        /// <summary>
        /// Método que búsca todas las publicaciones que contienen la ubicación pasada por parámetro. Recorre todas las
        /// publicaciones y se fija si tiene la misma ubicación recibida. Si es igual, se agrega la publicación a la lista que 
        /// va a devolver y se va a fijar a la siguiente.
        /// </summary>
        /// <param name="addresToSearch">Dirección para buscar.</param>
        /// <returns>Lista de Publicaciones.</returns>
        public List<Publication> Search(string addresToSearch)
        {
            double distance;
            location = new GeoLocation(addresToSearch, "Montevideo");
            List<Publication> result = new List<Publication>();
            List<Publication> listaPublicaciones = PublicationSet.ReturnListPublications();
                foreach (Publication publication in listaPublicaciones)
                {
                    distance = AsyncContext.Run<double>(() => location.CalculateDistance(publication.Location));
                    if (distance < 500)
                    {
                        result.Add(publication);
                    }
                }    
            return result;
        }
    }
}