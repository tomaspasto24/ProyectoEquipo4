using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text;

namespace Bot
{
    /// <summary>
    /// En esta clase se aplica el patrón Expert porque se necesita que sea experta en toda la información referente al emprendedor y a su lógica, es capáz de modificar
    /// su información y de llamar a las clases que hace falta para cumplir con sus requerimientos (llamar a las búsquedas, acceder al contacto de empresas).
    /// </summary>
    public class EntrepreneurInfo
    {
        /// <summary>
        /// Lista de las publiaciones adquiridas por el emprendedor.
        /// </summary>
        /// <typeparam name="Publication">Publicación.</typeparam>
        /// <returns>Coleción de tipo Publication.</returns>
        private List<Publication> listHistorialPublications = new List<Publication>();
        
        private SearchByLocation searchByLocation = new SearchByLocation();

        private SearchByMaterial searchByMaterial = new SearchByMaterial();
        private List<string> certifications = new List<string>();
        private List<string> specializations = new List<string>();

        /// <summary>
        /// Obtiene la ubicación.
        /// </summary>
        /// <value>Ubicación.</value>
        public GeoLocation Location { get; private set; }

        /// <summary>
        /// Establece el Rubro.
        /// </summary>
        /// <value>Obtiene el rubro.</value>
        public string Heading { get; set; }

        [JsonConstructor]
        public EntrepreneurInfo() { }

        /// <summary>
        /// Constructor de la clase Entrepreneur, setea los valores de los parámetros
        /// y suma un valor al contador de emprendedores estático.
        /// </summary>
        /// <param name="heading">Rubro.</param>
        /// <param name="geolocation">Ubicación.</param>
        /// <returns>No se devuelve, se procede con la inicialización de la instancia de clase.</returns>
        public EntrepreneurInfo(string heading, GeoLocation geolocation)
        {
            this.Location = geolocation;
            this.Heading = heading;
        }

        /// <summary>
        /// Devuelve la lista con las publicaciones que están en el historial de las adquiridas por el emprendedor.
        /// </summary>
        /// <returns>Lista de publicaciones.</returns>
        [JsonInclude]
        public List<Publication> ListHistorialPublications
        {
            get
            {
                return this.listHistorialPublications;
            }
            set
            {
                if (!(value.Count == 0))
                {
                    this.listHistorialPublications.Clear();
                    this.listHistorialPublications.AddRange(value);
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de certificaciones del emprendedor.
        /// </summary>
        /// <returns>Lista de certificaciones.</returns>
        [JsonInclude]
        public List<string> Certifications
        {
            get
            {
                return this.certifications;
            }
            set
            {
                if (!(value.Count == 0))
                {
                    this.certifications.Clear();
                    this.certifications.AddRange(value);
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de especializaciones del emprendedor.
        /// </summary>
        /// <returns>Lista de especializaciones.</returns>
        [JsonInclude]
        public List<string> Specializations
        {
            get
            {
                return this.specializations;
            }
            set
            {
                if (!(value.Count == 0))
                {
                    this.specializations.Clear();
                    this.specializations.AddRange(value);
                }
            }
        }

        /// <summary>
        /// Método para agregarle certificaciones al emprendedor.
        /// </summary>
        /// <param name="certification">Certificación.</param>
        public void AddCertification(string certification)
        {
            this.certifications.Add(certification);
        }

        /// <summary>
        /// Método para agregarle espcializaciones al emprendedor.
        /// </summary>
        /// <param name="specialization">Especialización.</param>
        public void AddSpecialization(string specialization)
        {
            this.specializations.Add(specialization);
        }

        /// <summary>
        /// Buscar publicaciones por material.
        /// </summary>
        /// <param name="wordToSearch"></param>
        /// <returns>Lista de publicaciones con el material buscado, si hay alguna que lo contenga.</returns>
        // public string SearchingByMaterials(string wordToSearch)
        // {
        //     string publications = string.Empty;
        //     foreach (Publication publication in (this.searchByMaterial.Search(wordToSearch)))
        //     {
        //         publications = publications + publication.ReturnPublication(publication);
        //     }
        //     return publications;
        // }


        /// <summary>
        /// Buscar publicaciones por ubicación.
        /// </summary>
        /// <param name="addresToSearch">Palabra clave como ubicación.</param>
        /// <returns>Lista de publicaciones con la ubicación indicada, si hay alguna.</returns>
        // public string SearchingByLocation(string addresToSearch)
        // {
        //     string publications = string.Empty;
        //     foreach (Publication publication in (this.searchByLocation.Search(addresToSearch)))
        //     {
        //         publications = publications + publication.ReturnPublication(publication);
        //     }
        //     return publications;
        // }

        /// <summary>
        /// Método público que guarda las Publicaciones adquiridas por el emprendedor.
        /// </summary>
        /// <param name="publication">Publicación.</param>
        public void AddHistorialPublication(Publication publication)
        {
            this.listHistorialPublications.Add(publication);
        }

        /// <summary>
        /// Método que se encarga de llamar al método SetInterestedPerson para que este lo fije
        /// como InterestedPerson de la clase Publication que prefiera. El método termina devolviendo
        /// el contacto de la empresa dueña de la publicación.
        /// </summary>
        /// <param name="publication">Publicación de la cual se requiere saber el contacto.</param>
        /// <returns>Contacto de la empresa de la publicación como un string.</returns>
        public string ContactCompany(Publication publication)
        {
            this.AddHistorialPublication(publication);
            publication.SetInterestedPerson(this);
            return publication.Company.ReturnContact();
        }

        public string GetCertifications()
        {
            if (certifications.Count == 0)
            {
                return "Ninguna";
            }
            StringBuilder sb = new StringBuilder();
            foreach (string text in certifications)
            {
                sb.Append(text).Append("\n");
            }
            return sb.ToString().Trim();
        }

        public string GetSpecializations()
        {
            if (specializations.Count == 0)
            {
                return "Ninguna";
            }
            StringBuilder sb = new StringBuilder();
            foreach (string text in specializations)
            {
                sb.Append(text).Append("\n");
            }
            return sb.ToString().Trim();

        }

        public override string ToString()
        {
            return "Emprendedor";
        }

        public bool ContainsSpecialization(string specialization)
        {
            return this.specializations.Contains(specialization);
        }

        public bool ContainsCertification(string certification)
        {
            return this.certifications.Contains(certification);
        }

        public void DeleteSpecialization(string specialization)
        {
            this.specializations.Remove(specialization);
        }
        public void DeleteCertification(string certification)
        {
            this.certifications.Remove(certification);
        }
    }
}