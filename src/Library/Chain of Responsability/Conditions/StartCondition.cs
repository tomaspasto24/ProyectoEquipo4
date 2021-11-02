namespace Bot
{
    /// <summary>
    /// Condición que deberá ser cumplida para que el StartHandler se ejecute.
    /// </summary>
    public class StartCondition : ICondition
    {
        /// <summary>
        /// Valida si el mensaje recibido fue el comando /hola
        /// </summary>
        /// <param name="condition">Mensaje que contiene el texto y el id del usuario.</param>
        /// <returns>True porque debe estar siempre activo al ser el ultimo de la cadena.</returns>
        public bool VerifyCondition(Message condition)
        {
            return condition.Text == "/hola";
        }
    }
}