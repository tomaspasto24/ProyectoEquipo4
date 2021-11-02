using System;
using System.Collections.Generic;
using System.Text;


namespace Bot
{
    public class EntrepreneurReport : IReport
    {
        private RoleEntrepreneur emprendedor;

        public EntrepreneurReport(RoleEntrepreneur emprendedor)
        {
            this.emprendedor = emprendedor;
        }

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