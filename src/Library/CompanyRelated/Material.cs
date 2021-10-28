using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    public class Material
    {
        private string name;
        private int quantity;
        private int price;
        private List<string> keyWords = new List<string>(); // Palabras clave

        /// <summary>
        /// Devuelve Atributo nombre del la clase Material.
        /// </summary>
        /// <value>String</value>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Constructor de la clase Material que se encarga de asignar valores a los 
        /// atributos name, quantity y price. Construye el Material. En caso de no tener 
        /// precio, se asigna 0 a su atributo.
        /// </summary>
        /// <param name="nombre">String nombre.</param>
        /// <param name="cantidad">Int cantidad</param>
        /// <param name="precio">Int precio</param>
        public Material(string nombre, int cantidad, int precio)
        {
            this.name = nombre;
            this.quantity = cantidad;
            this.price = precio;   
        }

        /// <summary>
        /// Agrega una palabra clave a la lista de palabras clave
        /// </summary>
        /// <param name="palabraClave"></param>
        public void AddKeyWord(string palabraClave) // Agregar palabras clave
        {
            this.keyWords.Add(palabraClave);
        }

        /// <summary>
        /// El método busca si hay un valor en el indice ingresado como parámetro, en caso de que exista un elemento:
        /// lo elimina y retorna True. De lo contrario solamente retorna False.
        /// </summary>
        /// <param name="indicePalabraClave">Indice de la palabra clave que se quiera eliminar.
        /// Se obtiene con la función DevolverPalabrasClave.</param>
        /// <returns></returns>
        public bool DeleteKeyWord(int indicePalabraClave)
        {
            return keyWords.Remove(keyWords[indicePalabraClave]);
        }

        /// <summary>
        /// Método que devuelve todas las Palabras Clave que contiene el Material.
        /// </summary>
        /// <returns>String</returns>
        public string ReturnKeyWords() 
        {
            StringBuilder resultado = new StringBuilder("Palabras Clave: \n");
            int contador = 0;

            foreach(string palabra in this.keyWords)
            {
                resultado.Append($"{++contador}- {palabra} \n");
            }
            return resultado.ToString();
        }
    }
}