using GerenciadorAcademicoAPI.Data;
using GerenciadorAcademicoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorAcademicoAPI.Repositories
{
    public class ProfessorEfRepository : IProfessorRepository
    {
        private readonly AcademicoContext _context;

        public ProfessorEfRepository(AcademicoContext context)
        {
            _context = context;
        }

        public async Task<List<Professor>> ListarAsync()
        {
            return await _context.Professores
                .AsNoTracking()
                .OrderBy(professor => professor.Nome)
                .ToListAsync();
        }

        public async Task<Professor?> ObterPorIdAsync(int id)
        {
            return await _context.Professores
                .AsNoTracking()
                .FirstOrDefaultAsync(professor => professor.Id == id);
        }
    }
}
