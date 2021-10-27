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
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>

        public Admin(string username, string password) : base(username, password)
        {
        }

        public void Generator()
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var Charsarr = new char[8];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            var resultString = new String(Charsarr);
            Console.WriteLine(resultString);
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

