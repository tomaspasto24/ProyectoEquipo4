using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Clase encargada de guardar todos los comandos existentes en el ChatBot y sus respectivos permisos necesarios para utilizar a cada uno de ellos.
    /// Patrones y principios:
    /// Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP. Éste motivo podría ser, cambiar la forma de
    /// almacenar los comandos.
    /// También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase, la cuál es el
    /// almacenamiento de los comandos con sus respectivas descripciones y permisos.
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
            {"/crearinvitacion - Genera una nueva invitación para una Empresa.", Permission.GenerateToken},
            {"/emprender - Registrate como un Emprendedor.", Permission.Undertake},
            {"/agregarmaterial - Agregar un material a una publicación existente.", Permission.AddMaterial}
        };

        /// <summary>
        /// Obtiene la lista de comandos visibles para un usuario dado
        /// </summary>
        /// <param name="userId">Id del usuario en cuestión</param>
        /// <returns>Lista de comandos que este usuario puede utilizar</returns>
        public static string GetCommands(long userId)
        {
            UserInfo user = SessionRelated.Instance.GetUserById(userId);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (string cmd in commands.Keys)
            {
                if (user.HasPermission(commands[cmd]))
                {
                    sb.Append(cmd).Append("\n");
                }
            }

            return sb.ToString();
        }
    }
}