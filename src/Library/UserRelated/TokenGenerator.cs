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

        /// <summary>
        /// Metodo para generar una instancia de token.
        /// </summary>
        /// <value>Instancia del Token. </value>
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

        /// <summary>
        /// Se inicializa la lista tokenList.
        /// </summary>
        /// <typeparam name="int"></typeparam>
        /// <returns>La lista inicializada.</returns>
        public static List<int> tokenList = new List<int>();


        /// <summary>
        /// Metodo para generar el token. verifica si existe en la lista, si existe, intenta genera uno nuevo si no existe lo agrega en la lista de globalRatings 
        /// </summary>
        /// <returns>El token generado.</returns>
        public int GenerateToken()
        {
            int token = 0;
            foreach (int i in tokenList)
            {
                token = token + 1;
            }

            tokenList.Add(token);
            return token;

        }
    }
}
