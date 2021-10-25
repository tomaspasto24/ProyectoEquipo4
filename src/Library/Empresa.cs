using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    public class Empresa
    {
        private static int contadorEmpresas = 0;
        private string nombre;
        private string rubro;
        private Ubicacion ubicacion;
        private string contacto;
        private List<Usuario> conjuntoUsuarios = new List<Usuario>();

        public static int ContadorEmpresas
        {
            get 
            {
                return contadorEmpresas;
            }
        }

        public Empresa(string nombre, string rubro, Ubicacion ubicacion, string contacto)
        {
            this.nombre = nombre;
            this.rubro = rubro;
            this.ubicacion = ubicacion;
            this.contacto = contacto;
        }

        public string DevolverContacto()                    
        {
            StringBuilder resultado = new StringBuilder("Materiales: \n");

            resultado.Append($"Empresa: {this.nombre} \n");
            resultado.Append($"Rubro: {this.rubro} \n");
            resultado.Append($"Contacto: {this.contacto} \n");

            return resultado.ToString();
        }
    }
}