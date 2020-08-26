using System;
using System.Collections.Generic;
using System.Text;
using XpertGroup.Dominio.ReglasDeNegocio.Interfaces;
using XpertGroup.Entidades;

namespace XpertGroup.Dominio.ReglasDeNegocio
{
    /// <summary>
    /// Clase utilizada para calcular la regla de negocio: 
    /// Número de coincidencias de la palabra URGENTE por registro:
    /// • Si es menor o igual que 2 (- 5puntos).
    /// • Si es mayor que 2 (-10 puntos).
    /// </summary>
    public class CalcularCoincidenciasPalabraUrgente : IReglaConversacion
    {
        public int CalcularPuntos(List<Linea> lineas)
        {
            int coincidencias = 0;
            foreach (var item in lineas)
            {
                if (item.Mensaje.ToUpper().Contains("URGENTE"))
                    coincidencias++;
            }
            return coincidencias <= 2 ? -5 : -10;
        }
    }
}
