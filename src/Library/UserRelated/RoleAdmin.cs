using System;
using System.Collections.Generic;
namespace Bot
{
    /// <summary>
    /// clase RoleAdmin que hereda de la clase Role
    /// </summary>
    public class RoleAdmin : Role
    {
        /// <summary>
        /// se inicializa la lista globalRatingsList
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <returns></returns>
        public static List<string> globalRatingsList = new List<string>();

        public RoleAdmin(String name, int id) : base(name, id)
        {
        }

        /// <summary>
        /// metodo para generar el token. verifica si existe en la lista, si existe, intenta genera uno nuevo si no existe lo agrega en la lista de globalRatings 
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public String GenerateToken(Company company)
        {
            String newToken = CodeGeneratortoUserCompany(globalRatingsList);
            SessionRelated.DiccUserTokens.Add(newToken, company);
            return newToken;

        }

        /// <summary>
        /// metodo estatico para generar un string alfanumerico unico para una lista dada.
        /// </summary>
        /// <param name="company"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private static string CodeGeneratortoUserCompany(List<string> tokens)
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var Charsarr = new char[8];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            var resultString = new String(Charsarr);

            if (tokens.Contains(resultString))
            {
                return CodeGeneratortoUserCompany(tokens);
            }
            else
            {
                tokens.Add(resultString);
                return resultString;
            }
        }
        /// <summary>
        /// metodo para agregar las habilitaciones a la lista "globalRatingsList" 
        /// </summary>
        /// <param name="rating"></param>
        /// 
        /// 
       /* public void AddRating(string rating)
        {
            globalRatingsList.Add(rating);
        }*/


        /// <summary>
        /// metodo para eliminar las habilitaciones a la lista "globalRatingsList" 
        /// </summary>
        /// <param name="rating"></param>
        public void DeleteRating(string rating)
        {
            globalRatingsList.Remove(rating);
        }
    }
}