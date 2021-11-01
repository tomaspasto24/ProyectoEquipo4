using System;
using System.Collections.Generic;
using System.Text;



namespace Bot
{
    /// <summary>
    ///clase ReporteEmpresa que implementa la interfaz de IReporte
    /// </summary>
    public class CompanyReport : IReport
    {
        /// <summary>
        /// atributo de la clase 
        /// </summary>
        private Company company;  //arraylist con la data para generar el reporte 

        /// <summary>
        /// Constructor de la clase CompanyReport
        /// </summary>
        /// <param name="company"></param>
        public CompanyReport(Company company)
        {
            this.company = company;
        }
        /// <summary>
        /// método de la clase ReporteEmpresa
        /// </summary>
        /// <returns></returns>
        public String GiveReport()
        {
            StringBuilder report = new StringBuilder("Publicaciones cerradas de los ultimos 30 dias de la empresa: " + company.Name);
            int contador = 0;

            foreach (Publication publication in this.company.GetListHistorialPublications())
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