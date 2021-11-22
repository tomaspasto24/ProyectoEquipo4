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
    public class SearchByLocation : ISearch<Publication>
    {
        /// <summary>
        /// Método que búsca todas las publicaciones que contienen la ubicación pasada por parámetro. Recorre todas las
        /// publicaciones y se fija si tiene la misma ubicación recibida. Si es igual, se agrega la publicación a la lista que 
        /// va a devolver y se va a fijar a la siguiente.
        /// </summary>
        /// <param name="addresToSearch">Dirección para buscar.</param>
        /// <returns>Lista de Publicaciones.</returns>
        public IReadOnlyCollection<Publication> Search(string addresToSearch)
        {
            //double distance;           SACAR JUNTO CON EL FOREACH
            GeoLocation location = new GeoLocation(addresToSearch, "Montevideo");
            List<Publication> result = new List<Publication>();
            IReadOnlyCollection<Publication> listPublications = PublicationSet.Instance.ListPublications;
            /*foreach (Publication publication in listPublications)
            {
                distance = location.CalculateDistance(publication.Location);
                if (distance < 500)
                {
                    result.Add(publication);
                }
            }*/

            result.Add((listPublications as List<Publication>).Find(publication => publication.location.CalculateDistance(location) < 500));
            return result.AsReadOnly();
        }
    }
}