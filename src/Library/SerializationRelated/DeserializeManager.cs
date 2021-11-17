using System;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;

namespace Bot
{
    public class DeserializeManager 
    {
        private const string pathContainerCompany = @"..\..\..\..\..\docs\CompanyDataBase.json";
        private const string pathContainerPublication = @"..\..\..\..\..\docs\PublicationDataBase.json";
        private static DeserializeManager instance;
        private DeserializeManager() { }

        /// <summary>
        /// Método estático que controla el acceso a la propia instancia de la clase DeserializeManager,
        /// en caso de que la variable _instance no este creada, la crea y la retorna. En caso 
        /// contrario de que anteriormente este creada simplemente la retorna, asi se asegura de que
        /// siempre se use la misma variable instancia.
        /// </summary>
        /// <returns>Instancia Deserialize.</returns>
        public static DeserializeManager GetInstance()
        {
            if (instance == null)
            {
                instance = new DeserializeManager();
            }
            return instance;
        }

        public bool DeserializeProgram()
        {
            bool conditionCompanies = this.DeserializeCompanies();
            bool conditionPublications = this.DeserializePublications();

            return conditionCompanies && conditionPublications;
        }

        private bool DeserializeCompanies()
        {
            if (File.Exists(pathContainerCompany))
            {
                // Leer cada linea.
                // string json = File.ReadAllLines(pathContainerCompany);

                string json = File.ReadAllText(pathContainerCompany);

                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = MyReferenceHandler.Instance,
                    WriteIndented = true
                };

                IList<Company> listCompanies = JsonSerializer.Deserialize<IList<Company>>(json, options);
                foreach (Company company in listCompanies)
                {
                    CompanySet.Instance.AddElement(company);
                }
                return true;
            }
            else
            {
                File.Create(pathContainerCompany);
                return false;
            }
        }

        private bool DeserializePublications()
        {
            if (File.Exists(pathContainerPublication))
            {
                string json = File.ReadAllText(pathContainerPublication);

                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = MyReferenceHandler.Instance,
                    WriteIndented = true
                };

                IList<Publication> listPublications = JsonSerializer.Deserialize<IList<Publication>>(json, options);
                foreach (Publication publication in listPublications)
                {
                    PublicationSet.Instance.AddElement(publication);
                }
                return true;
            }
            else
            {
                File.Create(pathContainerPublication);
                return false;
            }
        }
    }
}
