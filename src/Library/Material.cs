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
        private List<string> listRatings = new List<string>(); // Lista Habilitaciones

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

        /// <summary>
        /// Agrega una habilitación a la lista de Habilitaciones de la clase Material.
        /// </summary>
        /// <param name="habilitacion">String</param>
        public void AddRating(string habilitacion)
        {
            listRatings.Add(habilitacion);
        }

        /// <summary>
        /// Elimina una habilitación de la lista de Habilitaciones de la clase Material.
        /// </summary>
        /// <param name="indiceHabilitacion">Índice de la Habilitación</param>
        /// <returns><c>True</c> en caso de que se pueda eliminar, <c>False</c> en caso contrario.</returns>
        public bool DeleteRating(int indiceHabilitacion)
        {
            return listRatings.Remove(keyWords[indiceHabilitacion]);
        }

        /// <summary>
        /// Retorna la lista de Habilitaciones que tiene el material.s
        /// </summary>
        /// <returns>String</returns>
        public string ReturnListRatings()
        {
            StringBuilder resultado = new StringBuilder("Habilitaciones: \n");
            int contador = 0;

            foreach(string palabra in this.listRatings)
            {
                resultado.Append($"{++contador}- {palabra} \n");
            }
            return resultado.ToString();
        }
    }
}