using System.ComponentModel.DataAnnotations;

namespace KYHProjekt2API.DTO.TimeRegistration;

public class CreateTimeRegDTO
{
    [Required(ErrorMessage = "Registreringen")]
    public int? CustomerID { get; set; }
    [Required]
    public int? ProjectID { get; set; }
    [Required]
    public string EventStart { get; set; }
    [Required]
    public string EventEnd { get; set; }
    public string? Description { get; set; }   
}