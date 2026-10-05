using GerenciadorAcademico.Data;
using GerenciadorAcademico.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorAcademico.Repositories
{
    public class ProjetoEfRepository : IProjetoRepository
    {
        private readonly AcademicoContext _context;

        public ProjetoEfRepository(AcademicoContext context)
        {
            _context = context;
        }

        public async Task<List<Projeto>> ListarAsync()
        {
            return await _context.Projetos
                .AsNoTracking()
                .Include(projeto => projeto.Professor)
                .OrderBy(projeto => projeto.Id)
                .ToListAsync();
        }

        public async Task<Projeto?> ObterPorIdAsync(int id)
        {
            return await _context.Projetos
                .AsNoTracking()
                .Include(projeto => projeto.Professor)
                .FirstOrDefaultAsync(projeto => projeto.Id == id);
        }

        public async Task AdicionarAsync(Projeto projeto)
        {
            _context.Projetos.Add(projeto);

            await _context.SaveChangesAsync();
        }



        public async Task<bool> AtualizarAsync(Projeto projeto)
        {
            var existente = await _context.Projetos
                .FirstOrDefaultAsync(item => item.Id == projeto.Id);

            if (existente is null)
                return false;

            existente.Titulo = projeto.Titulo;
            existente.Descricao = projeto.Descricao;
            existente.CargaHoraria = projeto.CargaHoraria;
            existente.ProfessorId = projeto.ProfessorId;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var projeto = await _context.Projetos
                .FirstOrDefaultAsync(item => item.Id == id);

            if (projeto is null)
                return false;

            _context.Projetos.Remove(projeto);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
