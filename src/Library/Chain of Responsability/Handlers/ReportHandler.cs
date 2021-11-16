using System;
using System.Text;

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
    public class ReportHandler : AbstractHandler
    {
        /// <summary>
        /// Indica los diferentes estados que puede tener el comando RegisterHandler.
        /// - Start: El estado inicial del comando. En este estado el comando pide el token de registro
        /// - ConfirmingToken: Luego de pedir el token. En este estado el comando valida si el token ingresado existe y vuelve al estado Start.
        /// </summary>
        public enum ReportState
        {
            /// Estado antes de mandar el token
            Start,
            /// Estado mientras el bot espera y confirma un token
            ConfirmingToken,
        }

        /// <summary>
        /// El estado del comando.
        /// </summary>
        public ReportState State { get; private set; }

        /// <summary>
        /// Constructor de la clase RegisterHandler
        /// </summary>
        /// <param name="succesor">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public ReportHandler(AbstractHandler succesor) : base(succesor)
        {
            State = ReportState.Start;
        }

        /// <summary>
        /// Metodo que se encarga de atender el handler.
        /// </summary>
        /// <param name="request">Mensaje que contiene el texto y el id del usuario.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        protected override bool InternalHandle(Message request, out string response)
        {
            UserInfo user = SessionRelated.Instance.GetUserById(request.UserId);

            if (!(user.UserRole is RoleEntrepreneur) || !(user.UserRole is RoleUserCompany))
            {
                throw new IncorrectRoleException("Disculpa no tienes el rol adecuado para utilizar este comando");
            }

            if (user.UserRole is RoleEntrepreneur && request.Text.ToLower() == "/reporte" && user.HandlerState == Bot.State.Start)
            {
                StringBuilder report = new StringBuilder();
                int contador = 0;

                foreach (Publication publication in ((RoleEntrepreneur)user.UserRole).ReturnListHistorialPublications())
                {
                    if (publication.ClosedDate >= DateTime.Now.AddDays(-30)
                    && publication.IsClosed)
                    {
                        report.Append($"#{++contador} - {publication.Title} - {publication.ClosedDate} \n");
                    }
                }

                if (report.Length > 0)
                {
                    response = $"Materiales consumidos en los ultimos 30 dias por el emprendedor: {user.Name} {report} ";
                    return true;
                }
                else
                {
                    response = $"El emprendedor: {user.Name}, no tiene publicaciones asignadas en los ultimos 30 dias";
                    return false;
                }
            }
            else if (user.UserRole is RoleUserCompany && request.Text.ToLower() == "/reporte" && user.HandlerState == Bot.State.Start)
            {
                StringBuilder report = new StringBuilder();
                int contador = 0;

                foreach (Publication publication in ((RoleUserCompany)user.UserRole).company.ListHistorialPublications)
                {
                    if (publication.ClosedDate >= DateTime.Now.AddDays(-30)
                    && publication.IsClosed)
                    {
                        report.Append($"{++contador}- {publication.Title} - {publication.ClosedDate} \n");
                    }
                }

                if (report.Length > 0)
                {
                    response = $"Publicaciones cerradas de los ultimos 30 dias de la empresa: {((RoleUserCompany)user.UserRole).company.Name} \n  {report.ToString()}";
                    return true;
                }
                else
                {
                    response = $"No hay publicaciones cerradas en los ultimos 30 dias para la empresa: {((RoleUserCompany)user.UserRole).company.Name}";
                    return false;
                }
            }
            else if (user.UserRole is RoleAdmin && request.Text.ToLower() == "/reporte" && user.HandlerState == Bot.State.Start)
            {
                response = "Disculpa, no eres un emprendedor o un usuario empresa";
                return false;
            }

            response = string.Empty;
            return false;
        }
    }
}