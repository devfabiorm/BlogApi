using System.ComponentModel.DataAnnotations;

namespace BlogApi.ViewModels;

public class EditorCategoryViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "Este campo deve contner entre 3 e 40 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Slug { get; set; } = string.Empty;
}
