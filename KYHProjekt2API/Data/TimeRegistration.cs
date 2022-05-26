using System.ComponentModel.DataAnnotations;

namespace KYHProjekt2API.Data;

public class TimeRegistration
{
    public int Id { get; set; }
    [Required]
    public Customer? Customer { get; set; }
    [Required]
    public Project? Project { get; set; }
    [Required]
    public DateTime? EventStart { get; set; }
    [Required]
    public DateTime? EventEnd { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; } = true;
}