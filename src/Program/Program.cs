using System;

namespace Bot
{
    /// <summary>
    /// Programa de consola de demostración.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Programa principal que inicia la comunicacion con los bots.
        /// </summary>
        public static void Main()
        {
            ConsoleBot.Instance.StartCommunication(); // iniciar comunicación por consola

            RoleAdmin role = new RoleAdmin();

            UserInfo user = new UserInfo("seba",123,role);

            IRole userRole = user.UserRole;

            if (userRole is RoleAdmin)
            {
                RoleAdmin admin = (RoleAdmin)userRole;
            }
        }
    }
}
