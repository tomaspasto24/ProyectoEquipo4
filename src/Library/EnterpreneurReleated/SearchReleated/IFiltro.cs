using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library
{
    public interface IFilter
    {
        String Filter(int opcion, String listToSearch);
        //ponerle el tipo a ReturnListPublications dePublicationsSet
    }
}