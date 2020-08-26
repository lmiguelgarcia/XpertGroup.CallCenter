using System;
using System.Collections.Generic;
using System.Text;
using XpertGroup.Entidades;

namespace XpertGroup.Dominio.Servicios
{
    public interface ICallCenterService
    {
        List<Conversacion> CalcularCalificaciones(List<string> textoConversaciones);
    }
}
