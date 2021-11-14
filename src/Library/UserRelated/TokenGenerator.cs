using System;
using System.Collections.Generic;
namespace Bot
{
    /// <summary>
    /// Clase TokenGenerator.
    /// </summary>
    public class TokenGenerator
    {
        private static TokenGenerator instance;

        public static TokenGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TokenGenerator();
                }
                return instance;
            }
        }
        private TokenGenerator()
        {
        }
        /// <summary>
        /// Se inicializa la lista tokenList.
        /// </summary>
        /// <typeparam name="int"></typeparam>
        /// <returns></returns>
        public static List<int> tokenList = new List<int>();

        /// <summary>
        /// Metodo para generar el token. verifica si existe en la lista, si existe, intenta genera uno nuevo si no existe lo agrega en la lista de globalRatings 
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public int GenerateToken(Company company)
        {
            int token = 0;
            foreach (int i in tokenList)
            {
                token = token + 1;
            }
            tokenList.Add(token);
            return token;
        }

        // /// <summary>
        // /// Metodo estatico para generar un token unico para una lista dada.
        // /// </summary>
        // /// <param name="token"></param>
        // /// <returns></returns>
        // private static int GenerateTokenToUserCompany(List<int> tokenList)
        // {
        //     var token = 0;

        //     if (tokenList.Contains(token))
        //     {
        //         token = +1;
        //         return GenerateTokenToUserCompany(tokenList);
        //     }
        //     else
        //     {
        //         tokenList.Add(token);
        //         return token;
        //     }
        // }
    }
}