using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Bot
{
    /// <summary>
    /// Conjunto de Empresas, clase estática que administra la lista de Empresas en general.
    /// </summary>
    public static class CompanySet
    {
        private const string Path = @"..\..\..\..\..\docs\CompanyDataBase.json";

        /// <summary>
        /// Obtiene la lista de Empresas, esto para que la clase Búsqueda pueda manipular eficientemente las Publicaciones.
        /// </summary>
        /// <value>Clase Empresa.</value>
        public static IReadOnlyCollection<Company> ListCompany
        {
            get
            {
                List<Company> listCompanies = new List<Company>();
                Company company;
                using (StreamReader txtReader = new StreamReader(Path))
                {
                    string line = txtReader.ReadLine();
                    string name;
                    string item;
                    GeoLocation location;
                    string contact;
                    IReadOnlyList<Publication> listHistorialPublications;
                    IReadOnlyList<Publication> listOwnPublications;
                    IReadOnlyList<User> listUsers;

                    while (line != null)
                    {
                        name = JsonSerializer.Deserialize<Company>(line).Name;
                        item = JsonSerializer.Deserialize<Company>(line).Item;
                        contact = JsonSerializer.Deserialize<Company>(line).Contact;
                        location = JsonSerializer.Deserialize<Company>(line).Location;

                        listHistorialPublications = JsonSerializer.Deserialize<Company>(line).ListHistorialPublications;
                        listOwnPublications = JsonSerializer.Deserialize<Company>(line).ListOwnPublications;
                        listUsers = JsonSerializer.Deserialize<Company>(line).ListUsers;
                        company = new Company(name, item, location, contact);
                        company.AddListHistorialPublication(listHistorialPublications);
                        company.AddOwnPublication(listOwnPublications);
                        company.AddUser(listUsers);

                        listCompanies.Add(company);
                        line = txtReader.ReadLine();
                    }

                    txtReader.Close();
                    txtReader.Dispose();
                }

                return listCompanies.AsReadOnly();
            }
        }

        /// <summary>
        /// Método que se encarga de agregar una Empresa a la lista de Empresas del sistema.
        /// </summary>
        /// <param name="name">Nombre de Empresa.</param>
        /// <param name="item">Rubro de Empresa.</param>
        /// <param name="location">Ubicación de Empresa.</param>
        /// <param name="contact">Contacto de Empresa.</param>
        /// <param name="listUsers">Lista de Users.</param>
        /// <param name="listOwnPublications">Lista propia de publicaciones.</param>
        /// <param name="listHistorialPublications">Lista Historial de Publicaciones.</param>
        /// <returns><c>True</c> en caso de que se pueda agregar y <c>False</c> en caso
        /// contrario.</returns>
        public static bool AddCompany(string name, string item, GeoLocation location, string contact, IReadOnlyList<User> listUsers, IReadOnlyList<Publication> listOwnPublications, IReadOnlyList<Publication> listHistorialPublications)
        {
            Company company = new Company(name, item, location, contact);
            company.AddUser(listUsers);
            company.AddOwnPublication(listOwnPublications);
            company.AddListHistorialPublication(listHistorialPublications);

            if (!ContainsCompanyInListCompanies(company))
            {
                string jsonCompany = JsonSerializer.Serialize(company);

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
        /// Sobrecarga del método AddCompany que permite ingresar un objeto Empresa como parámetro
        /// para ser ingresado al sistema.
        /// </summary>
        /// <param name="company">Empresa.</param>
        /// <returns><c>True</c> en caso de que se pueda agregar y <c>False</c> en caso contrario.</returns>
        public static bool AddCompany(Company company)
        {
            if (!ContainsCompanyInListCompanies(company))
            {
                string jsonCompany = JsonSerializer.Serialize(company);

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
        /// Método que se encarga de eliminar una Empresa de la lista de Empresas del sistema.
        /// </summary>
        /// <param name="company">Empresa.</param>
        /// <returns><c>True</c> en caso de que se haya eliminado correctamente y <c>False</c> en caso
        /// contrario.</returns>
        public static bool DeleteCompany(Company company)
        {
            if (company != null)
            {
                if (ContainsCompanyInListCompanies(company))
                {
                    string nameCompanyToDelete = company.Name;
                    List<Company> listCompaniesEdit = new List<Company>();

                    foreach (Company item in ListCompany)
                    {
                        if (company.Name != item.Name)
                        {
                            listCompaniesEdit.Add(item);
                        }
                    }

                    File.WriteAllText(Path, string.Empty);

                    foreach (Company companyToAdd in listCompaniesEdit)
                    {
                        AddCompany(companyToAdd);
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
                throw new ArgumentNullException(nameof(company));
            }
        }

        /// <summary>
        /// Método que retorna la lista completa de Empresas en un string con sus respectivos
        /// índices.
        /// </summary>
        /// <returns>String con el nombre de la Empresa y sus indices.</returns>
        public static string ReturnListCompanies()
        {
            StringBuilder result = new StringBuilder("Empresas: \n");

            foreach (Company company in ListCompany)
            {
                result.Append($"{company.Name} \n");
            }

            return result.ToString();
        }

        /// <summary>
        /// Método simple que se encarga de comprobar si una clase Empresa se encuentra
        /// en el sistema de Empresas.
        /// </summary>
        /// <param name="company">Empresa.</param>
        /// <returns><c>True</c> en caso de encontrarse en el sistema y <c>False</c> en caso
        /// contrario.</returns>
        public static bool ContainsCompanyInListCompanies(Company company)
        {
            if (company != null)
            {
                foreach (Company item in ListCompany)
                {
                    if (item.Name == company.Name)
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                throw new ArgumentNullException(nameof(company));
            }
        }
    }
}