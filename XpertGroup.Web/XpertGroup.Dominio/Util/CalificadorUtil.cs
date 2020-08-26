using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using XpertGroup.Dominio.Excepcion;
using XpertGroup.Dominio.ReglasDeNegocio.Interfaces;
using XpertGroup.Dominio.Util;
using XpertGroup.Entidades;

namespace XpertGroup.Dominio
{
    /// <summary>
    /// Clase utilizada para realizar los calculos de los puntos y calificaciones de las conversaciones
    /// </summary>
    public class CalificadorUtil
    {

        /// <summary>
        /// Metodo que permite validar que el encabezado tenga el formato correcto
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static bool ValidarNombreEncabezadoConversacion(string texto)
        {
            var pattern = @"^(CONVERSACION \d)";
            var regex = new Regex(pattern);
            var match = regex.Match(texto);

            if (!match.Success)
            {
                Trazabilidad.Instancia.LogArchivoPlano.Error(string.Concat("Error validando el encabezado de la conversacion: ", texto));
            }
            return match.Success;
        }


        /// <summary>
        /// Metodo que permite validar que la linea de una conversacion tenga el formato correcto
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static Linea CargarLineaConversacion(string texto, string conversacion)
        {
            Linea linea = new Linea();
            var pattern = @"^(?<horas>[01]\d|2[0-3]):(?<minutos>[0-5]\d):(?<segundos>[0-5]\d) (?<emisor>CLIENTE\d|ASESOR\d): (?<mensaje>.*$)";
            var regex = new Regex(pattern);
            var match = regex.Match(texto);

            if (match.Success)
            {
                linea.Emisor = match.Groups["emisor"].ToString();
                linea.Mensaje = match.Groups["mensaje"].ToString();
                linea.Fecha = new DateTime(2000, 01, 01, Convert.ToInt32(match.Groups["horas"].ToString())
                    , Convert.ToInt32(match.Groups["minutos"].ToString()), Convert.ToInt32(match.Groups["segundos"].ToString()));
                return linea;
            }
            else
            {
                Trazabilidad.Instancia.LogArchivoPlano.Error(string.Concat("Error validando el formato del mensaje: '", texto, "' de la conversacion: ", conversacion));
                return null;
            }
        }

        /// <summary>
        /// Metodo que permite calcular la calificacion de una conversacion de acuerdo a sus puntos acumulados
        /// </summary>
        /// <param name="puntos"></param>
        /// <returns></returns>
        public static int CalcularCalificacionConversacion(int puntos)
        {
            var resultado = puntos switch
            {
                var x when (x < 0) => 0,
                var x when (x <= 25) => 1,
                var x when (x > 25 && x <= 50) => 2,
                var x when (x > 50 && x <= 75) => 3,
                var x when (x > 75 && x <= 90) => 4,
                var x when (x > 90) => 5,
                _ => throw new ArgumentException("Error al calcular puntuacion"),
            };
            return resultado;
        }

        /// <summary>
        /// Metodo que permite calular los puntos de una conversacion de acuerdo a las reglas de negocio que se hayan configurado
        /// </summary>
        /// <param name="conversacion"></param>
        /// <param name="reglasDeNegocio"></param>
        /// <returns></returns>
        public static int CalcularPuntosConversacion(Conversacion conversacion, List<IReglaConversacion> reglasDeNegocio)
        {
            int puntosAcumulados = 0, puntos = 0;

            foreach (var regla in reglasDeNegocio)
            {
                puntos = regla.CalcularPuntos(conversacion.Lineas);
                puntosAcumulados += puntos;

                //EXCELENTE SERVICIO o Conversacion abandonada
                if (puntos == 100 || puntos == -100)
                    break;
            }

            return puntosAcumulados;
        }


    }
}
