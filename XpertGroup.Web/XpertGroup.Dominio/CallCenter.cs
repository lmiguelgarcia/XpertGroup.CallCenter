using System;
using System.Collections.Generic;
using System.Text;
using XpertGroup.Dominio.Excepcion;
using XpertGroup.Dominio.ReglasDeNegocio.Interfaces;
using XpertGroup.Dominio.Util;
using XpertGroup.Entidades;

namespace XpertGroup.Dominio
{
    /// <summary>
    /// Clase que contiene la logica que permite cargar las conversaciones y calcular las calificaciones
    /// </summary>
    public class CallCenter
    {
        private readonly List<Conversacion> _conversaciones;
        private readonly List<IReglaConversacion> _reglasDeNegocio;

        public CallCenter(List<IReglaConversacion> reglasDeNegocio)
        {
            _conversaciones = new List<Conversacion>();
            _reglasDeNegocio = reglasDeNegocio;
        }


        public List<Conversacion> Ejecutar(List<string> textoConversaciones)
        {
            try
            {
                this.CargarConversaciones(textoConversaciones);
                this.EvaluarConversaciones();
                return this._conversaciones;
            }
            catch (Exception ex)
            {
                string mensaje = "Se produjo un error al ejecutar la evaluacion de las conversaciones";
                Trazabilidad.Instancia.LogArchivoPlano.Error(mensaje, ex);
                throw new CallCenterExcepcion(mensaje, ex);
            }
        }

        /// <summary>
        /// Metodo que permite evaluar cada una de las conversaciones y calcular tanto los puntos como la calificacion correspondiente
        /// </summary>
        private void EvaluarConversaciones()
        {
            foreach (var conversacion in _conversaciones)
            {
                int puntos = CalificadorUtil.CalcularPuntosConversacion(conversacion, _reglasDeNegocio);
                int calificacion = CalificadorUtil.CalcularCalificacionConversacion(puntos);

                conversacion.Puntos = puntos;
                conversacion.Calificacion = calificacion;
            }
        }

        /// <summary>
        /// Metodo que permite tomar el texto de las conversaciones y cargarlo en la lista de objetos Conversacion
        /// </summary>
        /// <param name="lineas">texto del archivo de conversaciones</param>
        private void CargarConversaciones(List<string> textoConversaciones)
        {
            Conversacion conversacion = null;
            for (int i = 0; i < textoConversaciones.Count; i++)
            {
                //Validar encabezado y crear conversacion
                if (textoConversaciones[i].ToUpper().Contains("CONVERSACION") &&
                    CalificadorUtil.ValidarNombreEncabezadoConversacion(textoConversaciones[i]))
                {
                    conversacion = new Conversacion()
                    {
                        Nombre = textoConversaciones[i]
                    };
                    continue;
                }

                //Validar si se termino una conversacion y adicionarla a la lista de conversaciones 
                //(el espacio en el archivo determina donde finaliza y donde comienzan las conversaciones)
                if (string.IsNullOrEmpty(textoConversaciones[i]))
                {
                    if (conversacion != null)
                    {
                        _conversaciones.Add(conversacion);
                        conversacion = null;
                    }
                    continue;
                }

                //Adicionar linea a la conversacion que se esta recorriendo
                if (conversacion != null)
                {
                    //se valida si el mensaje tiene el formato correcto y se adiciona a la conversacion, de lo contrario la conversacion sera omitida
                    Linea linea = CalificadorUtil.CargarLineaConversacion(textoConversaciones[i], conversacion.Nombre);
                    if (linea != null)
                        conversacion.Lineas.Add(linea);
                    else
                    {
                        conversacion = null;
                    }
                }

            }
        }

    }
}
