using GerenciadorAcademicoAPI.Models;
using GerenciadorAcademicoAPI.Services;
using GerenciadorAcademicoAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorAcademicoAPI.Controllers
{
    [ApiController]
    [Route("api/projetos")]
    public class ProjetosController : ControllerBase
    {
        private readonly IProjetoService _projetoService;

        public ProjetosController(IProjetoService projetoService)
        {
            _projetoService = projetoService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar(
            [FromQuery] string? pesquisa,
            [FromQuery] string? ordenarPor)
        {
            var projetos = await _projetoService.PesquisarPorTituloAsync(pesquisa);
            projetos = _projetoService.Ordenar(projetos, ordenarPor);

            return Ok(projetos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var projeto = await _projetoService.ObterPorIdAsync(id);

            if (projeto is null)
                return NotFound();

            return Ok(projeto);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] NovoProjetoViewModel model)
        {
            try
            {
                await _projetoService.AdicionarAsync(model);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }

            return StatusCode(StatusCodes.Status201Created, new { mensagem = "Projeto cadastrado com sucesso!" });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Editar(int id, [FromBody] EditarProjetoViewModel model)
        {
            if (id != model.Id)
                return BadRequest(new { mensagem = "O id da rota não corresponde ao id do corpo da requisição." });

            var atualizado = await _projetoService.AtualizarAsync(model);

            if (!atualizado)
                return NotFound();

            return Ok(new { mensagem = "Projeto atualizado com sucesso!" });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var removido = await _projetoService.RemoverAsync(id);

            if (!removido)
                return NotFound();

            return Ok(new { mensagem = "Projeto excluído com sucesso!" });
        }
    }
}
