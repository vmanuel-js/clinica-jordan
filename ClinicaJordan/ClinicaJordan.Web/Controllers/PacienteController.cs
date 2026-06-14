using ClinicaJordan.Web.Data;
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

        public IActionResult Index()
        {
            var pacientes = _pacienteRepository.ObtenerTodos();
            return View(pacientes);
        }
    }
}
