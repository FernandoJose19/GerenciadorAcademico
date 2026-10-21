using GerenciadorAcademicoAPI.Models;
//using GerenciadorAcademicoAPI.ViewModels;

namespace GerenciadorAcademicoAPI.Services
{
    public interface IProjetoService
    {
        Task<List<Projeto>> ListarAsync();
        Task<List<Projeto>> PesquisarPorTituloAsync(string? titulo);
        List<Projeto> Ordenar(IEnumerable<Projeto> projetos, string? ordenarPor);
        Task<Projeto?> ObterPorIdAsync(int id);
        Task<List<Professor>> ListarProfessoresAsync();
        Task AdicionarAsync(Projeto model);
        Task<bool> AtualizarAsync(Projeto model);
        Task<bool> RemoverAsync(int id);
    }
}