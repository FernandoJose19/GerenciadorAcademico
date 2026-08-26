using GerenciadorAcademico.Models;
using GerenciadorAcademico.ViewModels;


namespace GerenciadorAcademico.Services;

public class ProjetoService : IProjetoService
{
    private readonly List<Professor> _professores =
    [
        new Professor
        {
            Id = 1,
            Nome = "Ana Souza",
            Email = "ana.souza@academico.com"
        },

        new Professor
        {
            Id = 2,
            Nome = "Carlos Mendes",
            Email = "carlos.mendes@academico.com"
        },

        new Professor
        {
            Id = 3,
            Nome = "Beatriz Lima",
            Email = "beatriz.lima@academico.com"
        }
    ];

    private readonly List<Projeto> _projetos =
    [
        new Projeto
        {
            Id = 1,
            Titulo = "Sistema de Biblioteca Escolar",
            Descricao = "Aplicação para controle de livros e empréstimos.",
            CargaHoraria = 80,
            ProfessorId = 1

        },
        new Projeto
        {
            Id = 2,
            Titulo = "Portal de Projetos Acadêmicos",
            Descricao = "Sistema para divulgação de projetos desenvolvidos pelos estudantes.",
            CargaHoraria = 60,
            ProfessorId = 2

        },
        new Projeto
        {
            Id = 3,
            Titulo = "Aplicativo Educacional",
            Descricao = "Aplicativo para apoio ao processo educacional.",
            CargaHoraria = 60,
            ProfessorId = 3
        }


        ];
    public List<Projeto> Listar()
    {
        return _projetos
        .Select(VincularProfessor)
        .ToList();
    }

    public Projeto? ObterPorId(int id)
    {
        var projeto = _projetos.FirstOrDefault(p => p.Id == id);

        if (projeto == null)
            return null;

        return VincularProfessor(projeto);
    }

    public void Adicionar(NovoProjetoViewModel vm)
    {
        var professor = _professores.FirstOrDefault(p => p.Id == vm.ProfessorId);

        var novoProjeto = new Projeto
        {
            Id = GerarNovoId(),
            Titulo = vm.Titulo,
            Descricao = vm.Descricao,
            CargaHoraria = vm.CargaHoraria,
            ProfessorId = vm.ProfessorId!.Value,
            Professor = professor
        };

        _projetos.Add(novoProjeto);
    }

    public bool Atualizar(EditarProjetoViewModel model)
    {
        var projeto = ObterPorId(model.Id);

        if (projeto is null)
            return false;

        var professor = _professores.FirstOrDefault(p => p.Id == model.ProfessorId);

        projeto.Titulo = model.Titulo;
        projeto.Descricao = model.Descricao;
        projeto.CargaHoraria = model.CargaHoraria;
        projeto.ProfessorId = model.ProfessorId!.Value;
        projeto.Professor = professor;

        return true;
    }

    public bool Remover(int id)
    {
        var projeto = ObterPorId(id);

        if (projeto is null)
            return false;

        _projetos.Remove(projeto);
        return true;
    }
    private int GerarNovoId()
    {
        return _projetos.Count == 0 ? 1 : _projetos.Max(projeto => projeto.Id) + 1;
    }


    
    public List<Projeto> PesquisarPorTitulo(string? titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            return Listar();

        return _projetos
            .Where(projeto => projeto.Titulo.Contains(titulo, StringComparison.CurrentCultureIgnoreCase))
            .ToList();
    }

    public List<Projeto> Ordenar(IEnumerable<Projeto> projetos, string? ordenarPor)
    {
        return ordenarPor?.ToLowerInvariant() switch
        {
            "titulo" => projetos.OrderBy(projeto => projeto.Titulo).ToList(),
            "cargahoraria" => projetos.OrderBy(projeto => projeto.CargaHoraria).ToList(),
            _ => projetos.ToList()
        };
    }

    public List<Professor> ListarProfessores()
    {
        return _professores;
    }

    private Professor? ObterProfessorPorId(int professorId)
    {
        return _professores.FirstOrDefault(p => p.Id == professorId);
    }

    private Projeto VincularProfessor(Projeto projeto)
    {
        projeto.Professor = ObterProfessorPorId(projeto.ProfessorId);

        return projeto;
    }



}