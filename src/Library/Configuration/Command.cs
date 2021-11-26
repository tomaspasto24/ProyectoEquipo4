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
            {"/busqueda - Buscar.", Permission.Search},
            {"/reporte - Obtener un reporte de las compras realizadas en los últimos 30 días.", Permission.PurchasesReport},
            {"/reporte - Obtener un reporte de las entregas realizadas en los últimos 30 días.", Permission.SalesReport},
            {"/contacto - Obtener el contacto de una Empresa.", Permission.ContactCompany},
            {"/datos - Gestionar los datos del Usuario.", Permission.Data},
            {"/modificardatos - Modifica los datos del Usuario.", Permission.Data},
            {"/publicar - Crear una nueva publicación con un material o varios.", Permission.Publish},
            {"/crearinvitacion - Genera un nueva nueva invitación para una Empresa.", Permission.GenerateToken},
            {"/emprender - Registrate como un Emprendedor", Permission.Undertake},
            {"/agregarmaterial - Agregar un material a una publicación existente", Permission.AddMaterial}
        };

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