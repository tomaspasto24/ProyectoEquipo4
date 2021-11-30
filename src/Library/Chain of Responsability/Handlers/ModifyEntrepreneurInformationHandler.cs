using System;
using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Handler que se encarga de comenzar el proceso de modificación de información de un emprendedor
    /// Patrones y principios:
    /// Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP, este motivo de cambio podría ser, cambiar el método InternalHandle.
    /// También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    /// Cumple con Polymorphism porque usa el método polimórfico InternalHandle. 
    /// A su vez, cumple con el patrón Chain of Responsability.
    /// </summary>
    public class ModifyEntrepreneurInformationHandler : AbstractHandler
    {
        /// <summary>
        /// Crea una nueva instancia de éste handler y define su sucesor.
        /// </summary>
        /// <param name="succesor">El siguiente handler a ser invocado en caso de que el actual no cumpla la condición.</param>
        public ModifyEntrepreneurInformationHandler(AbstractHandler succesor) 
        : base(succesor)
        {
        }

        /// <summary>
        /// Intenta procesar el mensaje recibido y devuelve una respuesta.
        /// </summary>
        /// <param name="request">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
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