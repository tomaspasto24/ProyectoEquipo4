namespace Bot
{
    /// <summary>
    /// Interfaz pública que define el tipo de los objetos que pueden ser serializados y deserializados.
    /// </summary>
    public interface IJsonConvertible
    {
        /// <summary>
        /// Método que convierte el objeto a formato JSON en cadena de caraceteres y lo escribe en el archivo JSON.
        /// </summary>
        void ConvertToJson();
        
        /// <summary>
        /// Método que carga el archivo JSON y lo inicializa.
        /// </summary>
        void LoadFromJson();
    }
}