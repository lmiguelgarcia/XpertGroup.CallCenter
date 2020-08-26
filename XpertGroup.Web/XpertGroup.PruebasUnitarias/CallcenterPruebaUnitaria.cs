using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using XpertGroup.Dominio;
using XpertGroup.Dominio.ReglasDeNegocio;
using XpertGroup.Dominio.ReglasDeNegocio.Interfaces;
using XpertGroup.Entidades;

namespace XpertGroup.PruebasUnitarias
{
    [TestClass]
    public class CallcenterPruebaUnitaria
    {
        [TestMethod]
        public void ValidarNombreEncabezadoConversacion_OK()
        {
            //Arange
            StringBuilder texto = new StringBuilder();
            texto.Append("CONVERSACION 1");

            //Act
            bool resultado = CalificadorUtil.ValidarNombreEncabezadoConversacion(texto.ToString());

            //Asert
            Assert.AreEqual(true, resultado);
        }

        [TestMethod]
        public void ValidarNombreEncabezadoConversacion_ErrorFormato()
        {
            //Arange
            StringBuilder texto = new StringBuilder();
            texto.Append("CONVERSACIONES 1");

            //Act
            bool resultado = CalificadorUtil.ValidarNombreEncabezadoConversacion(texto.ToString());

            //Asert
            Assert.AreEqual(false, resultado);
        }

        [TestMethod]
        public void CargarLineaConversacion_OK()
        {
            //Arange
            StringBuilder texto = new StringBuilder();
            texto.Append("11:58:59 ASESOR3: Con gusto, atenderemos su solicitud.");

            //Act
            Linea linea = CalificadorUtil.CargarLineaConversacion(texto.ToString(), "CONVERSACION 1");

            //Asert
            Assert.IsNotNull(linea);
        }

        [TestMethod]
        public void CargarLineaConversacion_ErrorFormato()
        {
            //Arange
            StringBuilder texto = new StringBuilder();
            texto.Append("11:58:59 ASESORES3: Con gusto, atenderemos su solicitud.");

            //Act
            Linea linea = CalificadorUtil.CargarLineaConversacion(texto.ToString(), "CONVERSACION 1");

            //Asert
            Assert.IsNull(linea);
        }

        [TestMethod]
        public void CalcularCalificacionConversacion_OK()
        {
            //Arange
            int puntos = 20;

            //Act
            int calificacion = CalificadorUtil.CalcularCalificacionConversacion(puntos);

            //Asert
            Assert.AreEqual(1, calificacion);
        }

        [TestMethod]
        public void CalcularPuntosConversacion_OK()
        {
            //Arange
            Conversacion conversacion = new Conversacion();
            conversacion.Nombre = "CONVERSACION 1";
            conversacion.Lineas = new System.Collections.Generic.List<Linea>()
            {
                new Linea(){ Emisor="CLIENTE1", Mensaje="Hola, buenos dias.", Fecha=DateTime.Now },
                new Linea(){ Emisor="ASESOR1", Mensaje="Hola CLIENTE1, bienvenido al centro de servicio.", Fecha=DateTime.Now }
            };

            List<IReglaConversacion> reglasDeNegocio = new List<IReglaConversacion>()
            {
                new CalcularConversacionAbandonada(),
                new CalcularBuenServicio(),
                new CalcularCoincidenciasPalabraUrgente(),
                new CalcularNumeroMensajes()
            };

            //Act
            int puntos = CalificadorUtil.CalcularPuntosConversacion(conversacion, reglasDeNegocio);

            //Asert
            Assert.AreEqual(15, puntos);
        }

        /// <summary>
        /// Metodo que valida que devuelve el numero de conversaciones correctas
        /// </summary>
        [TestMethod]
        public void ObtenerConversaciones_TotalCorrectas()
        {
            //Arange
            List<string> texto = new List<string>();
            texto.Add("CONVERSACION 1");
            texto.Add("11:51:00 CLIENTE1: Hola");
            texto.Add("11:51:05 ASESOR1: Hola CLIENTE1, bienvenido al centro de servicio.");
            texto.Add("");
            texto.Add("CONVERSACION 2");
            texto.Add("11:55:00 CLIENTE2: Hola");
            texto.Add("");


            List<IReglaConversacion> reglasDeNegocio = new List<IReglaConversacion>()
            {
                new CalcularConversacionAbandonada(),
                new CalcularBuenServicio(),
                new CalcularCoincidenciasPalabraUrgente(),
                new CalcularNumeroMensajes()
            };
            CallCenter _callCenterDominio = new CallCenter(reglasDeNegocio);

            //Act
            List<Conversacion> conversaciones = _callCenterDominio.Ejecutar(texto);

            //Asert
            Assert.AreEqual(2, conversaciones.Count);
        }

        /// <summary>
        /// En este metodo se prueba enviando una conversacion con formato ok y otra con formato mal
        /// por lo tanto se valida que devuelva una unica conversacion correcta
        /// </summary>
        [TestMethod]
        public void ObtenerConversaciones_TotalConErrorFormato()
        {
            //Arange
            List<string> texto = new List<string>();
            texto.Add("CONVERSACIONES 1");
            texto.Add("11:51:00 CLIENTE1: Hola");
            texto.Add("11:51:05 ASESOR1: Hola CLIENTE1, bienvenido al centro de servicio.");
            texto.Add("");
            texto.Add("CONVERSACION 2");
            texto.Add("11:55:00 CLIENTE2: Hola");
            texto.Add("");

            List<IReglaConversacion> reglasDeNegocio = new List<IReglaConversacion>()
            {
                new CalcularConversacionAbandonada(),
                new CalcularBuenServicio(),
                new CalcularCoincidenciasPalabraUrgente(),
                new CalcularNumeroMensajes()
            };
            CallCenter _callCenterDominio = new CallCenter(reglasDeNegocio);

            //Act
            List<Conversacion> conversaciones = _callCenterDominio.Ejecutar(texto);

            //Asert
            Assert.AreEqual(1, conversaciones.Count);
        }

    }
}
