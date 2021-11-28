using System.Collections.Generic;
using System;

namespace Bot
{
    /// <summary>
    /// Handler que se encarga de modificar la lista de especializaciones de un emprendedor
    /// Patrones y principios:
    /// Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP, este motivo de cambio podría ser, cambiar el método InternalHandle.
    /// También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    /// Cumple con Polymorphism porque usa el método polimórfico InternalHandle. 
    /// A su vez, cumple con el patrón Chain of Responsability.
    /// </summary>
    public class ModifyUserSpecializationsHandler : AbstractHandler
    {
        /// <summary>
        /// Crea una nueva instancia de éste handler y define su sucesor.
        /// </summary>
        /// <param name="succesor">El siguiente handler a ser invocado en caso de que el actual no cumpla la condición.</param>
        public ModifyUserSpecializationsHandler(AbstractHandler succesor) : base(succesor) { }

        /// <summary>
        /// Intenta procesar el mensaje recibido y devuelve una respuesta.
        /// </summary>
        /// <param name="request">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario</returns>
        protected override bool InternalHandle(Message request, out string response)
        {
            UserInfo user = SessionRelated.Instance.GetUserById(request.UserId);
            EntrepreneurInfo entrepreneurInfo = SessionRelated.Instance.GetEntrepreneurInfoByUserInfo(user);

            if (user.HandlerState == Bot.State.ChangingUserSpecializations)
            {
                if (request.Text.Equals("1"))
                {
                    user.HandlerState = Bot.State.AddingUserSpecializations;
                    response = "Que especialidad quieres agregar?\nEnvia \"/cancelar\" para cancelar la operación";
                    return true;
                }
                else if (request.Text.Equals("2"))
                {
                    user.HandlerState = Bot.State.DeletingUserSpecializations;
                    response = "Que especialidad quieres eliminar?\nEnvia \"/cancelar\" para cancelar la operación";
                    return true;
                }
                else
                {
                    // TODO Capaz puedo meter exception
                    response = "Esta opcion no esta disponible. \nPor favor, dinos una opcion del 1 al 2.\nEnvia \"/cancelar\" para cancelar la operación";
                    return true;
                }
            }
            else if (user.HandlerState == Bot.State.AddingUserSpecializations)
            {
                if (entrepreneurInfo.ContainsSpecialization(request.Text))
                {
                    response = "Esta especialidad ya fue agregada anteriormente.\nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación";
                    user.HandlerState = Bot.State.AskingDataNumber;
                    return true;
                }
                else
                {
                    entrepreneurInfo.AddSpecialization(request.Text);
                    response = "Especialidad agregada correctamente. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación";
                    user.HandlerState = Bot.State.AskingDataNumber;
                    return true;
                }
            }
            else if (user.HandlerState == Bot.State.DeletingUserSpecializations)
            {
                if (entrepreneurInfo.ContainsSpecialization(request.Text))
                {
                    entrepreneurInfo.DeleteSpecialization(request.Text);
                    response = "Especialidad eliminada correctamente. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
                                + "\n 3 - Especialidades \n 4 - Certificaciones \nEnvia \"/cancelar\" para cancelar la operación";
                    user.HandlerState = Bot.State.AskingDataNumber;
                    return true;
                }
                else
                {
                    response = "Esta especialidad no existe. \nQuieres modificar algo mas?\n1 - Ubicacion \n2 - Rubro"
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