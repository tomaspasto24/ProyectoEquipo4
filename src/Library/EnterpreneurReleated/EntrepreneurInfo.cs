using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Bot
{
    /// <summary>
    /// En esta clase se aplica el patrón Expert porque se necesita que sea experta en toda la información referente al emprendedor y a su lógica, es capáz de modificar
    /// su información y de llamar a las clases que hace falta para cumplir con sus requerimientos (llamar a las búsquedas, acceder al contacto de empresas).
    /// </summary>
    public class EntrepreneurInfo
    {   
        private List<Publication> listHistorialPublications = new List<Publication>();

        private List<string> certifications = new List<string>();
        private List<string> specializations = new List<string>();
        /// <summary>
        /// Crea una nueva instancia de la clase EntrepreneurInfo, asignando el rubro y la localización del emprendedor.
        /// </summary>
        /// <param name="heading">Rubro del emprendedor.</param>
        /// <param name="geolocation">Ubicación del emprendedor.</param>
        public EntrepreneurInfo(string heading, GeoLocation geolocation)
        {
            this.Location = geolocation;
            this.Heading = heading;
        }

        /// <summary>
        /// Constructor sin implementación para la etiqueta JsonConstructor.
        /// </summary>
        [JsonConstructor]
        public EntrepreneurInfo() { }

        /// <summary>
        /// Obtiene la lista de publicaciones adquiridas por el emprendedor.
        /// </summary>
        /// <returns>Lista de publicaciones adquiridas.</returns>
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
        /// Obtiene del emprendedor.
        /// </summary>
        /// <value>Obtiene ubiación.</value>
        public GeoLocation Location { get; set; }

        /// <summary>
        /// Establece rubro del emprendedor.
        /// </summary>
        /// <value>Obtiene heading.</value>
        public string Heading { get; set; }

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
        /// Agrega una certificación al emprendedor.
        /// </summary>
        /// <param name="certification">Certificación en cuestión.</param>
        public void AddCertification(string certification)
        {
            this.certifications.Add(certification);
        }

        /// <summary>
        /// Agrega una especialización al emprendedor.
        /// </summary>
        /// <param name="specialization">Especialización en cuestión.</param>
        public void AddSpecialization(string specialization)
        {
            this.specializations.Add(specialization);
        }

        /// <summary>
        /// Agrega una publicación al historial de publicaciones adquiridas.
        /// </summary>
        /// <param name="publication">Publicación en cuestión.</param>
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
            bool precondition = publication != null;
            if (!precondition)
            {
                throw new ArgumentNullException("Publicación es null.");
            }
            this.AddHistorialPublication(publication);
            publication.SetInterestedPerson(this);
            bool postcondition = publication.Company.ReturnContact() != string.Empty;
            if (!postcondition)
            {
                throw new NullReferenceException("El contacto de la publicación esta vacío.");
            }
            return publication.Company.ReturnContact();
        }

        /// <summary>
        /// Obtiene todas las certificaciones que tiene el emprendedor.
        /// </summary>
        /// <returns>Las certificaciones.</returns>
        public string GetCertifications()
        {
            if (this.certifications.Count == 0)
            {
                return "Ninguna";
            }

            StringBuilder sb = new StringBuilder();
            foreach (string text in this.certifications)
            {
                sb.Append(text).Append('\n');
            }

            return sb.ToString().Trim();
        }

        /// <summary>
        /// Obtiene todas las especializaciones que tiene el emprendedor.
        /// </summary>
        /// <returns>Las especializaciones.</returns>
        public string GetSpecializations()
        {
            if (this.specializations.Count == 0)
            {
                return "Ninguna";
            }

            StringBuilder sb = new StringBuilder();
            foreach (string text in this.specializations)
            {
                sb.Append(text).Append('\n');
            }

            return sb.ToString().Trim();
        }

        /// <summary>
        /// Verifica si el emprendedor tiene o no una especialización.
        /// </summary>
        /// <param name="specialization">Especialización a verificar.</param>
        /// <returns>True si contiene la especialización, false en caso contrario.</returns>
        public bool ContainsSpecialization(string specialization)
        {
            return this.specializations.Contains(specialization);
        }

        /// <summary>
        /// Verifica si el emprendedor tiene o no una certificación.
        /// </summary>
        /// <param name="certification">Certificación a verificar.</param>
        /// <returns>True si contiene la certificación, false en caso contrario.</returns>
        public bool ContainsCertification(string certification)
        {
            return this.certifications.Contains(certification);
        }

        /// <summary>
        /// Elimina una especialización del emprendedor.
        /// </summary>
        /// <param name="specialization">Especialización en cuestión.</param>
        public void DeleteSpecialization(string specialization)
        {
            this.specializations.Remove(specialization);
        }

        /// <summary>
        /// Elimina una certificación del emprendedor.
        /// </summary>
        /// <param name="certification">Certificación en cuestión.</param>
        public void DeleteCertification(string certification)
        {
            this.certifications.Remove(certification);
        }
    }
}