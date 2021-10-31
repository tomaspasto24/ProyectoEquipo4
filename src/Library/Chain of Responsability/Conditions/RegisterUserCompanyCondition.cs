namespace Bot
{
    /// <summary>
    /// Clase RegisterUserCompanyCondition que se encarga de servir como condición del RegisterUserCompanyHandler.
    /// </summary>
    public class RegisterUserCompanyCondition : ICondition
    {
        /// <summary>
        /// Condición servida a RegisterUserCompanyHandler.
        /// </summary>
        /// <param name="condition">Clase Message</param>
        /// <returns><c>True</c> en caso de que el comando sea el ingresado, <c>False</c> en caso contrario.</returns>
        public bool VerifyCondition(Message condition)
        {
            return condition.Text == "/tengoinvitacion";
        }
    }
}