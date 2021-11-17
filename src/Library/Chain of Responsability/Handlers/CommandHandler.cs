using System.Collections.Generic;
using System;

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
        public List<string> EntrepreneurCommandList()
        {
            return new List<string>()
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
        }

        /// <summary>
        /// Metodo que retorna una lista de comandos para el usuario empresa
        /// </summary>
        /// <returns>Lista de comandos</returns>
        public List<string> CompanyUserCommandList()
        {
            return new List<string>()
            {
                "/comandos",
                "/hola",
                "/exit",
                "/reporte",
                "/publicar"
                // TODO crear el material y despues si, usar el publicar (sin material no puedo crear una publicacion) => Crear /materiales
            };
        }

        /// <summary>
        /// Metodo que retorna una lista de comnados para el admin
        /// </summary>
        /// <returns>Lista de comandos</returns>
        public List<string> AdminCommandList()
        {
            return new List<string>()
            {
                "/comandos",
                "/hola",
                "/exit",
                "/generartoken"
            };
        }

        /// <summary>
        /// Metodo que retorna una lista de comnados para el admin
        /// </summary>
        /// <returns>Lista de comandos</returns>
        public List<string> DefaultCommandList()
        {
            return new List<string>()
            {
                "/comandos",
                "/hola",
                "/exit",
                "/emprender"
            };
        }

        /// <summary>
        /// Metodo que se encarga de atender el handler.
        /// </summary>
        /// <param name="request">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        protected override bool InternalHandle(Message request, out string response)
        {
            if (request.Text == null)
            {
                throw new NullReferenceException("El mensaje no puede estar vacio, ni ser una imagen o video");
            }

            if (request.Text.ToLower().Equals("/comandos"))
            {
                response = $"Estos son todos los comandos: \n" + Command.ReturnCommands(request.UserId);
                return true;
            }

            response = string.Empty;
            return false;
        }
    }
}