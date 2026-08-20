using Microsoft.AspNetCore.Mvc;
using GerenciadorAcademico.Services;
using GerenciadorAcademico.ViewModels;

namespace GerenciadorAcademico.Controllers
{
    public class ProjetosController : Controller
    {
        private readonly IProjetoService _projetoService;

        // O construtor recebe o serviço injetado automaticamente
        public ProjetosController(IProjetoService projetoService)
        {
            _projetoService = projetoService;
        }

        public IActionResult Index(string? pesquisa, string? ordenarPor)
        {
            var projetos = _projetoService.PesquisarPorTitulo(pesquisa);

            projetos = _projetoService.Ordenar(
                projetos,
                ordenarPor);

            var model = new ProjetosIndexViewModel
            {
                Projetos = projetos,
                TextoPesquisa = pesquisa,
                QuantidadeTotal = projetos.Count,
                OrdenarPor = ordenarPor
            };

            return View(model);
        }


        public IActionResult Detalhes(int id)
        {
            var projeto = _projetoService.ObterPorId(id);

            if (projeto is null)
                return NotFound();

            return View(projeto);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(NovoProjetoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _projetoService.Adicionar(model);
            TempData["Mensagem"] = "Projeto cadastrado com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var projeto = _projetoService.ObterPorId(id);

            if (projeto is null)
                return NotFound();

            var model = new EditarProjetoViewModel
            {
                Id = projeto.Id,
                Titulo = projeto.Titulo,
                Descricao = projeto.Descricao,
                CargaHoraria = projeto.CargaHoraria
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(EditarProjetoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var atualizado = _projetoService.Atualizar(model);

            if (!atualizado)
                return NotFound();

            TempData["Mensagem"] = "Projeto atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var projeto = _projetoService.ObterPorId(id);

            if (projeto is null)
                return NotFound();

            return View(projeto);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarExclusao(int id)
        {
            var removido = _projetoService.Remover(id);

            if (!removido)
                return NotFound();

            TempData["Mensagem"] = "Projeto excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}