using System;
namespace Bot
{

    /// <summary>
    /// clase que representa al Usuario en el sistema
    /// </summary>
    public class Usuario
    {
        public string Ci { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }

        /// <summary>
        /// se registra al usuario
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="nombre"></param>
        /// <param name="direccion"></param>
        /// <param name="telefono"></param>
        public Usuario(string ci, string nombre, string direccion, int telefono)
        {
            if (Utils.IdIsValid(ci))
            {
                this.Ci = ci;
            }

            this.Nombre = nombre;
            this.Direccion = direccion;
            this.Telefono = telefono;
        }



    }
}

