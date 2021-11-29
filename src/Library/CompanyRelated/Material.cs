/* Encabezado
*/
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Bot
{
    /// <summary>
    /// Clase Material se encarga de representar cada material, tiene la responsabilidad de conocer su nombre, cantidad, precio y 
    /// sus palabras clave.
    /// Cumple con el patrón Expert (GRASP) y SRP (SOLID) porque todas las operaciones que hace, las realiza con
    /// datos internos que solo la clase Material conoce ya que es experta en lo que hace. En caso de que la clase Material no se hubiera creado, 
    /// la clase Publication tendría más de una responsabilidad y además imposibilitaría que el usuario pueda publicar más de un material a la vez rompiendo
    /// con el patron SRP.
    /// </summary>
    public class Material
    {
        private string name;
        private int quantity;
        private double price;
        private IList<string> keyWords = new List<string>(); // Palabras clave

        /// <summary>
        /// Constructor sin implementación para ser usado por JsonConstructor.
        /// </summary>
        [JsonConstructor]
        public Material() { }

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
        /// Obtiene o establece nombre del la clase Material.
        /// </summary>
        /// <value>String.</value>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Obtiene todas las Palabras Clave que contiene el Material.
        /// </summary>
        /// <returns>Cadena de caracteres.</returns>
        [JsonInclude]
        public IReadOnlyList<string> KeyWords
        {
            get
            {
                return new ReadOnlyCollection<string>(this.keyWords);
            }
        }

        /// <summary>
        /// Obtiene o establece cantidad del la clase Material.
        /// </summary>
        /// <value>Entero.</value>
        public int Quantity
        {
            get
            {
                return this.quantity;
            }

            set
            {
                this.quantity = value;
            }
        }

        /// <summary>
        /// Obtiene o establece precio del la clase Material.
        /// </summary>
        /// <value>Entero.</value>
        public double Price
        {
            get
            {
                return this.price;
            }

            set
            {
                this.price = value;
            }
        }

        /// <summary>
        /// Agrega una palabra clave a la lista de palabras clave.
        /// </summary>
        /// <param name="keyWord">Palabra clave.</param>
        public void AddKeyWord(string keyWord) // Agregar palabras clave
        {
            // TODO revisar que en el handler se agreguen KeyWords
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