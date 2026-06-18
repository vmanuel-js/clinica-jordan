using ClinicaJordan.Web.Data;
using ClinicaJordan.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicaJordan.Web.Controllers
{
    public class MedicoController : Controller
    {
        private readonly MedicoRepository _medicoRepository;
        private readonly EspecialidadRepository _especialidadRepository;

        public MedicoController(MedicoRepository medicoRepository, EspecialidadRepository especialidadRepository)
        {
            _medicoRepository = medicoRepository;
            _especialidadRepository = especialidadRepository;
        }

        public IActionResult Index()
        {
            var medicos = _medicoRepository.Listar();
            return View(medicos);
        }

        public IActionResult Insertar()
        {
            ViewBag.Especialidades = new SelectList(_especialidadRepository.Listar(), "IdEspecialidad", "Nombre");
            return View();
        }

        [HttpPost]
        public IActionResult Insertar(Medico medico)
        {
            _medicoRepository.Insertar(medico);
            return RedirectToAction("Index");
        }

        public IActionResult Actualizar(string dni)
        {
            var medicos = _medicoRepository.ObtenerPorDni(dni);
            ViewBag.Especialidades = new SelectList(_especialidadRepository.Listar(), "IdEspecialidad", "Nombre");
            return View(medicos);
        }

        [HttpPost]
        public IActionResult Actualizar(Medico medico)
        {
            _medicoRepository.Actualizar(medico);
            return RedirectToAction("Index");
        }

        public IActionResult Desactivar(string dni)
        {
            var resultado = _medicoRepository.Desactivar(dni);

            if (resultado != "OK")
                TempData["Error"] = resultado;
            else
                TempData["Exito"] = "Médico desactivado correctamente";
            return RedirectToAction("Index");
        }
    }
}
