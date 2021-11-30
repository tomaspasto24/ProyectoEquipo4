using System;
using System.Text;

namespace Bot
{
    /// <summary>
    /// Handler que se encarga de otorgar, a un usuario de empresa, las ventas realizadas anteriormente
    /// Patrones y principios:
    /// Debido a que se indentifica una sola razón de cambio, esta clase cumple con SRP, este motivo de cambio podría ser, cambiar el método InternalHandle.
    /// También cumple con Expert, ya que posee todo lo necesario para cumplir la responsabilidad otorgada a la clase.
    /// Cumple con Polymorphism porque usa el método polimórfico InternalHandle. 
    /// A su vez, cumple con el patrón Chain of Responsability.
    /// </summary>
    public class SalesReportHandler : AbstractHandler
    {
        /// <summary>
        /// Crea una nueva instancia de éste handler y define su sucesor.
        /// </summary>
        /// <param name="succesor">El siguiente handler a ser invocado en caso de que el actual no cumpla la condición.</param>
        public SalesReportHandler(AbstractHandler succesor) 
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
            UserCompanyInfo userCompanyInfo = SessionRelated.Instance.GetUserCompanyByUserInfo(user);

            if (!user.HasPermission(Permission.SalesReport))
            {
                response = string.Empty;
                return false;
            }

            if (request.Text.Equals("/reporte") && user.HandlerState == Bot.State.Start)
            {
                string report = string.Empty;
                CompanyReport companyReport = new CompanyReport(userCompanyInfo.company);
                report = companyReport.GiveReport();

                if (report.Length > 0)
                {
                    response = $"Publicaciones cerradas de los ultimos 30 dias de la empresa: {userCompanyInfo.company.Name} \n  {report}";
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