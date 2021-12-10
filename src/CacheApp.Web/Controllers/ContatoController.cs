using CacheApp.Web.Contracts;
using CacheApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CacheApp.Web.Controllers
{
    [Controller]
    public class ContatoController : Controller
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoController(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public IActionResult Index()
        {
            
            return View(TodosContatos);
        }

        private IEnumerable<Contato> TodosContatos => _contatoRepository.ListarTodos();

        public IActionResult Novo()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Novo(Contato contato)
        {
            _contatoRepository.Adicionar(contato);
            return RedirectToAction("Index");
        }

        public IActionResult Detalhes(Guid id)
        {
            return View(_contatoRepository.Obter(id));
        }
    }
}
