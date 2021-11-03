using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Bot
{
    public class SearchByLocation: ISearch 
    {
        GeoLocation location;
        public List<Publication> Search(string addresToSearch)
        {
            double distance;
            location = new GeoLocation(addresToSearch, "Montevideo", "Montevideo");
            List<Publication> result = new List<Publication>();
            List<Publication> listaPublicaciones = PublicationSet.ListPublications;
                /// <summary>
                /// Variable para salir de la publicación cuando en ella ya se encontró el material buscado
                /// </summary>
                bool exitPublication = false;
                foreach (Publication publication in listaPublicaciones)
                {
                    distance = AsyncContext.Run<double>(() => location.CalculateDistance(publication.Location));
                    if (distance < 500)
                    {
                        result.Add(publication);
                        exitPublication = true;
                    }
                }    
            return result;
        }
    }
}