using System;
using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Clase RoleAdmin que implementa la interfaz IRole
    /// </summary>
    public class AdminInfo
    {
        // TODO Resolver donde guardar estas qualifications

        /// <summary>
        /// Se inicializa la lista globalQualificationList
        /// </summary>
        /// <returns></returns>
        public static IList<string> globalQualificationList = new List<string>();

        /// <summary>
        /// Metodo para generar el token. verifica si existe en la lista, si existe, intenta genera uno nuevo si no existe lo agrega en la lista de globalRatings 
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public string GenerateToken(Company company)
        {
            int newToken = TokenGenerator.Instance.GenerateToken();
            SessionRelated.Instance.DiccUserTokens.Add(newToken.ToString(), company);
            return newToken.ToString();
        }
    }
}