using System;
using System.Collections.Generic;
using System.Text;
using XpertGroup.Dominio.ReglasDeNegocio.Interfaces;
using XpertGroup.Entidades;
using System.Linq;

namespace XpertGroup.Dominio.ReglasDeNegocio
{
    /// <summary>
    /// Clase utilizada para calcular la regla de negocio: 
    /// Lista de palabras que exclaman el buen servicio en la conversación:
    /// • Si coincide la palabra EXCELENTE SERVICIO(100 puntos) y no continúa con la calificación del servicio.
    /// • Si existe alguna coincidencia en la lista de palabras (10 puntos).
    /// </summary>
    public class CalcularBuenServicio : IReglaConversacion
    {
        public int CalcularPuntos(List<Linea> lineas)
        {
            int puntos = 0;
            string[] palabras = new[] { "GRACIAS", "BUENA ATENCION", "MUCHAS GRACIAS" };

            foreach (var item in lineas)
            {
                if (item.Mensaje.ToUpper().Contains("EXCELENTE SERVICIO"))
                    return 100;

                if (palabras.Contains(item.Mensaje.ToUpper()))
                    puntos += 10;

            }
            return puntos;
        }
    }
}
