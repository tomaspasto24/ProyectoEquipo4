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
        private SearchByLocation searchByLocation;
        private SearchByMaterial searchByMaterial;
        /// <summary>
        /// Rubro
        /// </summary>
        public string heading;
        private List<string> certification;
        private List<string> specializations;
        public List<Publication> listHistorialPublications = new List<Publication>();

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
            if (certification != null)
            {
                this.certification.Add(certification);    
            }
        }
        public List<string> ReturnCertification()
        {
            return this.certification;
        }

        /// <summary>
        /// Método para agregarle espcializaciones al emprendedor
        /// </summary>
        /// <param name="specializations"></param>
        public void AddSpecialization(string specialization)
        {
            if (specialization != null)
            {
                this.specializations.Add(specialization);
            }
        }

        public List<string> ReturnSpecialization()
        {
            return this.specializations;
        }

        /// <summary>
        /// Devuelve el contador estático que representa la cantidad de emprendedores registrados
        /// </summary>
        /// <value></value>
        public static int EntrepreneurAccountant
        {
            get
            {
                return entrepreneurAccountant;
            }
        }

        /// <summary>
        /// Buscar publicaciones por material
        /// </summary>
        /// <returns></returns>
        public List<Publication> SearchingByMaterials(string wordToSearch)
        {
            return this.searchByMaterial.Search(wordToSearch); 
        }
        /// <summary>
        /// Buscar publicaciones por ubicación
        /// </summary>
        /// <param name="addresToSearch"></param>
        /// <returns></returns>
        public List<Publication> SearchingByLocation(string addresToSearch)
        {
            return this.searchByLocation.Search(addresToSearch);
        }

        /// <summary>
        /// Método público que guarda las Publicaciones adquiridas por el emprendedor.
        /// </summary>
        /// <param name="publication">Publicación cerrada.</param>
        public void AddHistorialPublication(Publication publication)
        {
            this.listHistorialPublications.Add(publication);
        }

        public List<Publication> ReturnListHistorialPublications()
        {
            {
                return this.listHistorialPublications;
            }
        }

        /// <summary>
        /// Método que se encarga de llamar al método SetInterestedPerson para que este lo fije
        /// como InterestedPerson de la clase Publicación que prefiera. El método termina devolviendo
        /// el contacto de la empresa dueña de la publicación.
        /// </summary>
        /// <param name="publication"></param>
        public string ContactCompany(Publication publication)
        {
            this.AddHistorialPublication(publication);
            publication.SetInterestedPerson(this);
            return publication.Company.ReturnContact();
        }
    }
}