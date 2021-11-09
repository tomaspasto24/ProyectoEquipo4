using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    /* 
        Al tener esta interfaz se cumple con el principio OCP, implementándola podemos extender el código a nuevas formas de búsqueda
        sin cambiar el código de las que ya tenemos.
        También se cumple con el principio DIP porque se depende de una abstracción en lugar de directamente de una clase.
    */
    public interface ISearch
    {
        List<Publication> Search(string wordToSearch);
    }
}