using System.Collections.Generic;

namespace Bot
{
    /*
    Patrones y principios:
    Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP.
    También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    */
    /// <summary>
    /// Clase Command que se ocupa de guardar las listas de comandos segun el rol del usuario
    /// </summary>
    public class Command
    {
        //TODO Colocar explicacion para cada comando breve.
        
        /// <summary>
        /// Lista de comandos que un entrepeneur puede ejecutar.
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <returns></returns>
        private static List<string> entrepeneurCommandList = new List<string>()
        {
            "/comandos",
            "/registro",
            "/hola",
            "/exit",
            "/busqueda",
            "/reporte",
            "/contacto",
            "/infoemprendedor",
            // TODO habilitaciones cuales tiene
        };
        
        private static List<string> companyUserCommandList = new List<string>()
        {
            "/comandos",
            "/hola",
            "/exit",
            "/reporte",
            "/publicar"
            // TODO crear el material y despues si, usar el publicar (sin material no puedo crear una publicacion) => Crear /materiales
        };

        private static List<string> adminCommandList = new List<string>()
        {
            "/comandos",
            "/hola",
            "/exit",
            "/generartoken"
        };


        private static List<string> defaultCommandList = new List<string>()
        {
            "/comandos",
            "/hola",
            "/exit",
            "/emprender"
        };


        /// <summary>
        /// Metodo para retornar la lista de comandos segun que usuario la pida.
        /// </summary>
        /// <param name="userId">Id del usuario que pide la lista de comandos</param>
        /// /// <returns>Lista de comandos</returns>
        public static string ReturnCommands(long userId)
        {
            UserInfo user = SessionRelated.Instance.GetUserById(userId);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (user.UserRole is RoleEntrepreneur)
            {
                foreach (string command in entrepeneurCommandList)
                {
                    sb.Append(command).Append("\n");
                }
            }
            else if (user.UserRole is RoleUserCompany)
            {
                foreach (string command in companyUserCommandList)
                {
                    sb.Append(command).Append("\n");
                }
            }
            else if (user.UserRole is RoleAdmin)
            {
                foreach (string command in adminCommandList)
                {
                    sb.Append(command).Append("\n");
                }
            }
            else
            {
                foreach (string command in defaultCommandList)
                {
                    sb.Append(command).Append("\n");
                }
            }
            return sb.ToString();
        }
    }
}