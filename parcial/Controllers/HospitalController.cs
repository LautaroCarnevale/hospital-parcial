using Microsoft.AspNetCore.Mvc;
using parcial.Data;
using parcial.Models;

namespace parcial.Controllers
{
    public class HospitalController : Controller
    {
        BaseDeDatos bd = new BaseDeDatos();

        public ActionResult ListarPacientes()
        {

            return View(bd.ListarPacientes());
        }

        public ActionResult CountObrasSociales()
        {
            int osdeCount = 0;
            int aprossCount = 0;
            int pamiCount = 0;
            int otroCount = 0;

            foreach (var item in bd.ListarPacientes())
            {
                if (item.ObraSocial.Nombre == "OSDE")
                {
                    osdeCount++;
                }
                else if (item.ObraSocial.Nombre == "APROSS")
                {
                    aprossCount++;
                }
                else if (item.ObraSocial.Nombre == "PAMI")
                {
                    pamiCount++;
                }
                else
                {
                    otroCount++;
                }
            }

            @ViewBag.osdeCount = osdeCount;
            @ViewBag.aprossCount = aprossCount;
            @ViewBag.pamiCount = pamiCount;
            @ViewBag.otroCount = otroCount;

            return View();
        }
        public ActionResult CantidadDePacientes()
        {
            return View(bd.ListarPacientes());
        }

        public ActionResult RegistrarPaciente()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarPaciente(IFormCollection collection)
        {
            try
            {
                Paciente p = new Paciente();

                // validaciones 
                if (string.IsNullOrEmpty(collection["Nombre"]))
                {
                    return View();
                }

                if (string.IsNullOrEmpty(collection["IdObraSocial"]))
                {
                    return View();
                }

                int edad;
                if (!(int.TryParse(collection["Edad"], out edad) && edad >= 0))
                {
                    @ViewBag.Edad = "La edad tiene que ser mayor o igual a 0 y un número";

                    return View();
                }


                if (string.IsNullOrEmpty(collection["Sintomas"]))
                {
                    return View();
                }

                p.Nombre = collection["Nombre"];
                p.IdObraSocial = collection["IdObraSocial"];
                p.Edad = edad;
                p.Sintomas = collection["Sintomas"];

                bd.CrearPaciente(p);
                return RedirectToAction(nameof(ListarPacientes));
            }
            catch
            {
                return View();
            }
        }
    }
}
