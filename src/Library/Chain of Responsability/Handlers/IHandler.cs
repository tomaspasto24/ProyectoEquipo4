namespace Bot
{
    /// <summary>
    /// Interfaz para ser utilizada en cada uno de los handlers.
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// El siguiente handler en caso de que el actual no cumpla la condicion.
        /// </summary>
        /// <value>Siguiente handler.</value>
        IHandler Succesor { get; set; }

        /// <summary>
        /// Procesa el mensaje o la pasa al siguiente handler si está definido o no es nulo.
        /// </summary>
        /// <param name="request">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje.</param>
        /// <returns>Este handler si fue capaz de procesar el mensaje, en caso contrario el Succesor.</returns>
        IHandler Handle(Message request, out string response);

        /// <summary>
        /// Retorna este handler al estado inicial y cancela el próximo handler si existe. 
        /// Utilizado para que los handlers que procesan varios mensajes cambiando de estado 
        /// entre mensajes puedan volver al estado inicial en caso de error por ejemplo.
        /// </summary>
        void Cancel();
    }
}