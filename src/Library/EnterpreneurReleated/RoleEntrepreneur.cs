using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    /// <summary>
    /// En esta clase se aplica el patrón Expert porque se necesita que sea experta en toda la información referente al emprendedor y a su lógica, es capáz de modificar
    /// su información y de llamar a las clases que hace falta para cumplir con sus requerimientos (llamar a las búsquedas, acceder al contacto de empresas).
    /// </summary>
    public class RoleEntrepreneur : Role
    {
        /// <summary>
        /// Lista de las publiaciones adquiridas por el emprendedor.
        /// </summary>
        /// <typeparam name="Publication">Tipo de la lista devuelta.</typeparam>
        /// <returns>Lista de tipo Publication.</returns>
        private List<Publication> listHistorialPublications = new List<Publication>();
        private GeoLocation location;
        private SearchByLocation searchByLocation;
        private SearchByMaterial searchByMaterial;

        /// <summary>
        /// Rubro.
        /// </summary>
        private string heading;
        private List<string> certification = new List<string>();
        private List<string> specializations = new List<string>();

        /// <summary>
        /// Constructor de la clase Entrepreneur, setea los valores de los parámetros
        /// y suma un valor al contador de emprendedores estático.
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
        /// Método para devolver la lista de certificaciones del emprendedor
        /// </summary>
        /// <returns></returns>
        public List<string> ReturnCertification()
        {
            return this.certification;
        }

        /// <summary>
        /// Método para agregarle espcializaciones al emprendedor
        /// </summary>
        /// <param name="specialization"></param>
        public void AddSpecialization(string specialization)
        {
            this.specializations.Add(specialization);
            /*if (specialization != null)
            {
                this.specializations.Add(specialization);
            }*/
        }
        /// <summary>
        /// Método para devolver la lista de especializaciones del emprendedor
        /// </summary>
        /// <returns></returns>
        public List<string> ReturnSpecialization()
        {
            return this.specializations;
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
        /// <summary>
        /// Devuelve la lista con las publicaciones adquiridas por el emprendedor
        /// </summary>
        /// <returns></returns>
        public List<Publication> ReturnListHistorialPublications()
        {
            {
                return this.listHistorialPublications;
            }
        }
        /// <summary>
        /// Método que se encarga de llamar al método SetInterestedPerson para que este lo fije
        /// como InterestedPerson de la clase Publication que prefiera. El método termina devolviendo
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