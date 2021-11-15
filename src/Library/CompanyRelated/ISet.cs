namespace Bot
{
    /// <summary>
    /// Interfaz pública ISet para CompanySet y PublicationSet.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISet<T>
    {
        /// <summary>
        /// Método que se encarga de agregar un elemento a la lista de elementos del propio Set en cuestión.
        /// </summary>
        /// <param name="element">Elemento.</param>
        /// <returns><c>True</c> en caso de que se pueda agregar y <c>False</c> en caso
        /// contrario.</returns>
        bool AddElement(T element);

        /// <summary>
        /// Método que se encarga de eliminar un elemento a la lista de elementos del propio Set en cuestión.
        /// </summary>
        /// <param name="element">Elemento.</param>
        /// <returns><c>True</c> en caso de que se pueda eliminar y <c>False</c> en caso
        /// contrario.</returns>
        bool DeleteElement(T element);

        /// <summary>
        /// Método que retorna en forma de string la lista de elementos.
        /// </summary>
        /// <returns>String.</returns>
        string ReturnListElements();

        /// <summary>
        /// Método que se encarga de buscar el elemento en la lista de elementos y retornar si se encuentra o no.
        /// </summary>
        /// <param name="element"></param>
        /// <returns><c>True</c> en caso de que se pueda encontrar y <c>False</c> en caso
        /// contrario.</returns>
        bool ContainsElementInListElements(T element);

        /// <summary>
        /// Sobrecarga del método ContainsElementInListElements, mismo funcionamiento pero con el nombre del elemento.
        /// </summary>
        /// <param name="elementName">Nombre del elemento.</param>
        /// <returns><c>True</c> en caso de que se pueda encontrar y <c>False</c> en caso
        /// contrario.</returns>
        bool ContainsElementInListElements(string elementName);
    }
}