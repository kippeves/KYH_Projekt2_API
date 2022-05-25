using System.ComponentModel.DataAnnotations;

namespace KYHProjekt2API.DTO.Project;

public class CreateProjectDTO
{
    [Required(ErrorMessage = "Du glömde skriva in ett namn")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Projektet behöver en ägande.")]
    public int? CustomerId { get; set; }
}