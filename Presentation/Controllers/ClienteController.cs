using System.Web.Mvc;
using Application.DTOs;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Presentation.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClienteController()
        {}

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public async Task<ActionResult> Index()
        {
            var clientesDto = await _clienteService.ObterTodosAsync();
            return View(clientesDto);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var cliente = await _clienteService.ObterPorIdAsync(id);
            if (cliente is null)
                return RedirectToAction("NotFound", "Error");

            return View(cliente);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClienteDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _clienteService.CadastrarAsync(dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var cliente = await _clienteService.ObterPorIdAsync(id);
            if (cliente is null)
                return RedirectToAction("NotFound", "Error");

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ClienteDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _clienteService.AtualizarAsync(dto.Id, dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var cliente = await _clienteService.ObterPorIdAsync(id);
            if (cliente is null)
                return RedirectToAction("NotFound", "Error");

            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _clienteService.RemoverAsync(id);
            return RedirectToAction("Index");
        }
    }
}