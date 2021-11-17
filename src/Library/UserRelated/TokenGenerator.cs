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

        int tkn = 0;

        /// <summary>
        /// Metodo para generar el token. verifica si existe en la lista, si existe, intenta genera uno nuevo si no existe lo agrega en la lista de globalRatings 
        /// </summary>
        /// <returns>El token generado.</returns>
        public int GenerateToken()
        {
            return tkn++;
        }
    }
}
