using System.Collections.Generic;

namespace Bot
{
    /*
    Patrones y principios:
    Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP, este motivo de cambio podría ser, cambiar el método InternalHandle.
    También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.    
    A su vez, cumple con el patrón Chain of Responsability.
    */
    /// <summary>
    /// Handler para mostrar los comandos que el usuario tiene acceso
    /// </summary>
    public class CommandHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de la clase CommandHandler
        /// </summary>
        /// <param name="succesor">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public CommandHandler(AbstractHandler succesor) : base(succesor) { }

        /// <summary>
        /// Metodo que retorna una lista de comandos para el emprendedor
        /// </summary>
        /// <returns>Lista de comandos</returns>
        public List<string> EntrepreneurList()
        {
            List<string> list = new List<string>()
            {
                "/comandos",
                "/registro",
                "/hola",
                "exit",
                "/busqueda",
                "/reporte",
                "/contacto",
                "/infoemprendedor"
            };
            return list;
        }

        /// <summary>
        /// Metodo que retorna una lista de comandos para el usuario empresa
        /// </summary>
        /// <returns>Lista de comandos</returns>
        public List<string> CompanyUserList()
        {
            List<string> list = new List<string>()
            {
                "/comandos",
                "/hola",
                "exit",
                "/reporte",
                "/publicar"
            };
            return list;
        }

        /// <summary>
        /// Metodo que retorna una lista de comnados para el admin
        /// </summary>
        /// <returns>Lista de comandos</returns>
        public List<string> AdminList()
        {
            List<string> list = new List<string>()
            {
                "/comandos",
                "/hola",
                "exit",
                "/generartoken"
            };
            return list;
        }

        /// <summary>
        /// Metodo que se encarga de atender el handler.
        /// </summary>
        /// <param name="request">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        protected override bool InternalHandle(Message request, out string response)
        {
            Command commands = new Command();
            UserInfo user = SessionRelated.Instance.GetUserById(request.UserId);

            if (request.Text.ToLower().Equals("/comandos"))
            {
                string commandList = string.Empty;
                if (user.UserRole is RoleEntrepreneur)
                {
                    foreach (string command in EntrepreneurList())
                    {
                        commandList = commandList + command + "\n";
                    }
                }
                else if (user.UserRole is RoleUserCompany)
                {
                    foreach (string command in CompanyUserList())
                    {
                        commandList = commandList + command + "\n";
                    }
                }
                else
                {
                    foreach (string command in AdminList())
                    {
                        commandList = commandList + command + "\n";
                    }
                }
                
                response = $"Estos son todos los comandos: \n" + commandList;
                return true;
            }

            response = string.Empty;
            return false;
        }
    }
}