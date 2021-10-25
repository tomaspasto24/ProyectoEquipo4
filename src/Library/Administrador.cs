using System;
namespace Bot
{

    /// <summary>
    /// clase que representa al administrador en el sistema y hereda de usuario
    /// </summary>
    public class Administrador : Usuario
    {
        /// <summary>
        /// constructor de la clase administador, el "base" se utiliza para inicializar el constructor de la clase usuario 
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="nombre"></param>
        /// <param name="direccion"></param>
        /// <param name="telefono"></param>
        /// <returns></returns>
        public Administrador(string ci, string nombre, string direccion, string telefono, string password) : base(ci, nombre, direccion, telefono, password)
        {
        }
        /// <summary>
        /// metodo para generar invitación
        /// </summary>
        /// <returns></returns>
        public string GenerarInvitacion()  //puede retornar una url o lo que sea 

        {
            return "";
        }
    }
}

