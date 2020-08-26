using System;
using System.Collections.Generic;
using System.Text;
using XpertGroup.Dominio.ReglasDeNegocio.Interfaces;
using XpertGroup.Entidades;

namespace XpertGroup.Dominio.ReglasDeNegocio
{
    /// <summary>
    /// Clase utilizada para calcular la regla de negocio: 
    /// Cuánto tiempo duró la conversación expresada en minutos y segundos:
    /// • Si es menor de 1 minuto(50 puntos).
    /// • Si es mayor o igual a 1 minuto(25 puntos).
    /// </summary>
    public class CalcularDuracionLlamada : IReglaConversacion
    {
        public int CalcularPuntos(List<Linea> lineas)
        {
            DateTime fechaIni = lineas[0].Fecha;
            DateTime fechaFin = lineas[lineas.Count - 1].Fecha;

            TimeSpan ts = fechaFin - fechaIni;
            return ts.TotalMinutes <= 1 ? 50 : 25;
        }
    }
}
