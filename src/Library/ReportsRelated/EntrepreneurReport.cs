using System;
using System.Collections.Generic;
using System.Text;


namespace Bot
{
    /// <summary>
    /// clase reporte emprendedor que hereda de la interfaz iReport
    /// </summary>
    public class EntrepreneurReport : IReport
    {
        private RoleEntrepreneur entrepreneur;
        /// <summary>
        /// constructor de la clase emprendedor
        /// </summary>
        /// <param name="entrepreneur"></param>
        public EntrepreneurReport(RoleEntrepreneur entrepreneur)
        {
            this.entrepreneur = entrepreneur;
        }
        /// <summary>
        /// metodo para entregar el reporte del emprendedor
        /// </summary>
        /// <returns></returns>
        public String GiveReport()
        {
            StringBuilder report = new StringBuilder();
            String result;
            int contador = 0;

            foreach (Publication publication in this.entrepreneur.listHistorialPublications)
            {
                if (publication.ClosedDate >= DateTime.Now.AddDays(-30)
                && publication.IsClosed)
                {
                    report.Append($"#{++contador} - {publication.Title} - {publication.ClosedDate} \n");
                }
            }
            if (report.Length > 0)
            {
                result = ($"Materiales consumidos en los ultimos 30 dias por el emprendedor: {this.entrepreneur.Name} {report} ");
            }
            else
            {
                result = $"El emprendedor: {this.entrepreneur.Name}, no tiene publicaciones asignadas en los ultimos 30 dias";
            }
            return result;
        }
    }
}