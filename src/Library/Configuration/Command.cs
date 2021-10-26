using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Clase Command que 
    /// </summary>
    public class Command
    {
        public List<string> CommandsList {get; set;}

        /// <summary>
        /// Constructor de la clase Command. Asigna a la lista de comandos, los comandos basicos que puede tener un usuario.
        /// </summary>
        public Commands()
        {
            this.CommandsList = new List<string>()
            {
                "/comandos",
                "/registro",
                "/iniciarsesion",
                "/cerrarsesion",
            };
        }

        /// <summary>
        /// Metodo para retornar la lista de comandos segun que usuario la pida.
        /// </summary>
        /// <param name="userId">Id del usuario que pide la lista de comandos</param>
        /// <returns>Lista de comandos</returns>
        public string ReturnCommands(string userId)
        {
            string commandList = string.Empty;
            foreach (string command in this.CommandsList)
            {
                commandList = commandList + command + "\n";
            }
            // if (!registrado)
            // {

            // }
            // else if (!logeado)
            // {

            // }
            // else
            // {

            // }
            // commandList va a ser igual a una string que cambia segun el estado actual del usuario (sin registro, sin logeo, logeado etc)
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