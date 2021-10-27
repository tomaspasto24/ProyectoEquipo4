using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    /// <summary>
    /// Cada objeto de la clase Publicación, administrado por un objeto Empresa, es el conjunto de items
    /// que la aplicación muestra a los emprendedores.
    /// </summary>
    public class Publication
    {
        private string title;
        private List<Material> listMaterials = new List<Material>();
        private DateTime date;
        private GeoLocation location;
        private Company Company;
        private bool IsClosed;
        
        /// <summary>
        /// Titulo que representa la publicación. Más que nada para poder retornar una lista
        /// identificando por título.
        /// </summary>
        /// <value>string</value>
        public string Title
        {
            get
            {
                return this.title;
            }
        }

        /// <summary>
        /// Constructor de Publicación, instancia la hora del sistema actual en donde se crea y setea nombreEmpresa y ubicacion.
        /// </summary>
        /// <param name="nombreEmpresa">Nombre de la empresa</param>
        /// <param name="location">Ubicación de la empresa</param>
        public Publication(String title, Company Company, GeoLocation location, Material material)
        {
            this.title = title;
            this.Company = Company;
            this.date = DateTime.Now;
            this.location = location;
            this.IsClosed = false;
            AddMaterial(material);
        }

        /// <summary>
        /// Método que agrega a material a la publicación.
        /// </summary>
        /// <param name="material">Objeto Material</param>
        public void AddMaterial(Material material)
        {
            listMaterials.Add(material);
        } 

        /// <summary>
        /// El método busca si hay un valor en el indice ingresado como parámetro, en caso de que exista un elemento:
        /// lo elimina y retorna True. De lo contrario solamente retorna False.
        /// </summary>
        /// <param name="indiceMaterial">Indice del Material que se quiera eliminar.
        /// Se obtiene con la función DevolverListaMateriales.</param>
        /// <returns></returns>
        public bool DeleteMaterial(int indiceMaterial)
        {
            return listMaterials.Remove(listMaterials[indiceMaterial]);
        }

        /// <summary>
        /// Devuelve un string con todos los materiales enumerados, necesario para poder eliminar un objeto Material.
        /// </summary>
        /// <returns>String con todo los materiales enumerados</returns>
        public string DevolverListaMateriales()
        {
            StringBuilder resultado = new StringBuilder("Materiales: \n");
            int contador = 0;

            foreach(Material material in this.listMaterials)
            {
                resultado.Append($"{++contador}- {material.Nombre} \n");
            }
            return resultado.ToString();
        }

        public void DeletePublication()
        {
            this.IsClosed = true;
        }
    }
}