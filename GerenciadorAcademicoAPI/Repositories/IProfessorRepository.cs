using GerenciadorAcademicoAPI.Models;

namespace GerenciadorAcademicoAPI.Repositories
{
    public interface IProfessorRepository
    {
        Task<List<Professor>> ListarAsync();
        Task<Professor?> ObterPorIdAsync(int id);
    }

}
