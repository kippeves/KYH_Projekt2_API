using System.ComponentModel.DataAnnotations;

namespace KYHProjekt2API.DTO.Project;

public class CreateProjectDTO
{
    [Required(ErrorMessage = "Projektet behöver ett namn")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Projektet behöver en ägare.")]
    public int? CustomerId { get; set; }
}