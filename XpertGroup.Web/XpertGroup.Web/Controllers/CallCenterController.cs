using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XpertGroup.Dominio;
using XpertGroup.Dominio.Excepcion;
using XpertGroup.Dominio.ReglasDeNegocio;
using XpertGroup.Dominio.ReglasDeNegocio.Interfaces;
using XpertGroup.Dominio.Servicios;
using XpertGroup.Entidades;

namespace XpertGroup.Web.Controllers
{
    public class CallCenterController : Controller
    {
        private ICallCenterService _callCenterService;

        public CallCenterController(ICallCenterService callCenterService)
        {
            this._callCenterService = callCenterService;
        }

        public IActionResult Index()
        {
            return View(new List<Conversacion>());
        }

        public IActionResult UploadFile()
        {
            return View("Index",new List<Conversacion>());
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return Content("Archivo no seleccionado");

                var textoConversaciones = new List<string>();
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        textoConversaciones.Add(reader.ReadLine());

                    textoConversaciones.Add(string.Empty);
                }
                var conversaciones = this._callCenterService.CalcularCalificaciones(textoConversaciones);

                return View("Index", conversaciones);
            }
            catch
            {
                return View("Error");
            }
        }
    }
}