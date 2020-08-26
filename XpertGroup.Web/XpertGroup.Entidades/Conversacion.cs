using System;
using System.Collections.Generic;

namespace XpertGroup.Entidades
{
    public class Conversacion
    {
        public string Nombre { get; set; }
        public int Calificacion { get; set; }
        public int Puntos { get; set; }
        public List<Linea> Lineas { get; set; }

        public Conversacion()
        {
            Lineas = new List<Linea>();
        }
    }
}
