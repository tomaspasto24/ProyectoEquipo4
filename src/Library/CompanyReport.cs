using System;

namespace Bot
{
    /// <summary>
    ///clase ReporteEmpresa que implementa la interfaz de IReporte
    /// </summary>
    public class ReporteEmpresa : IReport
    {
        /// <summary>
        /// atributo de la clase 
        /// </summary>
        public String reporte;  //arraylist con la data para generar el reporte 

        /// <summary>
        /// constructor de la clase
        /// </summary>
        /// <param name="data"></param>
        public ReporteEmpresa(String data)
        {
            this.reporte = data;
        }
        /// <summary>
        /// método de la clase ReporteEmpresa
        /// </summary>
        /// <returns></returns>
        public String GiveReport()
        {
            return "";
        }
    }

}