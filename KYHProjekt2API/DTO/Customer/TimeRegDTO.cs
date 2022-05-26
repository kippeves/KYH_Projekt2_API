using System.ComponentModel.DataAnnotations;

namespace KYHProjekt2API.DTO.Customer;

public class TimeRegDTO
{
    public int Id { get; set; }
    [Required]
    public DateTime? EventStart { get; set; }
    [Required]
    public DateTime? EventEnd { get; set; }
    public string Description { get; set; }   
}