namespace Bot
{
    /// <summary>
    /// Clase encargada de almacenar el mensaje y el id de un usuario.
    /// Patrones y principios:
    /// Debido a que se identifica una sola razón de cambio, esta clase cumple con SRP, esta razón podría ser
    /// cambiar la forma que se almacena el mensaje y el id.
    /// También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase
    /// la cual es almacenar un mensaje con su id respectivo.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Crea una nueva instancia de la clase Message, asignando el mensaje y el id del usuario.
        /// </summary>
        /// <param name="id">Id del usuario en cuestión.</param>
        /// <param name="message">Mensaje del usuario en cuestión.</param>
        public Message(long id, string message)
        {
            this.UserId = id;
            this.Text = message;
        }

        /// <summary>
        /// Id del usuario.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Mensaje del usuario.
        /// </summary>
        public string Text { get; set; }
    }
}