using System;
using System.Text;

namespace Bot
{
    /// <summary>
    /// clase reporte emprendedor que cumple con el patron SRP, debido a que tiene una responsabilidad unica que es dar el reporte emprendedor. esta clase tambien implementa la interfaz IReport y cumple con el patron ISP.
    /// </summary>
    public class EntrepreneurReport : IReport
    {
        private RoleEntrepreneur entrepreneur;

        /// <summary>
        /// constructor de la clase emprendedor.
        /// </summary>
        /// <param name="entrepreneur">El nombre del emprendedor.</param>
        public EntrepreneurReport(RoleEntrepreneur entrepreneur)
        {
            this.entrepreneur = entrepreneur;
        }

        /// <summary>
        /// metodo para entregar el reporte del emprendedor.
        /// </summary>
        /// <returns>El reporte del emprendedor.</returns>
        public String GiveReport()
        {
            StringBuilder report = new StringBuilder();
            String result;
            int contador = 0;

            foreach (Publication publication in this.entrepreneur.ReturnListHistorialPublications())
            {
                if (publication.ClosedDate >= DateTime.Now.AddDays(-30)
                && publication.IsClosed)
                {
                    report.Append($"#{++contador} - {publication.Title} - {publication.ClosedDate} \n");
                }
            }

            if (report.Length > 0)
            {
<<<<<<< HEAD
                result = $"Materiales consumidos en los ultimos 30 dias por el emprendedor {report} ";
=======
                result = $"Materiales consumidos en los ultimos 30 dias por el emprendedor: {report} ";
>>>>>>> d55b4d64c8abf6ebd77001d20ad6b99901e9ecb1
            }
            else
            {
                result = $"El emprendedor no tiene publicaciones asignadas en los ultimos 30 dias";
            }

            return result;
        }
    }
}