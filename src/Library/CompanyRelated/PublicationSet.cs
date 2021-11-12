using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace Bot
{
    /// <summary>
    /// Clase PublicationSet que se encarga de la gestion de las clases Publications y de que
    /// estas persistan en el sistema. Tiene un parecido a la clase CompanySet pero no se puede
    /// implementar abstracciones porque las clases estáticas no permiten este tipo de implementaciones.
    /// </summary>
    public static class PublicationSet 
    {
    private const string Path = @"..\..\..\..\..\docs\PublicationDataBase.json";

        /// <summary>
        /// Obtiene la lista de clases Publicación extraida del archivo PublicationSet.txt.
        /// </summary>
        /// <value>Lista Publicación de solo lectura.</value>
        public static IReadOnlyCollection<Publication> ListPublication
        {
            get
            {
                List<Publication> listPublications = new List<Publication>();
                Publication publication;
                try
                {
                    using(StreamReader txtReader = new StreamReader(Path))
                    {
                        string line = txtReader.ReadLine();
                        string title;
                        Company company;
                        GeoLocation location;

                        while (line != null)
                        {
                            title = JsonSerializer.Deserialize<Publication>(line).Title;
                            company = JsonSerializer.Deserialize<Publication>(line).Company;
                            location = JsonSerializer.Deserialize<Publication>(line).Location;
                            publication = new Publication(title, company, location);
                            listPublications.Add(publication);
                            //Read the next line
                            line = txtReader.ReadLine();
                        }
                        txtReader.Close();
                        txtReader.Dispose();
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return listPublications.AsReadOnly();
            }
        }

        /// <summary>
        /// Método que se encarga de agregar una clase Publicación al sistema para que 
        /// pueda persistir aunque el bot caiga.
        /// </summary>
        /// <param name="title">Titulo.</param>
        /// <param name="company">Empresa.</param>
        /// <param name="location">Ubicación</param>
        /// <param name="material">Material.</param>
        /// <returns><c>True</c> en caso de que se pueda agregar y <c>False</c> en caso 
        /// contrario.</returns>
        public static bool AddPublication(String title, Company company, GeoLocation location, Material material)
        {
            Publication publication = new Publication(title, company, location, material);

            if(!ContainsPublicationInListPublications(publication))
            {
                string jsonCompany = JsonSerializer.Serialize(publication);

                using(StreamWriter txtWrite = new StreamWriter(Path, true))
                {
                    txtWrite.WriteLine(jsonCompany);
                    txtWrite.Close();
                    txtWrite.Dispose();
                    return true;
                }
            }
            else return false;
        }

        /// <summary>
        /// Sobrecarga del método AddCompany que permite no ingresar un material inicial.
        /// </summary>
        /// <param name="title">Titulo.</param>
        /// <param name="company">Empresa.</param>
        /// <param name="location">Ubicación.</param>
        /// <returns><c>True</c> en caso de que se pueda agregar y <c>False</c> en caso 
        /// contrario.</returns>
        public static bool AddPublication(String title, Company company, GeoLocation location)
        {
            Publication publication = new Publication(title, company, location);

            if(!ContainsPublicationInListPublications(publication))
            {
                string jsonCompany = JsonSerializer.Serialize(publication);

                using(StreamWriter txtWrite = new StreamWriter(Path, true))
                {
                    txtWrite.WriteLine(jsonCompany);
                    txtWrite.Close();
                    txtWrite.Dispose();
                    return true;
                }
            }
            else return false;
        }

        /// <summary>
        /// Sobrecarga del método AddCompany que permite ingresar un objeto Publicación como parámetro
        /// para ser ingresado al sistema.
        /// </summary>
        /// <param name="publication"></param>
        /// <returns></returns>
        public static bool AddPublication(Publication publication)
        {
            if(!ContainsPublicationInListPublications(publication))
            {
                string jsonPublication = JsonSerializer.Serialize(publication);

                using(StreamWriter txtWrite = new StreamWriter(Path, true))
                {
                    txtWrite.WriteLine(jsonPublication);
                    txtWrite.Close();
                    txtWrite.Dispose();
                    return true;
                }
            }
            else return false;
        }

        /// <summary>
        /// Método que elimina una clase Publicación del sistema.
        /// </summary>
        /// <param name="publication">Publicación.</param>
        /// <returns><c>True</c> en caso de que se haya eliminado correctamente y <c>False</c> en caso 
        /// contrario.</returns>
        public static bool DeletePublication(Publication publication)
        {
            if(ContainsPublicationInListPublications(publication))
            {
                string titlePublicationToDelete = publication.Title;
                List<Publication> listPublicationsEdit = new List<Publication>();

                foreach(Publication item in ListPublication)
                {
                    if(publication.Title != item.Title)
                    {
                        listPublicationsEdit.Add(item);  
                    }
                }
                File.WriteAllText(Path, "");
                
                foreach(Publication publicationToAdd in listPublicationsEdit)
                {
                    AddPublication(publicationToAdd);
                }
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Método que se encarga de retornar un string con el listado de publicaciones.
        /// </summary>
        /// <returns>Cadena de caracteres.</returns>
        public static string ReturnListPublications()
        {
            StringBuilder result = new StringBuilder("Publicaciones: \n");

            foreach (Publication publication in ListPublication)
            {
                result.Append($"{publication.Title} \n");
            }
            return result.ToString();
        }

        /// <summary>
        /// Método simple que se encarga de comprobar si una clase Publicación se encuentra
        /// en el sistema de Publicaciones.
        /// </summary>
        /// <param name="publication">Publicación.</param>
        /// <returns><c>True</c> en caso de encontrarse en el sistema y <c>False</c> en caso 
        /// contrario.</returns>
        public static bool ContainsPublicationInListPublications(Publication publication)
        {
            foreach(Publication item in ListPublication)
            {
                if(item.Title == publication.Title) return true;
            }
            return false;
        }
    }
}