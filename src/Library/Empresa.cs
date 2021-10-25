using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    public class Company
    {
        //Hacer metodo para publicar Publicación. y guardarlo en lista 
        private static List<Company> empresasRegistradas = new List<Company>();
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

        public Company(string nombre, string rubro, Ubicacion ubicacion, string contacto)
        {
            this.nombre = nombre;
            this.rubro = rubro;
            this.ubicacion = ubicacion;
            this.contacto = contacto;
            contadorEmpresas++;
        }

        public string DevolverContacto()                    
        {
            StringBuilder resultado = new StringBuilder("Contacto: \n");

            resultado.Append($"Empresa: {this.nombre} \n");
            resultado.Append($"Rubro: {this.rubro} \n");
            resultado.Append($"Contacto: {this.contacto} \n");

            return resultado.ToString();
        }
        
        public void RegistrarEmpresa()
        {
            empresasRegistradas.Add(this);
        }

        public void BorrarEmpresa()
        {
            empresasRegistradas.Remove(this);
        }
    }
}