using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

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
            using (StreamReader txtReader = new StreamReader(Path))
            {
                string line = txtReader.ReadLine();
                string title;
                Company company;
                GeoLocation location;
                IReadOnlyList<Material> listMaterials;
                IReadOnlyList<string> listQualifications;

                while (line != null)
                {
                    title = JsonSerializer.Deserialize<Publication>(line).Title;
                    company = JsonSerializer.Deserialize<Publication>(line).Company;
                    location = JsonSerializer.Deserialize<Publication>(line).Location;

                    listMaterials = JsonSerializer.Deserialize<Publication>(line).ListMaterials;
                    listQualifications = JsonSerializer.Deserialize<Publication>(line).ListQualifications;

                    publication = new Publication(title, company, location, listMaterials[0]);
                    publication.AddMaterial(listMaterials);
                    publication.AddQualification(listQualifications);

                    listPublications.Add(publication);
                    line = txtReader.ReadLine();
                }

                txtReader.Close();
                txtReader.Dispose();
            }

            return listPublications.AsReadOnly();
            }
        }

        /// <summary>
        /// Método que se encarga de agregar una clase Publicación al sistema para que pueda persistir aunque el bot caiga.
        /// </summary>
        /// <param name="title">Titulo.</param>
        /// <param name="company">Empresa.</param>
        /// <param name="location">Ubicación.</param>
        /// <param name="material">Material.</param>
        /// <param name="listMaterials">Lista Material.</param>
        /// <param name="listQualifications">Lista Habilitaciones.</param>
        /// <returns><c>True</c> en caso de que se pueda agregar y <c>False</c> en caso contrario.</returns>
    public static bool AddPublication(String title, Company company, GeoLocation location, Material material, IReadOnlyList<Material> listMaterials, IReadOnlyList<string> listQualifications)
        {
            Publication publication = new Publication(title, company, location, material);
            publication.AddMaterial(listMaterials);
            publication.AddQualification(listQualifications);

            if (!ContainsPublicationInListPublications(publication))
            {
                string jsonCompany = JsonSerializer.Serialize(publication);
                using (StreamWriter txtWrite = new StreamWriter(Path, true))
                {
                    txtWrite.WriteLine(jsonCompany);
                    txtWrite.Close();
                    txtWrite.Dispose();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sobrecarga del método AddCompany que permite ingresar un objeto Publicación como parámetropara ser ingresado al sistema.
        /// </summary>
        /// <param name="publication"></param>
        /// <returns><c>True</c> en caso de que se pueda agregar y <c>False</c> en caso contrario.</returns>
    public static bool AddPublication(Publication publication)
        {
            if (!ContainsPublicationInListPublications(publication))
            {
                string jsonPublication = JsonSerializer.Serialize(publication);
                using (StreamWriter txtWrite = new StreamWriter(Path, true))
                {
                    txtWrite.WriteLine(jsonPublication);
                    txtWrite.Close();
                    txtWrite.Dispose();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método que elimina una clase Publicación del sistema.
        /// </summary>
        /// <param name="publication">Publicación.</param>
        /// <returns><c>True</c> en caso de que se haya eliminado correctamente y <c>False</c> en caso
        /// contrario.</returns>
    public static bool DeletePublication(Publication publication)
    {
        if (publication != null)
        {
            if (ContainsPublicationInListPublications(publication))
            {
                string titlePublicationToDelete = publication.Title;
                List<Publication> listPublicationsEdit = new List<Publication>();

                foreach (Publication item in ListPublication)
                {
                    if (publication.Title != item.Title)
                    {
                        listPublicationsEdit.Add(item);
                    }
                }

                File.WriteAllText(Path, string.Empty);

                foreach (Publication publicationToAdd in listPublicationsEdit)
                {
                    AddPublication(publicationToAdd);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            throw new ArgumentNullException(nameof(publication));
        }
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
    /// <returns><c>True</c> en caso de encontrarse en el sistema y <c>False</c> en caso contrario.</returns>
    public static bool ContainsPublicationInListPublications(Publication publication)
    {
        if (publication != null)
        {
            foreach (Publication item in ListPublication)
            {
                if (item.Title == publication.Title)
                {
                    return true;
                }
            }

            return false;
        }
        else
        {
            throw new ArgumentNullException(nameof(publication));
        }
    }
    }
}