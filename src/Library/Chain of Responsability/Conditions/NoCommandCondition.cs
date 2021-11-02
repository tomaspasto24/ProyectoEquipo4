namespace Bot
{
    /// <summary>
    /// Condición que deberá ser cumplida para que el NoCommandHandler se ejecute.
    /// </summary>
    public class NoCommandCondition : ICondition
    {
        /// <summary>
        /// Siempre valida la condicion, de esta forma siempre se ejecuta el ultimo handler de la cadena.
        /// </summary>
        /// <param name="condition">Mensaje que contiene el texto y el id del usuario.</param>
        /// <returns>True porque debe estar siempre activo al ser el ultimo de la cadena.</returns>
        public bool VerifyCondition(Message condition)
        {
            return true;
        }
    }
}