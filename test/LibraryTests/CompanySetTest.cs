using System;
using NUnit.Framework;
using Bot;

namespace BotTests
{
    /// <summary>
    /// Clase para testear la clase CompanySet.
    /// </summary>
    public class CompanySetTest
    {
        GeoLocation location = new GeoLocation("Universidad Católica", "Montevideo");
        Company companyTest1;
        Company companyTest2;
        Company companyTest3;

        [SetUp]
        public void SetUp()
        {
            companyTest1 = new Company("Prueba1", "Prueba", location, "0922877272");
            companyTest2 = new Company("Prueba1", "Prueba2", location, "0922877272");
            companyTest3 = new Company("Prueba2", "Prueba2", location, "0922877272");
        }

        /// <summary>
        /// Test que se encarga de testear el funcionamiento de la clase CompanySet de agregar
        /// clases Empresa al sistema.
        /// </summary>
        [Test]
        public void AddCompanyTest()
        {
            bool test1 = CompanySet.AddCompany(companyTest1);
            bool test2 = CompanySet.AddCompany(companyTest2);
            bool test3 = CompanySet.AddCompany(companyTest3);
            string stringCompaniesTest = CompanySet.ReturnListCompanies();

            Assert.IsTrue(test1);
            Assert.IsFalse(test2);
            Assert.IsTrue(test3);
            Assert.That(CompanySet.ListCompany.Count == 2);
            Assert.That(stringCompaniesTest.Contains("Prueba1"));
            Assert.That(stringCompaniesTest.Contains("Prueba2"));
        }

        /// <summary>
        /// Test que se encarga de testear el funcionamiento de la clase CompanySet de eliminar 
        /// clases Empresa del sistema.
        /// </summary>
        [Test]
        public void DeleteCompanyTest()
        {
            bool test4 = CompanySet.DeleteCompany(companyTest1);
            bool test5 = CompanySet.DeleteCompany(companyTest3);

            Assert.IsTrue(test4);
            Assert.IsTrue(test5);
            Assert.IsEmpty(CompanySet.ListCompany);
            Assert.IsFalse(CompanySet.ContainsCompanyInListCompanies(companyTest1));
            Assert.IsFalse(CompanySet.ContainsCompanyInListCompanies(companyTest2));
        }
    }
}