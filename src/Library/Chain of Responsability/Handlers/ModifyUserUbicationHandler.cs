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
    public class ModifyUserUbicationHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de la clase CommandHandler
        /// </summary>
        /// <param name="succesor">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public ModifyUserUbicationHandler(AbstractHandler succesor) : base(succesor) { }

        /// <summary>
        /// Metodo que se encarga de atender el handler.
        /// </summary>
        /// <param name="request">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        protected override bool InternalHandle(Message request, out string response)
        {
            UserInfo user = SessionRelated.Instance.GetUserById(request.UserId);
            if (user.HandlerState == Bot.State.ChangingUserUbication)
            {
                if (request.Text.Equals("1"))
                {
                    user.HandlerState = Bot.State.ChangingUserCity;
                    response = "En que ciudad vives ahora?\nEnvia \"/cancelar\" para cancelar la operación";
                    return true;
                }
                else if (request.Text.Equals("2"))
                {
                    user.HandlerState = Bot.State.ChangingUserAddress;
                    response = "En que ciudad vives ahora?\nEnvia \"/cancelar\" para cancelar la operación";
                    return true;
                }
                else
                {
                    // TODO Capaz puedo meter exception
                    response = "Esta opcion no esta disponible. \nPor favor, dinos una opcion del 1 al 2.\nEnvia \"/cancelar\" para cancelar la operación";
                    return true;
                }
            }
            else if (user.HandlerState == Bot.State.ChangingUserAddress)
            {
                ((RoleEntrepreneur)user.UserRole).Location.Address = request.Text;
                response = "Informacion actualizada. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación";
                user.HandlerState = Bot.State.AskingDataNumber;
                return true;
            }
            else if (user.HandlerState == Bot.State.ChangingUserCity)
            {
                ((RoleEntrepreneur)user.UserRole).Location.City = request.Text;
                response = "Informacion actualizada. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación";
                user.HandlerState = Bot.State.AskingDataNumber;
                return true;
            }

            response = string.Empty;
            return false;
        }
    }
}