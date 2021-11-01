using System;
using System.Collections.Generic;
namespace Bot
{

    /// <summary>
    /// clase que representa al administrador en el sistema y hereda de usuario
    /// </summary>
    public class Admin : Role
    {
        /// <summary>
        /// constructor de la clase administador, el "base" se utiliza para inicializar el constructor de la clase usuario
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>

        public Admin(string name, int id) : base(name, id)
        {
        }
        /// <summary>
        /// metodo estatico para poder acceder a el desde otra clase
        /// </summary>
        public static void CodeGenerator()
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var Charsarr = new char[8];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            var resultString = new String(Charsarr);
            List<string> CodeList = new List<string>();

            if (CodeList.Contains(resultString))
            {
                random = new Random();
            }
            else
            {
                CodeList.Add(resultString);
            }
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

        public static List<string> globalRatingsList = new List<string>();

        /// <summary>
        /// metodo para añadir las habilitaciones globales
        /// </summary>
        /// <param name="rating"></param>
        public void AddRatings(string rating)
        {
            globalRatingsList.Add(rating);
        }

        /// <summary>
        /// metodo para eliminar habilitaciones globales
        /// </summary>
        /// <param name="rating"></param>
        public void DelateRatings(string rating)
        {
            globalRatingsList.Remove(rating);
        }
    }
}