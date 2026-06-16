using ClinicaJordan.Web.Data;
using ClinicaJordan.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaJordan.Web.Controllers
{
    public class EspecialidadController : Controller
    {
        private readonly EspecialidadRepository _especialidadRepository;

        public EspecialidadController(EspecialidadRepository especialidadRepository)
        {
            _especialidadRepository = especialidadRepository;
        }
        public IActionResult Index()
        {
            var especialidad = _especialidadRepository.Listar();
            return View(especialidad);
        }

        public IActionResult Insertar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insertar(Especialidad especialidad)
        {
            _especialidadRepository.Insertar(especialidad);
            return RedirectToAction("Index");
        }

        public IActionResult Actualizar(int id)
        {
            var especialidad = _especialidadRepository.ObtenerEspecialidadPorId(id);
            return View(especialidad);
        }

        [HttpPost]
        public IActionResult Actualizar(Especialidad especialidad)
        {
            _especialidadRepository.Actualizar(especialidad);
            return RedirectToAction("Index");
        }

        public IActionResult Desactivar(int id)
        {
            var resultado = _especialidadRepository.Desactivar(id);

            if (resultado != "OK")
                TempData["Error"] = resultado;
            else
                TempData["Exito"] = "Especialidad desactivada correctamente";

            return RedirectToAction("Index");
        }

    }
}
