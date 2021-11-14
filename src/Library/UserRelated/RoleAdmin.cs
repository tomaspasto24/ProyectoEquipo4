using System;
using System.Collections.Generic;
namespace Bot
{
    /// <summary>
    /// Clase RoleAdmin que hereda de la clase Role
    /// </summary>
    public class RoleAdmin : Role
    {
        /// <summary>
        /// Se inicializa la lista globalQualificationList
        /// </summary>
        /// <returns></returns>
        public static List<string> globalQualificationList = new List<string>();

        /// <summary>
        /// Constructor de la clase RoleAdmin.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleAdmin() : base()
        {
        }

        /// <summary>
        /// Metodo para generar el token. verifica si existe en la lista, si existe, intenta genera uno nuevo si no existe lo agrega en la lista de globalRatings 
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public string GenerateToken(Company company)
        {
            int newToken = TokenGenerator.Instance.GenerateToken(company);
            SessionRelated.Instance.DiccUserTokens.Add(newToken.ToString(), company);
            return newToken.ToString();
        }

        // /// <summary>
        // /// Metodo para eliminar las habilitaciones a la lista "globalRatingsList" 
        // /// </summary>
        // /// <param name="rating"></param>
        // public void DeleteRating(string rating)
        // {
        //     globalQualificationList.Remove(rating);
        // }
    }
}