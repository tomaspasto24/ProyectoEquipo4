using System;

namespace Bot
{
    public class ReporteEmpresa : IReporte
    {
        public String reporte;

        public ReporteEmpresa(String data)
        {
            this.reporte = data;
        }
        public String OtorgarReporte()
        {
            return "";
        }
    }

}