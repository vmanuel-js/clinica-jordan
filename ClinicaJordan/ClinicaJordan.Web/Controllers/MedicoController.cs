using ClinicaJordan.Web.Data;
using ClinicaJordan.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaJordan.Web.Controllers
{
    public class MedicoController : Controller
    {
        private readonly MedicoRepository _medicoRepository;

        public MedicoController(MedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public IActionResult Index()
        {
            var medicos = _medicoRepository.Listar();
            return View(medicos);
        }

        public IActionResult Insertar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insertar(Medico medico)
        {
            _medicoRepository.Insertar(medico);
            return RedirectToAction("Index");
        }
    }
}
