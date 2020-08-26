using System;
using System.Collections.Generic;
using System.Text;
using XpertGroup.Entidades;

namespace XpertGroup.Dominio.ReglasDeNegocio.Interfaces
{
    public interface IReglaConversacion
    {
        int CalcularPuntos(List<Linea> lineas);
    }
}
