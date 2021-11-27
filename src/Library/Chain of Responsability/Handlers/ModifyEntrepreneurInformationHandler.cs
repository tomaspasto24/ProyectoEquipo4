using System;
using System.Collections.Generic;

namespace Bot
{
    /*
    Patrones y principios:
    También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.    
    A su vez, cumple con el patrón Chain of Responsability.
    */
    /// <summary>
    /// Handler que se encarga del registro de un usuario
    /// </summary>
    public class ModifyEntrepreneurInformationHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de la clase RegisterHandler
        /// </summary>
        /// <param name="succesor">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public ModifyEntrepreneurInformationHandler(AbstractHandler succesor) : base(succesor)
        {
        }

        /// <summary>
        /// Metodo que se encarga de atender el handler.
        /// </summary>
        /// <param name="request">Mensaje que contiene el texto y el id del usuario.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        protected override bool InternalHandle(Message request, out string response)
        {
            UserInfo user = SessionRelated.Instance.GetUserById(request.UserId);
            
            if (!user.HasPermission(Permission.Data))
            {
                response = string.Empty;
                return false;
            }

            if (user.HandlerState == Bot.State.Start && request.Text.Equals("/modificardatos"))
            {
                user.HandlerState = Bot.State.AskingDataNumber;
                response = "Por favor, dinos que campo quieres modificar:\n1 - Ubicacion \n2 - Rubro"
                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación";
                return true;
            }
            if (user.HandlerState == Bot.State.AskingDataNumber)
            {
                if (request.Text.Equals("1"))
                {
                    user.HandlerState = Bot.State.ChangingUserUbication;
                    response = "Qué campo quieres modificar? \n1 - Ciudad\n2 - Direccion \nEnvia \"/cancelar\" para cancelar la operación";
                    return true;
                }
                else if (request.Text.Equals("2"))
                {
                    user.HandlerState = Bot.State.ChangingUserHeader;
                    response = "Cual es tu nuevo rubro?\nEnvia \"/cancelar\" para cancelar la operación";
                    return true;
                }
                else if (request.Text.Equals("3"))
                {
                    user.HandlerState = Bot.State.ChangingUserSpecializations;
                    response = "Que desesa realizar? \n1 - Agregar una especialidad \n2 - Borrar una especialidad\nEnvia \"/cancelar\" para cancelar la operación";
                    return true;
                }
                else if (request.Text.Equals("4"))
                {
                    user.HandlerState = Bot.State.ChangingUserCertifications;
                    response = "Que desea realizar? \n1 - Agregar una certificacion \n2 - Borrar una certificacion";
                    return true;
                }
                else
                {
                    response = "Esta opcion no esta disponible. \nPor favor, dinos una opcion del 1 al 4.\nEnvia \"/cancelar\" para cancelar la operación";
                    return true;
                }
            }

            response = string.Empty;
            return false;
        }
    }
}