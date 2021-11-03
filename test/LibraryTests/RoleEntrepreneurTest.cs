using Bot;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BotTests
{
    public class RoleEntrepreneurTests
    {
        RoleEntrepreneur emprendedor;
        GeoLocation location;
        string name;
        int id;
        string heading;
        string certification;
        string specialization;
        [SetUp]
        public void Setup()
        {
            name = "Alejandra";
            id = 1;
            heading = "";
            GeoLocation location = new GeoLocation("8 de OCtubre", "Blanqueada", "Mdeo");
            certification = "";
            specialization = "";

            emprendedor = new RoleEntrepreneur(name, id, heading, location, certification, specialization);
        }

        [Test]
        public void AddCertificationTest()
        {
            certification = "Manejar expliosivos";
            emprendedor.AddCertification(certification);
            Assert.AreEqual(emprendedor.ReturnCertification(), "Manejar explisivos");
        }

        [Test]
        public void AddSpecializationTest()
        {
            specialization = "Quimica";
            emprendedor.AddSpecialization(specialization);
            Assert.AreEqual(emprendedor.ReturnSpecialization(), "Quimica");
        }
    }
}