using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    public class Material
    {
        private string nombre;
        private int cantidad;
        private int precio;
        private List<string> palabrasClave = new List<string>();
        private List<string> habilitaciones = new List<string>();

        public Material(string nombre, int cantidad, int precio)
        {
            this.nombre = nombre;
            this.cantidad = cantidad;
            this.precio = precio;   
        }

        public void AgregarPalabraClave(string palabraClave)
        {
            this.palabrasClave.Add(palabraClave);
        }

        /// <summary>
        /// El método busca si hay un valor en el indice ingresado como parámetro, en caso de que exista un elemento:
        /// lo elimina y retorna True. De lo contrario solamente retorna False.
        /// </summary>
        /// <param name="indicePalabraClave">Indice de la palabra clave que se quiera eliminar.
        /// Se obtiene con la función DevolverPalabrasClave.</param>
        /// <returns></returns>
        public bool EliminarPalabraClave(int indicePalabraClave)
        {
            return palabrasClave.Remove(palabrasClave[indicePalabraClave]);
        }

        public string DevolverPalabrasClave()
        {
            StringBuilder resultado = new StringBuilder("Palabras Clave: \n");
            int contador = 0;

            foreach(string palabra in this.palabrasClave)
            {
                resultado.Append($"{++contador}- {palabra} \n");
            }
            return resultado.ToString();
        }

        public void AgregarHabilitacion(string habilitacion)
        {
            habilitaciones.Add(habilitacion);
        }

        public bool EliminarHabilitacion(int indiceHabilitacion)
        {
            return habilitaciones.Remove(palabrasClave[indiceHabilitacion]);
        }

        public string DevolverHabilitaciones()
        {
            StringBuilder resultado = new StringBuilder("Habilitaciones: \n");
            int contador = 0;

            foreach(string palabra in this.habilitaciones)
            {
                resultado.Append($"{++contador}- {palabra} \n");
            }
            return resultado.ToString();
        }
    }
}