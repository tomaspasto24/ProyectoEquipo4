using System.Collections.Generic;

namespace Bot
{
    public class Commands
    {
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
        public string ReturnComands(string userId)
        {
            string commandList = string.Empty;
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
        public List<string> CommandsList {get; set;}
        public bool ExistingCommand(string command)
        {
            return this.CommandsList.Contains(command.ToLower());
        }
    }
}