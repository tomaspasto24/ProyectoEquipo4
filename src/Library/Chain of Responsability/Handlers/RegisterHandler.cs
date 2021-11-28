using System;

namespace Bot
{
    /// <summary>
    /// Handler que se encarga del registro de un usuario
    /// Patrones y principios:
    /// También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    /// Polimorfismo     
    /// A su vez, cumple con el patrón Chain of Responsability.
    /// </summary>
    public class RegisterHandler : AbstractHandler
    {
        /// <summary>
        /// Crea una nueva instancia de éste handler y define su sucesor.
        /// </summary>
        /// <param name="succesor">El siguiente handler a ser invocado en caso de que el actual no cumpla la condición.</param>
        public RegisterHandler(AbstractHandler succesor) : base(succesor)
        {
        }

        /// <summary>
        /// Intenta procesar el mensaje recibido y devuelve una respuesta.
        /// </summary>
        /// <param name="request">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario</returns>
        protected override bool InternalHandle(Message request, out string response)
        {
            UserInfo user = SessionRelated.Instance.GetUserById(request.UserId);
            

            if (!user.HasPermission(Permission.Register))
            {
                response = string.Empty;
                return false;
            }

            if ((user.HandlerState == Bot.State.Start) && (request.Text.Equals("/registro")))
            {
                user.HandlerState = Bot.State.ConfirmingToken;
                response = "Inserta tu token de usuario empresa: ";
                return true;
            }
            else if (user.HandlerState == Bot.State.ConfirmingToken)
            {
                if (SessionRelated.Instance.DiccUserTokens.ContainsKey(request.Text))
                {
                    user.HandlerState = Bot.State.Start;
                    user.Permissions = UserInfo.UserCompanyPermissions;
                    SessionRelated.Instance.DiccUserCompanyInfo.Add(user, new UserCompanyInfo(SessionRelated.Instance.GetCompanyByToken(request.Text)));
                    response = "Token verificado, ahora eres un usuario empresa! :)";
                    return true;
                }
                else
                {
                    user.HandlerState = Bot.State.Start;
                    response = "Disculpa, no hemos encontrado ese token :(";
                    return true;
                }
            }

            response = string.Empty;
            return false;
        }
    }
}