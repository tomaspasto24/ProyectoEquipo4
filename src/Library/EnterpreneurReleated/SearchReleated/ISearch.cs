using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Al tener esta interfaz se cumple con el principio OCP, implementándola podemos extender el código a nuevas formas de búsqueda
    /// sin cambiar el código de las que ya tenemos.
    /// También se cumple con el principio DIP porque se depende de una abstracción en lugar de directamente de una clase.
    /// </summary>
    public interface ISearch
    {
        /// <summary>
        /// Método que se implementa tanto en las clases SerachByMaterial, SearchByLocation y en cualquier otra que se agregue
        /// para hacer una búsqueda.
        /// </summary>
        /// <param name="wordToSearch">String por el que se buscará.</param>
        /// <returns>Devuelve una lista con las publicaciónes que cumplan con la búsqueda.</returns>
    List<Publication> Search(string wordToSearch);
    }
}