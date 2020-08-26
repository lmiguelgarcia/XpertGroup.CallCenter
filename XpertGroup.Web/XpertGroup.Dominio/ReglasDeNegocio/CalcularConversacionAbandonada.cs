using System;
using System.Collections.Generic;
using System.Text;
using XpertGroup.Dominio.ReglasDeNegocio.Interfaces;
using XpertGroup.Entidades;

namespace XpertGroup.Dominio.ReglasDeNegocio
{
    /// <summary>
    /// Clase utilizada para calcular la regla de negocio: 
    /// Se categoriza una conversación como abandonada cuando sólo tiene 1 línea de conversación debido a que el asesor nunca dio respuesta alguna(-100 puntos).
    /// </summary>
    public class CalcularConversacionAbandonada : IReglaConversacion
    {
        public int CalcularPuntos(List<Linea> lineas)
        {
            return lineas.Count == 1 ? -100 : 0;
        }
    }
}
