using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Clase Command que 
    /// </summary>
    public class Command
    {
        public List<string> CommandsList { get; set; }


        /// <summary>
        /// Constructor de la clase Command. Asigna a la lista de comandos, los comandos basicos que puede tener un usuario.
        /// </summary>
        public Command()
        {
            this.CommandsList = new List<string>()
            {
                "/comandos",
                "/registro",
                "/hola",
                "/chau",
                "/adios",
                "exit",
                "/busqueda",
                "/reporte",
                "/contacto",
                "/publicar",
                "/generartoken",
                "/infoemprendedor"
            };
        }

        public List<string> EntrepreneurList()
        {
            List<string> list = new List<string>()
            {
                "/comandos",
                "/registro",
                "/hola",
                "/chau",
                "/adios",
                "exit",
                "/busqueda",
                "/reporte",
                "/contacto",
                "/infoemprendedor"
            };
            return list;
        }

        public List<string> CompanyUserList()
        {
            List<string> list = new List<string>()
            {
                "/comandos",
                "/hola",
                "/chau",
                "/adios",
                "exit",
                "/reporte",
                "/publicar"
            };
            return list;
        }

        public List<string> AdminList()
        {
            List<string> list = new List<string>()
            {
                "/comandos",
                "/hola",
                "/chau",
                "/adios",
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
        public string ReturnCommands(string userId)
        {
            string commandList = string.Empty;
            UserRelated userRelated = SessionRelated.Instance.ReturnInfo(userId);
            if (userRelated.User.Role is RoleEntrepreneur)
            {
                foreach (string command in EntrepreneurList())
                {
                    commandList = commandList + "\n";
                }
            }
            else if (userRelated.User.Role is RoleUserCompany)
            {
                foreach (string command in CompanyUserList())
                {
                    commandList = commandList + "\n";
                }
            }
            else
            {
                foreach (string command in AdminList())
                {
                    commandList = commandList + "\n";
                }
            }
            return commandList;
        }

        /// <summary>
        /// Metodo para verificar si el comando pasado como parametro existe en la lista de comandos.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Si la lista contiene el comando buscado</returns>
        public bool ExistingCommand(string command)
        {
            return this.CommandsList.Contains(command.ToLower());
        }
    }
}