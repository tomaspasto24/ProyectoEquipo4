namespace Bot
{
    /// <summary>
    /// Clase abstracta que debe implementar cualquier clase de bot para ser utilizada en esta aplicacion.
    /// Patrones y principios:
    /// Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP.
    /// También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    /// </summary>
    public abstract class AbstractBot
    {
        /// <summary>
        /// Comienza la comunicacion entre el bot y los usuarios.
        /// </summary>
        public abstract void StartCommunication();

        /// <summary>
        /// Envia un mensaje al usuario con el bot como emisor.
        /// </summary>
        /// <param name="id">Id del usuario destinatario.</param>
        /// <param name="text">Mensaje a enviar.</param>
        public abstract void SendMessage(long id, string text);
    }
}