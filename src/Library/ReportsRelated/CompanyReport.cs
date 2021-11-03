using System;
using System.Collections.Generic;
using System.Text;


namespace Bot
{
    /// <summary>
    /// clase reporte emprendedor que cumple con el patron SRP, debido a que tiene una responsabilidad unica que es dar el reporte emprendedor. tambien esta clase implementa la interfaz IReport y cumple con el patron ISP.
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
            String result;
            StringBuilder report = new StringBuilder();
            int contador = 0;

            foreach (Publication publication in this.company.GetListHistorialPublications())
            {
                if (publication.ClosedDate >= DateTime.Now.AddDays(-30)
                && publication.IsClosed)
                {
                    report.Append($"{++contador}- {publication.Title} - {publication.ClosedDate} \n");
                }

            }
            if (report.Length > 0)
            {
                result = "Publicaciones cerradas de los ultimos 30 dias de la empresa: " + company.Name + "\n" + report.ToString();
            }
            else
            {
                result = "No hay publicaciones cerradas en los ultimos 30 dias para la empresa: " + company.Name;
            }
            return result;
        }
    }
}