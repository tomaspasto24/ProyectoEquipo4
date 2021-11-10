using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace Bot
{
    /// <summary>
    /// Conjunto de Empresas, clase estática que administra la lista de Empresas en general.
    /// </summary>
    public static class CompanySet 
    {
    private const string path = @"C:\Users\Tomás\OneDrive - Universidad Católica del Uruguay\Programación II\Ejercicios\PII_2021_2_Equipo4\docs\CompanySet.txt";
        /// <summary>
        /// Obtiene la lista de Empresas, esto para que la clase Búsqueda pueda 
        /// manipular eficientemente las Publicaciones.
        /// </summary>
        /// <value></value>
        public static IReadOnlyCollection<Company> ListCompany
        {
            get
            {
                List<Company> listCompanies = new List<Company>();
                Company company;
                try
                {
                    using(StreamReader txtReader = new StreamReader(path))
                    {
                        string line = txtReader.ReadLine();
                        string name;
                        string item;
                        GeoLocation location;
                        string contact;

                        while (line != null)
                        {
                            name = JsonSerializer.Deserialize<Company>(line).Name;
                            item = JsonSerializer.Deserialize<Company>(line).Item;
                            contact = JsonSerializer.Deserialize<Company>(line).Contact;
                            location = JsonSerializer.Deserialize<Company>(line).Location;
                            company = new Company(name, item, location, contact);
                            listCompanies.Add(company);
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
        /// <returns><c>True</c> en caso de que se pueda agregar y <c>False</c> en caso 
        /// contrario.</returns>
        public static bool AddCompany(string name, string item, GeoLocation location, string contact)
        {
            Company company = new Company(name, item, location, contact);

            if(!ContainsCompanyInListCompanies(company))
            {
                string jsonCompany = JsonSerializer.Serialize(company);

                using(StreamWriter txtWrite = new StreamWriter(path, true))
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
        /// Sobrecarga del método AddCompany que permite ingresar un objeto Empresa como parámetro
        /// para ser ingresado al sistema.
        /// </summary>
        /// <param name="company">Empresa.</param>
        /// <returns><c>True</c> en caso de que se pueda agregar y <c>False</c> en caso 
        /// contrario.</returns>
        public static bool AddCompany(Company company)
        {
            if(!ContainsCompanyInListCompanies(company))
            {
                string jsonCompany = JsonSerializer.Serialize(company);

                using(StreamWriter txtWrite = new StreamWriter(path, true))
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
        /// Método que se encarga de eliminar una Empresa de la lista de Empresas del sistema.
        /// </summary>
        /// <param name="company">Empresa.</param>
        /// <returns><c>True</c> en caso de que se haya eliminado correctamente y <c>False</c> en caso 
        /// contrario.</returns>
        public static bool DeleteCompany(Company company)
        {
            if(ContainsCompanyInListCompanies(company))
            {
                string nameCompanyToDelete = company.Name;
                List<Company> listCompaniesEdit = new List<Company>();

                foreach(Company item in ListCompany)
                {
                    if(company.Name != item.Name)
                    {
                        listCompaniesEdit.Add(item);  
                    }
                }
                File.WriteAllText(path, "");
                
                foreach(Company companyToAdd in listCompaniesEdit)
                {
                    AddCompany(companyToAdd);
                }
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Método que retorna la lista completa de Empresas en un string con sus respectivos
        /// índices.
        /// </summary>
        /// <returns>String con el nombre de la Empresa y sus indices.</returns>
        public static string ReturnListCompanies()
        {
            StringBuilder resultado = new StringBuilder("Empresas: \n");

            foreach (Company company in ListCompany)
            {
                resultado.Append($"{company.Name} \n");
            }
            return resultado.ToString();
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
            foreach(Company item in ListCompany)
            {
                if(item.Name == company.Name) return true;
            }
            return false;
        }
    }
}