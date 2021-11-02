using System.Collections.Generic;

namespace Bot
{
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
                "/chau",
                "/adios",
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
        /// <param name="command">Comando a verificar</param>
        /// <returns>Si la lista contiene el comando buscado</returns>
        public bool ExistingCommand(string command)
        {
            return this.CommandsList.Contains(command.ToLower());
        }
    }
}