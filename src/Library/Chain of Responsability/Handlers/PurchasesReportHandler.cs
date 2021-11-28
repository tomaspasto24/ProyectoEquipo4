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
    public class PurchasesReportHandler : AbstractHandler
    {

        /// <summary>
        /// Crea una nueva instancia de éste handler y define su sucesor.
        /// </summary>
        /// <param name="succesor">El siguiente handler a ser invocado en caso de que el actual no cumpla la condición.</param>
        public PurchasesReportHandler(AbstractHandler succesor) : base(succesor)
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
            EntrepreneurInfo entrepreneurInfo = SessionRelated.Instance.GetEntrepreneurInfoByUserInfo(user);

            if (!user.HasPermission(Permission.PurchasesReport))
            {
                response = string.Empty;
                return false;
            }

            if (request.Text.Equals("/reporte") && user.HandlerState == Bot.State.Start)
            {
                StringBuilder report = new StringBuilder();
                int contador = 0;

                foreach (Publication publication in entrepreneurInfo.ReturnListHistorialPublications())
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
                    return true;
                }
            }

            response = string.Empty;
            return false;
        }
    }
}