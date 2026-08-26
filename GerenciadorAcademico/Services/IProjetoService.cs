using GerenciadorAcademico.Models;
using GerenciadorAcademico.ViewModels;

namespace GerenciadorAcademico.Services;

public interface IProjetoService
{
    List<Projeto> Listar();
    Projeto? ObterPorId(int id);
    void Adicionar(NovoProjetoViewModel model);
    bool Atualizar(EditarProjetoViewModel model);
    bool Remover(int id);
    List<Projeto> PesquisarPorTitulo(string? titulo);
    List<Projeto> Ordenar(IEnumerable<Projeto> projetos, string? ordenarPor);
    List<Professor> ListarProfessores();

}
