using GerenciadorAcademico.Models;
using GerenciadorAcademico.Repositories;
using GerenciadorAcademico.ViewModels;

namespace GerenciadorAcademico.Services
{
    public class ProjetoService : IProjetoService
    {
        private readonly IProjetoRepository _projetoRepository;
        private readonly IProfessorRepository _professorRepository;

        public ProjetoService(IProjetoRepository projetoRepository, IProfessorRepository professorRepository)
        {
            _projetoRepository = projetoRepository;
            _professorRepository = professorRepository;
        }

        public Task<List<Professor>> ListarProfessoresAsync()
        {
            return _professorRepository.ListarAsync();
        }

        public Task<List<Projeto>> ListarAsync()
        {
            return _projetoRepository.ListarAsync();
        }

        public async Task<List<Projeto>> PesquisarPorTituloAsync(
            string? titulo)
        {
            var projetos = await ListarAsync();

            if (string.IsNullOrWhiteSpace(titulo))
                return projetos;

            return projetos
                .Where(projeto => projeto.Titulo.Contains(
                    titulo,
                    StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }

        public List<Projeto> Ordenar(
            IEnumerable<Projeto> projetos,
            string? ordenarPor)
        {
            return ordenarPor?.ToLowerInvariant() switch
            {
                "titulo" => projetos
                    .OrderBy(projeto => projeto.Titulo)
                    .ToList(),

                "cargahoraria" => projetos
                    .OrderBy(projeto => projeto.CargaHoraria)
                    .ToList(),

                _ => projetos.ToList()
            };
        }

        public Task<Projeto?> ObterPorIdAsync(int id)
        {
            return _projetoRepository.ObterPorIdAsync(id);
        }

        public async Task AdicionarAsync(NovoProjetoViewModel model)
        {
            var professor = await _professorRepository
                .ObterPorIdAsync(model.ProfessorId.Value);

            if (professor is null)
            {
                throw new InvalidOperationException(
                    "O professor selecionado não existe.");
            }

            var projeto = new Projeto
            {
                Titulo = model.Titulo,
                Descricao = model.Descricao,
                CargaHoraria = model.CargaHoraria,
                ProfessorId = model.ProfessorId.Value
            };

            await _projetoRepository.AdicionarAsync(projeto);
        }

        public async Task<bool> AtualizarAsync(
            EditarProjetoViewModel model)
        {
            var professor = await _professorRepository
                .ObterPorIdAsync(model.ProfessorId.Value);

            if (professor is null)
                return false;

            var projeto = new Projeto
            {
                Id = model.Id,
                Titulo = model.Titulo,
                Descricao = model.Descricao,
                CargaHoraria = model.CargaHoraria,
                ProfessorId = model.ProfessorId.Value,
                //Area = model.Area
            };

            return await _projetoRepository.AtualizarAsync(projeto);
        }

        public Task<bool> RemoverAsync(int id)
        {
            return _projetoRepository.RemoverAsync(id);
        }
    }
}