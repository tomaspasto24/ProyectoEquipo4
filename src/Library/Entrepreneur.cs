using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class Entrepreneur: User
    {
        private static List<Entrepreneur> entrepreneurs = new List<Entrepreneur>();
        private static int entrepreneurAccountant = 0;
        private string name;
        private GeoLocation location;
        public string heading;
        private List<string> certification;
        private List<string> specializations;
        /// <summary>
        /// Constructor de la clase Entrepreneur, setea los valores de los parámetros 
        /// y suma un valor al contador de emprendedores estático
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="heading"></param>
        /// <param name="geolocation"></param>
        /// <param name="certification"></param>
        /// <param name="specializations"></param>
        public Entrepreneur(string heading, GeoLocation geolocation, string certification, string specializations, string username, string password) : base(username, password)
        {
            this.location = location;
            this.heading = heading;
            entrepreneurAccountant++;
        }
        /// <summary>
        /// Método para agregarle certificaciones al emprendedor
        /// </summary>
        /// <param name="certification"></param>
        public void AddCertification(string certification)
        {
            this.certification.Add(certification);
        }
        /// <summary>
        /// Método para agregarle espcializaciones al empresario
        /// </summary>
        /// <param name="specializations"></param>
        public void AddSpecialization(string specializations)
        {
            this.specializations.Add(specializations);
        }
        /// <summary>
        /// Método para elimienar a un emprendedor de la lista de emprendedores
        /// </summary>
        /// <param name="entrepreneur"></param>
        public void DeleateEntrepreneur(Entrepreneur entrepreneur)
        {
            entrepreneurs.Remove(entrepreneur);
        }
        /// <summary>
        /// Método para registrar a un emprendedor, agregándolo a la lista de emprendedores registrados
        /// </summary>
        /// <param name="entrepreneur"></param>
        public void RegisterEntrepreneur(Entrepreneur entrepreneur)
        {
            entrepreneurs.Add(entrepreneur);
        }
        /// <summary>
        /// Método para obtener el reporte del emprendedor 
        /// </summary>
        /// <returns></returns>
        public string GetReport()
        {
            return ($"Nombre: {name}....");
        }
        public static int EntrepreneurAccountant
        {
            get 
            {
                return entrepreneurAccountant;
            }
        }
        /// <summary>
        /// Método para buscar por materiasles o por ubicación
        /// </summary>
        /// <returns></returns>
        public List<Publication> SearchingMaterials()
        {
            List<Publication> publicationsFound = new List<Publication>();
            return publicationsFound;
        }
    }
}