using System.Text.Json;

namespace Bot
{
    /// <summary>
    /// Clase TokenGenerator.
    /// </summary>
    public class TokenGenerator : IJsonConvertible
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

        public int tkn = 0;

        /// <summary>
        /// Metodo para generar el token. verifica si existe en la lista, si existe, intenta genera uno nuevo si no existe lo agrega en la lista de globalRatings 
        /// </summary>
        /// <returns>El token generado.</returns>
        public int GenerateToken()
        {
            return tkn++;
        }

        /// <summary>
        /// Método para devolver a la clase encarga de serializar, la serialización del atributo tkn
        /// </summary>
        /// <returns></returns>
        public string ConvertObjectToSaveToJson()
        {
            JsonSerializerOptions options = new () 
            {
                ReferenceHandler = MyReferenceHandler.Instance,
                WriteIndented = true,
            };
            return JsonSerializer.Serialize(this, options);
        }
    }
}
