using ClinicaJordan.Web.Data;
using ClinicaJordan.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaJordan.Web.Controllers
{
    public class PacienteController : Controller
    {
        private readonly PacienteRepository _pacienteRepository;

        public PacienteController(PacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public IActionResult Crear()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Crear(Paciente paciente)
        {
            _pacienteRepository.Insertar(paciente);
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var pacientes = _pacienteRepository.ObtenerTodos();
            return View(pacientes);
        }
    }
}
