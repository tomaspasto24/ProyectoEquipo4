using System;
using System.Collections.Generic;
using System.Text;


namespace Bot
{
    public class EntrepreneurReport : IReport
    {
        public List<Publication> publications { get; set; }

        public EntrepreneurReport(List<Publication> publications)
        {
            this.publications = publications;
        }

        public String GiveReport(String entrepreneurName)
        {
            StringBuilder report = new StringBuilder("Materiales consumidos en los ultimos 30 dias por el emprendedor: " + entrepreneurName);
            int contador = 0;

            foreach (Publication publication in this.publications)
            {
                if (Equals(publication.interestedPerson.Name, entrepreneurName)
                && publication.ClosedDate >= DateTime.Now.AddDays(-30)
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