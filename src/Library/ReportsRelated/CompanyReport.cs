using System;
using System.Text;

namespace Bot
{
    /// <summary>
    /// Clase reporte emprendedor que cumple con el patron SRP, debido a que tiene una responsabilidad unica que es dar el reporte a la empresa. tambien esta clase implementa la interfaz IReport y cumple con el patron ISP.
    /// </summary>
    public class CompanyReport : IReport
    {
        /// <summary>
        /// atributo de la clase.
        /// </summary>
        private Company company;

        /// <summary>
        /// Constructor de la clase CompanyReport.
        /// </summary>
        /// <param name="company">El nombre de la compania.</param>
        public CompanyReport(Company company)
        {
            this.company = company;
        }

        /// <summary>
        /// método de la clase ReporteEmpresa el cual se encarga de generar el reporte y devolverlo.
        /// </summary>
        /// <returns>El reporte de la empresa.</returns>
        public String GiveReport()
        {
            String result;
            StringBuilder report = new StringBuilder();
            int contador = 0;

            foreach (Publication publication in this.company.ListHistorialPublications)
            {
                if (publication.ClosedDate >= DateTime.Now.AddDays(-30)
                && publication.IsClosed)
                {
                    UserInfo interestedUser = SessionRelated.Instance.GetUserInfoByEntrepreneurInfo(publication.InterestedPerson);
                    report.Append($"{++contador} - {interestedUser.Name} - {publication.Title} - {publication.ClosedDate}\n");
                }
            }

            if (report.Length > 0)
            {
                result = $"Publicaciones cerradas de los ultimos 30 dias de la empresa: {this.company.Name}\n{report.ToString()}";
            }
            else
            {
                result = $"No hay publicaciones cerradas en los ultimos 30 dias para la empresa: {this.company.Name}";
            }

            return result;
        }
    }
}
