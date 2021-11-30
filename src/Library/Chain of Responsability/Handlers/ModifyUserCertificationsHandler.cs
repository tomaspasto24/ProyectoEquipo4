using System;
using System.Collections.Generic;

namespace Bot
{
    /// <summary>
    /// Handler que se encarga de modificar la lista de certificaciones de un emprendedor
    /// Patrones y principios:
    /// Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP, este motivo de cambio podría ser, cambiar el método InternalHandle.
    /// También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    /// Cumple con Polymorphism porque usa el método polimórfico InternalHandle. 
    /// A su vez, cumple con el patrón Chain of Responsability.
    /// </summary>
    public class ModifyUserCertificationsHandler : AbstractHandler
    {
        /// <summary>
        /// Crea una nueva instancia de éste handler y define su sucesor.
        /// </summary>
        /// <param name="succesor">El siguiente handler a ser invocado en caso de que el actual no cumpla la condición.</param>
        public ModifyUserCertificationsHandler(AbstractHandler succesor) 
        : base(succesor) { }

        /// <summary>
        /// Intenta procesar el mensaje recibido y devuelve una respuesta.
        /// </summary>
        /// <param name="request">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(Message request, out string response)
        {
            UserInfo user = SessionRelated.Instance.GetUserById(request.UserId);
            EntrepreneurInfo entrepreneurInfo = SessionRelated.Instance.GetEntrepreneurInfoByUserInfo(user);

            if (user.HandlerState == Bot.State.ChangingUserCertifications)
            {
                if (request.Text.Equals("1"))
                {
                    user.HandlerState = Bot.State.AddingUserCertification;
                    response = "Que certificacion quieres agregar?\nEnvia \"/cancelar\" para cancelar la operación";
                    return true;
                }
                else if (request.Text.Equals("2"))
                {
                    user.HandlerState = Bot.State.DeletingUserCertification;
                    response = "Que certificacion quieres eliminar?\nEnvia \"/cancelar\" para cancelar la operación";
                    return true;
                }
                else
                {
                    // TODO Capaz puedo meter exception
                    response = "Esta opcion no esta disponible. \nPor favor, dinos una opcion del 1 al 2.\nEnvia \"/cancelar\" para cancelar la operación";
                    return true;
                }
            }
            else if (user.HandlerState == Bot.State.AddingUserCertification)
            {
                if (entrepreneurInfo.ContainsCertification(request.Text))
                {
                    response = "Esta certificacion ya fue agregada anteriormente.\nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación";
                    user.HandlerState = Bot.State.AskingDataNumber;
                    return true;
                }
                else
                {
                    entrepreneurInfo.AddCertification(request.Text);
                    response = "Certificacion agregada correctamente. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación";
                    user.HandlerState = Bot.State.AskingDataNumber;
                    return true;
                }
            }
            else if (user.HandlerState == Bot.State.DeletingUserCertification)
            {
                if (entrepreneurInfo.ContainsCertification(request.Text))
                {
                    entrepreneurInfo.DeleteCertification(request.Text);
                    response = "Certificacion eliminada correctamente. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación";
                    user.HandlerState = Bot.State.AskingDataNumber;
                    return true;
                }
                else
                {
                    response = "Esta Certificacion no existe. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación";
                    user.HandlerState = Bot.State.AskingDataNumber;
                    return true;
                }
            }

            response = string.Empty;
            return false;
        }
    }
}