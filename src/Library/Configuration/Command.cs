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
        /// <summary>
        /// Lista de comandos
        /// </summary>
        public List<string> CommandsList { get; set; }

        /// <summary>
        /// Constructor de la clase Command. Asigna a la lista de comandos, todos los comandos disponibles.
        /// </summary>
        public Command()
        {
            this.CommandsList = new List<string>()
            {
                "/comandos",
                "/registro",
                "/hola",
                "exit",
                "/busqueda",
                "/reporte",
                "/contacto",
                "/publicar",
                "/generartoken",
                "/infoemprendedor"
            };
        }

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
        /// Metodo para retornar la lista de comandos segun que usuario la pida.
        /// </summary>
        /// <param name="userId">Id del usuario que pide la lista de comandos</param>
        /// <returns>Lista de comandos</returns>
        public string ReturnCommands(int userId)
        {
            string commandList = string.Empty;
            UserInfo user = SessionRelated.Instance.GetUserById(userId);
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
            return commandList;
        }

        /// <summary>
        /// Metodo para verificar si el comando pasado como parametro existe en la lista de comandos.
        /// </summary>
        /// <param name="command">Comando a verificar</param>
        /// <returns>Si la lista contiene el comando buscado</returns>
        public bool ExistingCommand(string command)
        {
            return this.CommandsList.Contains(command.ToLower());
        }
    }
}