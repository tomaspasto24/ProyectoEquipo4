using System;
namespace Bot
{

    /// <summary>
    /// clase que representa al administrador en el sistema y hereda de usuario
    /// </summary>
    public class Admin : User
    {
        /// <summary>
        /// constructor de la clase administador, el "base" se utiliza para inicializar el constructor de la clase usuario 
        /// 
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="telephone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Admin(string ci, string name, string location, string telephone, string password) : base(ci, name, location, telephone, password)
        {
        }
        /// <summary>
        /// metodo para generar invitación
        /// </summary>
        /// <returns></returns>
        public string GenerateInvitation()  //puede retornar una url o lo que sea 

        {
            return "";
        }
    }
}

