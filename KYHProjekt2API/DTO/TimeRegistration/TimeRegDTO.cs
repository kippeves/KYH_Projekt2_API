using System.ComponentModel.DataAnnotations;
using KYHProjekt2API.DTO.Customer;
using KYHProjekt2API.DTO.Project;

namespace KYHProjekt2API.DTO.TimeRegistration;

public class TimeRegDTO
{
    public int Id { get; set; }
    public CustomerDTO Customer { get; set; }
    public ProjectDTO Project { get; set; }
    [Required]
    public DateTime? EventStart { get; set; }
    [Required]
    public DateTime? EventEnd { get; set; }
    public string Description { get; set; }   
}