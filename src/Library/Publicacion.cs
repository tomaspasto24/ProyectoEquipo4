using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    /// <summary>
    /// Cada objeto de la clase Publicación, administrado por un objeto Empresa, es el conjunto de items
    /// que la aplicación muestra a los emprendedores.
    /// </summary>
    public class Publicacion
    {
        private List<Material> listaMateriales = new List<Material>();
        private DateTime fecha;
        private Ubicacion ubicacion;
        private string nombreEmpresa;

        /// <summary>
        /// Constructor de Publicación, instancia la hora del sistema actual en donde se crea y setea nombreEmpresa y ubicacion.
        /// </summary>
        /// <param name="nombreEmpresa">Nombre de la empresa</param>
        /// <param name="ubicacion">Ubicación de la empresa</param>
        public Publicacion(string nombreEmpresa, Ubicacion ubicacion)
        {
            this.nombreEmpresa = nombreEmpresa;
            this.fecha = DateTime.Now;
            this.ubicacion = ubicacion;
        }

        /// <summary>
        /// Método que agrega a material a la publicación.
        /// </summary>
        /// <param name="material">Objeto Material</param>
        public void AgregarMaterial(Material material)
        {
            listaMateriales.Add(material);
        } 

        /// <summary>
        /// El método busca si hay un valor en el indice ingresado como parámetro, en caso de que exista un elemento:
        /// lo elimina y retorna True. De lo contrario solamente retorna False.
        /// </summary>
        /// <param name="indiceMaterial">Indice del Material que se quiera eliminar.
        /// Se obtiene con la función DevolverListaMateriales.</param>
        /// <returns></returns>
        public bool EliminarMaterial(int indiceMaterial)
        {
            return listaMateriales.Remove(listaMateriales[indiceMaterial]);
        }

        /// <summary>
        /// Devuelve un string con todos los materiales enumerados, necesario para poder eliminar un objeto Material.
        /// </summary>
        /// <returns>String con todo los materiales enumerados</returns>
        public string DevolverListaMateriales()
        {
            StringBuilder resultado = new StringBuilder("Materiales: \n");
            int contador = 0;

            foreach(Material material in this.listaMateriales)
            {
                resultado.Append($"{++contador}- {material.Nombre} \n");
            }
            return resultado.ToString();
        }
    }
}