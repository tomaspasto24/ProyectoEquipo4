/* Encabezado
*/
using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Clase Material que cumple con el patrón Expert ya que todos los calculos que hace, los realiza con
    /// datos internos que solo la clase Material conoce.
    /// </summary>
    public class Material
    {
        private string name;
        private int quantity;
        private double price;
        private List<string> keyWords = new List<string>(); // Palabras clave

        /// <summary>
        /// Método de la clase Material que se encarga de asignar valores a los atributos name, quantity y price. Construye el Material. En caso de no tener precio, se asigna 0 a su atributo.
        /// </summary>
        /// <param name="name">String nombre.</param>
        /// <param name="quantity">Entero cantidad.</param>
        /// <param name="price">Entero precio.</param>
        public Material(string name, int quantity, int price)
        {
            this.name = name;
            this.quantity = quantity;
            this.price = price;   
        }

        /// <summary>
        /// Obtiene nombre del la clase Material.
        /// </summary>
        /// <value>String.</value>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Obtiene todas las Palabras Clave que contiene el Material.
        /// </summary>
        /// <returns>Cadena de caracteres.</returns>
        public IReadOnlyList<string> KeyWords
        {
            get
            {
                return this.keyWords.AsReadOnly();
            }
        }

        /// <summary>
        /// Obtiene cantidad del la clase Material.
        /// </summary>
        /// <value>Entero.</value>
        public int Quantity
        {
            get
            {
                return this.quantity;
            }
        }

        /// <summary>
        /// Obtiene precio del la clase Material.
        /// </summary>
        /// <value>Entero.</value>
        public double Price
        {
            get
            {
                return this.price;
            }
        }

        /// <summary>
        /// Agrega una palabra clave a la lista de palabras clave.
        /// </summary>
        /// <param name="keyWord">Palabra clave.</param>
        public void AddKeyWord(string keyWord) // Agregar palabras clave
        {
            this.keyWords.Add(keyWord);
        }

        /// <summary>
        /// El método busca si hay un valor del string ingresado como parámetro, en caso de que exista un elemento:
        /// lo elimina y retorna True. De lo contrario solamente retorna False.
        /// </summary>
        /// <param name="keyWord">Cadena de caracteres de la palabra clave que se quiera eliminar.</param>
        /// <returns><c>True</c> en caso de que se pueda eliminar, <c>False</c> en caso contrario.</returns>
        public bool DeleteKeyWord(string keyWord)
        {
            return this.keyWords.Remove(keyWord);
        }
    }
}