using System.ComponentModel.DataAnnotations;

namespace GerenciadorAcademico.ViewModels
{
    public class EditarProjetoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o título do projeto.")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a descrição do projeto.")]
        public string Descricao { get; set; } = string.Empty;

        [Range(1, 1000, ErrorMessage = "A carga horária deve estar entre 1 e 1000 horas.")]
        public int CargaHoraria { get; set; } 
    }
}
