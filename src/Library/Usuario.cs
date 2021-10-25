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
        public string Telefono { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// se registra al usuario
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="nombre"></param>
        /// <param name="direccion"></param>
        /// <param name="telefono"></param>
        /// <param name="password"></param>
        public Usuario(string ci, string nombre, string direccion, string telefono, string password)
        {
            if (Utils.IdIsValid(ci))
            {
                this.Ci = ci;
            }
            this.Ci = ci;
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.Telefono = telefono;
            this.Password = password;
        }
        public void cambiarNombre(string nuevoNombre)
        {
            this.Nombre = nuevoNombre;
        }
        public void cambiarPassword(string newPassword)
        {
            this.Password = newPassword;
        }
    }
}

