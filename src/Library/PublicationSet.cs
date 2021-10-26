using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    public class ConjuntoPublicaciones
    {
        private List<Publicacion> listaPublicaciones;

        public void AgregarPublicacion(Publicacion publicacion)
        {
            listaPublicaciones.Add(publicacion);
        } 

        public bool EliminarPublicacion(int indicePublicacion)
        {
            return listaPublicaciones.Remove(listaPublicaciones[indicePublicacion]);
        }

        public string DevolverListaMateriales()
        {
            StringBuilder resultado = new StringBuilder("Publicaciones: \n");
            int contador = 0;

            foreach(Publicacion publicacion in this.listaPublicaciones)
            {
                resultado.Append($"{++contador}- {publicacion} \n");
            }
            return resultado.ToString();
        }
    }