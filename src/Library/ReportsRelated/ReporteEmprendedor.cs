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
        private RoleEntrepreneur emprendedor;
        /// <summary>
        /// constructor de la clase emprendedor
        /// </summary>
        /// <param name="emprendedor"></param>
        public EntrepreneurReport(RoleEntrepreneur emprendedor)
        {
            this.emprendedor = emprendedor;
        }
        /// <summary>
        /// metodo para entregar el reporte del emprendedor
        /// </summary>
        /// <returns></returns>
        public String GiveReport()
        {
            StringBuilder report = new StringBuilder("Materiales consumidos en los ultimos 30 dias por el emprendedor: ");
            int contador = 0;

            foreach (Publication publication in this.emprendedor.ListHistorialPublications)
            {
                if (publication.ClosedDate >= DateTime.Now.AddDays(-30)
                && publication.IsClosed)
                {
                    report.Append($"{++contador}- {publication.Title} - {publication.ClosedDate} \n");
                    return "";
                }
            }
            return report.ToString();
        }
    }
}