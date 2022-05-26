using System.ComponentModel.DataAnnotations;

namespace KYHProjekt2API.DTO.TimeRegistration;

public class CreateTimeRegDTO
{
    [Required(ErrorMessage = "Giltig kund krävs.")]
    public int? CustomerID { get; set; }
    [Required(ErrorMessage = "Giltigt projekt krävs.")]
    public int? ProjectID { get; set; }
    [Required(ErrorMessage = "Det krävs en startpunkt för tidsregistreringen.")]
    public string EventStart { get; set; }
    [Required(ErrorMessage = "Det krävs en slutpunkt för tidsregistreringen.")]
    public string EventEnd { get; set; }
    public string? Description { get; set; }   
}