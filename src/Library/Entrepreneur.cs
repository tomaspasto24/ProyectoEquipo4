using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class Entrepreneur: Usuario
    {
        private static List<Entrepreneur> entrepreneurs = new List<Entrepreneur>();
        private static int entrepreneurAccountant = 0;
        private string name;
        private GeoLocation geolocation;
        public string heading;
        private List<string> certification;
        private List<string> specializations;

        public Entrepreneur(string name, string heading, GeoLocation geolocation, string certification, string specializations)
        {
            this.certification = certification;
            this.specializations = specializations;
            this.name = name;
            this.geolocation = geolocation;
            this.heading = heading;
            entrepreneurAccountant++;
        }

        public string GetReport()
        {
            return ($"Nombre: {name}....")
        }
        public void RegisterEntrepreneur(Entrepreneur entrepreneur)
        {
            entrepreneurs.Add(entrepreneur);
        }
        public List<Publication> SearchingMaterials()
        {
            return 
        }
        public void Deleated(Entrepreneur entrepreneur)
        {
            entrepreneur.Remove(Entrepreneur entrepreneur)
        }

    }
}