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
        private static Dictionary<string, Permission> commands = new Dictionary<string, Permission>(){
            {"/comandos - Muestra la lista de comandos.", Permission.None},
            {"/hola - Saluda al bot.", Permission.None},
            {"/registro - Registrate como usuario de una empresa.", Permission.Register},
            {"/busqueda - Buscar materiales.", Permission.MaterialSearch},
            {"/reporte - Obtener un reporte de las compras realizadas en los últimos 30 días.", Permission.PurchasesReport},
            {"/reporte - Obtener un reporte de las entregas realizadas en los últimos 30 días.", Permission.SalesReport},
            {"/contacto - Obtener el contacto de una Empresa.", Permission.ContactCompany},
            {"/datos - Gestionar los datos del Usuario.", Permission.Data},
            {"/modificardatos - Modifica los datos del Usuario.", Permission.Data},
            {"/publicar - Crear una nueva publicación con un material existente.", Permission.Publish},
            {"/crearinvitacion - Genera un nueva nueva invitación para una Empresa.", Permission.GenerateToken},
            {"/emprender - Registrate como un Emprendedor", Permission.Undertake},

        };

        // /// <summary>
        // /// Lista de comandos que un entrepeneur puede ejecutar.
        // /// </summary>
        // /// <typeparam name="string"></typeparam>
        // /// <returns></returns>
        // private static List<string> entrepeneurCommandList = new List<string>()
        // {
        //         "/comandos - Muestra la lista de comandos.",
        //         "/registro - Registrate como un Usuario de una Empresa.",
        //         "/hola - Saluda al bot.",
        //         "/busqueda - Buscar materiales.",
        //         "/reporte - Obtener un reporte de las compras realizadas en los últimos 30 días.",
        //         "/contacto - Obtener el contacto de una Empresa.",
        //         "/datos - Gestionar los datos del Usuario.",
        //         "/modificardatos - Modifica los datos del Usuario."
        // };

        // private static List<string> companyUserCommandList = new List<string>()
        // {
        //         "/comandos - Muestra la lista de comandos.",
        //         "/hola - Saluda al bot.",
        //         "/reporte - Obtener un reporte de las entregas realizadas en los últimos 30 días.",
        //         "/publicar - Crear una nueva publicación con un material existente."
        //     // TODO crear el material y despues si, usar el publicar (sin material no puedo crear una publicacion) => Crear /materiales
        // };

        // private static List<string> adminCommandList = new List<string>()
        // {
        //         "/comandos - Muestra la lista de comandos.",
        //         "/hola - Saluda al bot.",
        //         "/crearinvitacion - Genera un nueva nueva invitación para una Empresa."
        // };


        // private static List<string> defaultCommandList = new List<string>()
        // {
        //         "/comandos - Muestra la lista de comandos.",
        //         "/hola - Saluda al bot.",
        //         "/emprender - Registrate como un Emprendedor",
        //         "/registro - Registrate como un Usuario de una Empresa.",

        // };


        // /// <summary>
        // /// Metodo para retornar la lista de comandos segun que usuario la pida.
        // /// </summary>
        // /// <param name="userId">Id del usuario que pide la lista de comandos</param>
        // /// /// <returns>Lista de comandos</returns>
        // public static string ReturnCommands(long userId)
        // {
        //     UserInfo user = SessionRelated.Instance.GetUserById(userId);
        //     System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //     if (user.UserRole is RoleEntrepreneur)
        //     {
        //         foreach (string command in entrepeneurCommandList)
        //         {
        //             sb.Append(command).Append("\n");
        //         }
        //     }
        //     else if (user.UserRole is RoleUserCompany)
        //     {
        //         foreach (string command in companyUserCommandList)
        //         {
        //             sb.Append(command).Append("\n");
        //         }
        //     }
        //     else if (user.UserRole is RoleAdmin)
        //     {
        //         foreach (string command in adminCommandList)
        //         {
        //             sb.Append(command).Append("\n");
        //         }
        //     }
        //     else
        //     {
        //         foreach (string command in defaultCommandList)
        //         {
        //             sb.Append(command).Append("\n");
        //         }
        //     }
        //     return sb.ToString();
        // }

        /// <summary>
        /// Metodo para retornar la lista de comandos segun que usuario la pida.
        /// </summary>
        /// <param name="userId">Id del usuario que pide la lista de comandos</param>
        /// /// <returns>Lista de comandos</returns>
        public static string GetCommands(long userId)
        {
            IRole role = SessionRelated.Instance.GetUserById(userId).UserRole;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (string cmd in commands.Keys)
            {
                if (role.HasPermission(commands[cmd]))
                {
                    sb.Append(cmd).Append("\n");
                }
            }

            return sb.ToString();
        }
    }
}