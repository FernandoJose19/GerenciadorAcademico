using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciadorAcademico.ViewModels;

public class ProjetoFormularioViewModel
{
    [Required(ErrorMessage = "Informe o título do projeto.")]
    [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
    [Display(Name = "Título")]
    public string Titulo { get; set; } = string.Empty;

    [Display(Name = "Descrição")]
    [Required(ErrorMessage = "Informe a descrição do projeto.")]
    [MinLength(20,ErrorMessage = "A descrição deve ter pelo menos 20 caracteres.")]
    [MaxLength(1000,ErrorMessage = "A descrição deve ter no máximo 1000 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Range(1, 1000, ErrorMessage = "A carga horária deve estar entre 1 e 1000 horas.")]
    [Display(Name = "Carga Horária")]
    public int CargaHoraria { get; set; }

    [Required(ErrorMessage = "Você precisa selecionar um professor.")]
    [Display(Name = "Professor")]
    public int ProfessorId { get; set; }

    public List<SelectListItem> Professores { get; set; } = [];
}