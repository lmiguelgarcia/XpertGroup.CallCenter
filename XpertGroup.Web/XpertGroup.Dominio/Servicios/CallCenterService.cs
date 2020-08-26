using System;
using System.Collections.Generic;
using System.Text;
using XpertGroup.Dominio.ReglasDeNegocio;
using XpertGroup.Dominio.ReglasDeNegocio.Interfaces;
using XpertGroup.Entidades;

namespace XpertGroup.Dominio.Servicios
{
    public class CallCenterService : ICallCenterService
    {
        public List<Conversacion> CalcularCalificaciones(List<string> textoConversaciones)
        {

            List<IReglaConversacion> reglasDeNegocio = new List<IReglaConversacion>()
            {
                new CalcularConversacionAbandonada(),
                new CalcularBuenServicio(),
                new CalcularCoincidenciasPalabraUrgente(),
                new CalcularNumeroMensajes(),
                new CalcularDuracionLlamada()
            };

            CallCenter _callCenterDominio = new CallCenter(reglasDeNegocio);

            return _callCenterDominio.Ejecutar(textoConversaciones);
        }
    }
}
