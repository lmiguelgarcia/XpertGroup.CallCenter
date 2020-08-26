using System;
using System.Collections.Generic;
using System.Text;
using XpertGroup.Dominio.ReglasDeNegocio.Interfaces;
using XpertGroup.Entidades;

namespace XpertGroup.Dominio.ReglasDeNegocio
{
    /// <summary>
    /// Clase utilizada para calcular la regla de negocio: 
    /// Obtener el número de mensajes enviados en la conversación, se identifican por un salto de línea [enter], los puntos se redistribuyen de la siguiente manera:
    /// • Si es menor o igual a 5 (20 puntos).
    /// • Si es mayor que 5 (10 puntos).
    /// </summary>
    public class CalcularNumeroMensajes : IReglaConversacion
    {
        public int CalcularPuntos(List<Linea> lineas)
        {
            return lineas.Count <= 5 ? 20 : 10;
        }
    }
}