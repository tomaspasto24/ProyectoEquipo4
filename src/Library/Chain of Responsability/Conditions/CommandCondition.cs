namespace Bot
{
    /// <summary>
    /// Condición que deberá ser cumplida para que el CommandHandler se ejecute.
    /// </summary>
    public class CommandCondition : ICondition
    {
        /// <summary>
        /// Valida si el mensaje recibido fue el comando /comandos.
        /// </summary>
        /// <param name="condition">Mensaje que contiene el texto y el id del usuario</param>
        /// <returns>True o false dependiendo si el texto es igual o no</returns>
        public bool VerifyCondition(Message condition)
        {
            return condition.Text == "/comandos";
        }
    }
}