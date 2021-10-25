using System;
using System.Collections.Generic;

namespace Bot
{
    public class Ubicacion
    {
        //En un futuro usar Geolocalización.
        private string zona;
        private string departamento;
        private string direccion;

        public Ubicacion(string direccion, string zona, string departamento)
        {
            this.direccion = direccion;
            this.zona = zona;
            this.departamento = departamento;
        }
    }
}