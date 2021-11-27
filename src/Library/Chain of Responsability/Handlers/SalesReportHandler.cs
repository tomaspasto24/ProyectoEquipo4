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
    public class SalesReportHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de la clase RegisterHandler
        /// </summary>
        /// <param name="succesor">Condicion que se tiene que cumplir para que se ejecute el handler</param>
        public SalesReportHandler(AbstractHandler succesor) : base(succesor)
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
            UserCompanyInfo userCompanyInfo = SessionRelated.Instance.GetUserCompanyByUserInfo(user);

            if (!user.HasPermission(Permission.PurchasesReport))
            {
                response = string.Empty;
                return false;
            }

            if (request.Text.Equals("/reporte") && user.HandlerState == Bot.State.Start)
            {
                StringBuilder report = new StringBuilder();
                int contador = 0;

                foreach (Publication publication in userCompanyInfo.company.ListHistorialPublications)
                {
                    if (publication.ClosedDate >= DateTime.Now.AddDays(-30)
                    && publication.IsClosed)
                    {
                        report.Append($"{++contador}- {publication.Title} - {publication.ClosedDate} \n");
                    }
                }

                if (report.Length > 0)
                {
                    response = $"Publicaciones cerradas de los ultimos 30 dias de la empresa: {userCompanyInfo.company.Name} \n  {report.ToString()}";
                    return true;
                }
                else
                {
                    response = $"No hay publicaciones cerradas en los ultimos 30 dias para la empresa: {userCompanyInfo.company.Name}";
                    return true;
                }
            }

            response = string.Empty;
            return false;
        }
    }
}