// Repositories/IProfessorRepository.cs
using GerenciadorAcademico.Models;

namespace GerenciadorAcademico.Repositories
{
    public interface IProfessorRepository
    {
        Task<List<Professor>> ListarAsync();
        Task<Professor?> ObterPorIdAsync(int id);
    }
}