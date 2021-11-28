using System;
using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Clase encargada de generar los tokens para las distintas compañías
    /// Patrones y principios:
    /// Cumple con SRP, ya que se identifica una única razón de cambio. Ésta podría ser, cambiar la forma de generar los tokens.
    /// Cumple con Expert, porque posee todo lo necesario para cumplir con la responsabilidad otorgada a la clase, 
    /// la cuál es la generación de tokens.
    /// Cumple con el patrón Singleton, esto lo que hace es que, garantiza que haya una única instancia de la clase y de esta forma se obtiene
    /// un acceso global a esta instancia.
    /// /// </summary>
    public class TokenGenerator
    {
        private static TokenGenerator instance;

        /// <summary>
        /// Obtiene una única instancia de esta clase
        /// </summary>
        /// <value>La única instancia de esta clase.</value>
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
        /// Genera un nuevo token para una compañía.
        /// </summary>
        /// <returns>El token generado.</returns>
        public int GenerateToken()
        {
            int tkn = SessionRelated.Instance.DiccUserTokens.Count;
            return tkn++;
        }
    }
}