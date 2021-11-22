namespace Bot
{
    /// <summary>
    /// Interfaz para los handlers de Chain of Responsibility
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Obtiene el próximo "handler".
        /// </summary>
        /// <value>El "handler" que será invocado si este "handler" no procesa el mensaje.</value>
        IHandler Succesor { get; set; }

        /// <summary>
        /// Procesa el mensaje o la pasa al siguiente "handler" si existe.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>El "handler" que procesó el mensaje si el mensaje fue procesado; null en caso contrario.</returns>
        IHandler Handle(Message message, out string response);

        /// <summary>
        /// Retorna este "handler" al estado inicial y cancela el próximo "handler" si existe. Es utilizado para que los
        /// "handlers" que procesan varios mensajes cambiando de estado entre mensajes puedan volver al estado inicial en
        /// caso de error por ejemplo.
        /// </summary>
        void Cancel();

    }
}