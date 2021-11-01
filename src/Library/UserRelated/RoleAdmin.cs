using System;
using System.Collections.Generic;
namespace Bot
{
    /// <summary>
    /// clase RoleAdmin que hereda de la clase Role
    /// </summary>
    public class RoleAdmin : Role
    {
        public static List<string> globalRatingsList = new List<string>();

        public RoleAdmin(String name, int id) : base(name, id)
        {
        }
        /// <summary>
        /// metodo estatico para generar una string alfanumerica la cual sera usada como una invitación
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static string CodeGeneratortoUserCompany(Company company)
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
            SessionRelated.DiccUserTokens.Add(resultString, company);
            return resultString;
        }
        /// <summary>
        /// metodo para agregar las habilitaciones a la lista "globalRatingsList" 
        /// </summary>
        /// <param name="rating"></param>
        public void AddRating(string rating)
        {
            globalRatingsList.Add(rating);
        }
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