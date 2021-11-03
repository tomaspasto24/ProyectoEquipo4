using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class RoleEntrepreneur : Role
    {
        private static int entrepreneurAccountant = 0;
        private GeoLocation location;
        public string heading; //rubro
        private List<Publication> listHistorialPublications = new List<Publication>();
        private List<string> certification = new List<string>();
        private List<string> specializations = new List<string>();

        public List<Publication> ListHistorialPublications
        {
            get
            {
                return this.listHistorialPublications;
            }
        }
        /// <summary>
        /// /// Constructor de la clase Entrepreneur, setea los valores de los parámetros 
        /// y suma un valor al contador de emprendedores estático
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="heading"></param>
        /// <param name="geolocation"></param>
        /// <param name="certification"></param>
        /// <param name="specialization"></param>
        /// <returns></returns>
        public RoleEntrepreneur(string name, int id, string heading, GeoLocation geolocation, string certification, string specialization) : base(name, id)
        {
            this.location = geolocation;
            this.heading = heading;
            this.AddCertification(certification);
            this.AddSpecialization(specialization);
            entrepreneurAccountant++;
        }
        /// <summary>
        /// Método para agregarle certificaciones al emprendedor
        /// </summary>
        /// <param name="certification"></param>
        public void AddCertification(string certification)
        {
            if (certification != null) this.certification.Add(certification);
        }
        /// <summary>
        /// Método para agregarle espcializaciones al empresario
        /// </summary>
        /// <param name="specializations"></param>
        public void AddSpecialization(string specializations)
        {
            if (specializations != null) this.specializations.Add(specializations);
        }

        /// <summary>
        /// Método para obtener el reporte del emprendedor 
        /// </summary>
        /// <returns></returns>
        public string GetReport()
        {
            return ($"Nombre: ....");
        }
        public static int EntrepreneurAccountant
        {
            get
            {
                return entrepreneurAccountant;
            }
        }
        /// <summary>
        /// Método para buscar por materiales o por ubicación
        /// </summary>
        /// <returns></returns>
        public List<Publication> SearchingMaterials(string wordToSearch)
        {
            return Search(wordToSearch);
        }

        public RoleEntrepreneur SaveHistorialPublication(Publication publication)
        {
            return 
        }

        /// <summary>
        /// Método público que guarda las Publicaciones adquiridas por el emprendedor.
        /// </summary>
        /// <param name="publication">Publicación cerrada.</param>
        public void SaveHistorialPublication(Publication publication)
        {
            listHistorialPublications.Add(publication);
        }

        /// <summary>
        /// Método que se encarga de llamar al método SetInterestedPerson para que este lo fije
        /// como InterestedPerson de la clase Publicación que prefiera. El método termina devolviendo
        /// el contacto de la empresa dueña de la publicación.
        /// </summary>
        /// <param name="publication"></param>
        public string AskContactToPublication(Publication publication)
        {
            this.SaveHistorialPublication(publication);
            publication.SetInterestedPerson(this);
            return publication.Company.ReturnContact();
        }
    }
}